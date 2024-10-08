﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Campus;
using FISCA;
using K12.Data;
using StudentTransferAPI;
using FISCA.DSAClient;
using FISCA.Authentication;
using System.Xml.XPath;
using StudentTransferCoreImpl;
using ClassLock_KH.DAO;

namespace StudentTransferStudentBrief_KH
{
    public partial class StudentBrief : WizardForm
    {
        private TransferInRecord Record { get; set; }

        /// <summary>
        /// 學生基本資料。
        /// </summary>
        private StudentRecord SRecord { get; set; }

        /// <summary>
        /// 學生班級資料。
        /// </summary>
        private ClassRecord CRecord { get; set; }

        /// <summary>
        /// 學生地址資料。
        /// </summary>
        private AddressRecord ARecord { get; set; }

        /// <summary>
        /// 鄉鎮縣市列表
        /// </summary>
        private List<TownRecord> TownList { get; set; }

        /// <summary>
        /// 縣市列表
        /// </summary>
        private List<string> CountyList { get; set; }

        private XElement XmlData { get; set; }

        private string _ClassID { get; set; }

        private int seatno_Max;

        List<string> sameClassStudentIDList = new List<string>();

        public StudentBrief(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();
            XmlData = null;
        }

        /// <summary>
        /// 載入表單
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StudentBrief_Load(object sender, EventArgs e)
        {
            try
            {
                Record = Arguments[Consts.TransferInRecord] as TransferInRecord;
                XmlData = Arguments[Consts.XmlData] as XElement;
                dynamic briefSection = (DynamicXmlObject)XmlData.Element("Student");
                //取得地址資料
                dynamic permanentAddress = GetAddressData();
                //<PermanentAddress>
                //  <AddressList>
                //    <Address>
                //      <ZipCode>303</ZipCode>
                //      <County>新竹縣</County>
                //      <Town>湖口鄉</Town>
                //      <District />
                //      <Area />
                //      <DetailAddress>嶺尾8號</DetailAddress>
                //    </Address>
                //  </AddressList>
                //</PermanentAddress>

                string urAction = briefSection["@UpdateRecordAction"];
                if (!string.IsNullOrWhiteSpace(urAction))
                    Arguments[Consts.UpdateRecordAction] = Enum.Parse(typeof(ContinueDirection), urAction);

                SelectStudentAddressRecord(briefSection);

                if (SRecord != null)
                {
                    if (!string.IsNullOrWhiteSpace(SRecord.RefClassID))
                        CRecord = SRecord.Class;

                    FillStudentNumberLast(SRecord.StudentNumber);
                }
                else
                    FillStudentNumberLast(string.Empty);



                if (SRecord == null)
                    SingleMode();
                else
                    FromOriginData();

                NextButtonEnabled = false;
                ClassRunning.IsRunning = true;
                ClassRunning.Visible = true;
                Task task = Task.Factory.StartNew(() =>
                {
                    LoadClassRowSource();

                    TownList = K12.Data.Town.SelectAll();

                    CountyList = K12.Data.Town.SelectCountyList();

                }, new CancellationToken(), TaskCreationOptions.None, TaskScheduler.Default);

                task.ContinueWith(x =>
                {
                    if (x.IsFaulted)
                    {
                        RTOut.WriteError(x.Exception);
                        MessageBox.Show(x.Exception.Message);
                        NextButtonEnabled = false;
                    }

                    SuspenSelectedValueChanged = true;
                    cmbTown.DataSource = TownList.Select(y => y.Area).ToList();
                    cmbCounty.DataSource = CountyList;
                    FromTransferData(briefSection, permanentAddress);
                    //cboClass.DataSource = ClassRowSource;
                    //cboClass.ValueMember = "ID";
                    //cboClass.DisplayMember = "Name";
                    SuspenSelectedValueChanged = false;
                    cboGradeYear.Text = "";
                    List<string> grList = Utility.GetGradeYearList();
                    cboGradeYear.Items.AddRange(grList.ToArray());

                    if (SRecord != null)
                    {
                        cboSeatNo.Text = SRecord.SeatNo + "";
                        cboStudentNumber.Text = SRecord.StudentNumber;

                        //if (CRecord != null)
                        //    cboClass.SelectedIndex = cboClass.FindStringExact(CRecord.Name);
                    }

                    // 如果學生是同校轉出又轉入，回到原班
                    if (CRecord != null)
                    {
                        // 2018/8/29 穎驊因應 高雄專案 [08-02][02]轉入學生編班  修正規則， 
                        // 如果學生本為同校生，則會有有選項，讓使用者選擇是否要回原班級，或是依照局端規則，優先提供轉入班級
                        List<KH_HighConcernCalc.ClassStudent> grClassList = Utility.GetClassNameFirst_List("" + CRecord.GradeYear);
                        
                        ClassMessage cm = new ClassMessage(CRecord.Name, grClassList.Find(c => c.ClassID == CRecord.ID).ClassStudentCount, grClassList[0].ClassName, grClassList[0].ClassStudentCount);
                        DialogResult dr = cm.ShowDialog();
                        if (cm.DialogResult == DialogResult.Yes)
                        {
                            txtClass.Text = CRecord.Name;

                        }
                        else
                        {
                            txtClass.Text = grClassList[0].ClassName;
                        }
                    }


                    txtClass.ReadOnly = true;

                    ClassRunning.IsRunning = false;
                    ClassRunning.Visible = false;
                    NextButtonEnabled = true;
                }, TaskScheduler.FromCurrentSynchronizationContext());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                RTOut.WriteError(ex);
                WizardResult = ContinueDirection.Cancel;
                Close();
            }
        }

        private dynamic GetAddressData()
        {
            XElement temp = XmlData.Element("StudentComplete");

            if (temp != null)
                temp = temp.Element("PermanentAddress");

            if (temp != null)
                temp = temp.Element("AddressList");

            if (temp != null)
                temp = temp.Element("Address");

            if (temp == null)
                return new DynamicXmlObject("Address");
            else
                return (DynamicXmlObject)temp;
        }

        protected override void OnRunningChanged()
        {
            NextButtonEnabled = !Running;
        }

        #region Class Selection ComboBox
        private List<ClassItem> ClassRowSource = new List<ClassItem>();

        private void LoadClassRowSource()
        {
            string cmd = "select id,class_name from class order by class_name";
            DataTable dt = Query.Select(cmd);

            ClassRowSource.Clear();
            ClassRowSource.Add(new ClassItem("", ""));
            foreach (DataRow row in dt.Rows)
            {
                ClassItem item = new ClassItem(row["id"].ToString(), row["class_name"].ToString());
                ClassRowSource.Add(item);
            }
        }

        private class ClassItem
        {
            public ClassItem(string id, string name)
            {
                ID = id;
                Name = name;
            }

            public string ID { get; set; }

            public string Name { get; set; }
        }
        #endregion

        #region 產生空座號
        private void FillEmptySeatNos(int? currentSeatNo, string ClassID)
        {
            string classId = "0";

            SeatNoRunning.IsRunning = true;
            SeatNoRunning.Visible = true;

            HashSet<int> EmptySeatNos = new HashSet<int>();

            classId = ClassID;

            Task task = new Task(() =>
            {//非同步讀取空座號資料。
                EmptySeatNos = GetEmptySeatNos(classId);
            });
            task.Start(TaskScheduler.Default);

            task.ContinueWith(x =>
            {
                if (x.IsFaulted)
                    cboSeatNo.Items.Clear();
                else
                {
                    cboSeatNo.Items.Clear();
                    cboSeatNo.Items.Add("");

                    //如果目前學生的座號不包含，就加入。
                    if (currentSeatNo.HasValue && !EmptySeatNos.Contains(currentSeatNo.Value))
                        EmptySeatNos.Add(currentSeatNo.Value);

                    List<object> seatnos = new List<object>();
                    foreach (int item in EmptySeatNos)
                        seatnos.Add(item);

                    seatnos.Sort();

                    cboSeatNo.Items.AddRange(seatnos.ToArray());
                }

                SeatNoRunning.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private HashSet<int> GetEmptySeatNos(string classId)
        {
            if (string.IsNullOrWhiteSpace(classId)) return new HashSet<int>();

            try
            {
                ////目前有的座號。
                //List<int> currents = new List<int>();
                //string cmd = string.Format("select seat_no from student where ref_class_id='{0}' and status='1' group by seat_no order by seat_no", classId);
                //DataTable seatNoList = Query.Select(cmd);

                //foreach (DataRow row in seatNoList.Rows)
                //{
                //    int seatno;
                //    if (int.TryParse(row["seat_no"].ToString(), out seatno))
                //        currents.Add(seatno);
                //}

                ////算人數。
                //int count = 0;
                //cmd = string.Format("select case when count(*)>max(seat_no) then count(*) else max(seat_no) end  count from student where ref_class_id='{0}' and status='1'", classId);
                //DataTable peopleCount = Query.Select(cmd);

                //foreach (DataRow row in peopleCount.Rows)
                //    count = int.Parse(row["count"].ToString());

                //HashSet<int> allseatno = new HashSet<int>();
                //for (int i = 1; i <= count; i++)
                //    allseatno.Add(i);

                //allseatno.ExceptWith(currents);

                //if (!allseatno.Contains(count + 1))
                //    allseatno.Add(count + 1);

                //2018/3/15 穎驊註解，因應高雄小組 [09-04][02] 轉入生學號怎麼了? 項目， 日後 轉入學生的建議號碼，一律是該班用過的最大號碼加一(不管學生的狀態)，
                //如此一來，系統建議提供的建議學號，就不會有重覆的問題

                HashSet<int> allseatno = new HashSet<int>();

                //不管狀態，該班有用過的號碼通通拿出來
                string cmd = string.Format("select id,seat_no from student where ref_class_id='{0}'order by seat_no", classId);

                DataTable seatNoList = Query.Select(cmd);

                int seatno = 0;
                seatno_Max = 0;
                foreach (DataRow row in seatNoList.Rows)
                {
                    if (int.TryParse(row["seat_no"].ToString(), out seatno))
                    {
                        //取最大號碼
                        if (seatno > seatno_Max)
                        {
                            seatno_Max = seatno;
                        }
                    }

                    sameClassStudentIDList.Add(row["id"].ToString());
                }

                // 最大號碼加一 為建議
                allseatno.Add(seatno_Max + 1);

                return allseatno;
            }
            catch (Exception e)
            {
                return new HashSet<int>();
            }
        }
        #endregion

        #region 產生建議學號項目
        private void FillStudentNumberLast(string currentStuNumber)
        {
            cboStudentNumber.Items.Clear();
            //算出最後一個學號。
            int stuNumber = 0;
            string cmd = "select student_number from student where status=1 and not student_number is null order by student_number desc limit 1";

            Task<int> task = Task.Factory.StartNew<int>(() =>
            {
                foreach (DataRow row in Query.Select(cmd).Rows)
                {
                    if (!int.TryParse(row["student_number"].ToString(), out stuNumber))
                        stuNumber = -1;
                }
                return stuNumber + 1;
            }, new CancellationToken(), TaskCreationOptions.None, TaskScheduler.Default);

            task.ContinueWith((t) =>
            {
                if (t.Result > 0)
                {
                    cboStudentNumber.Items.Add("");
                    if (!string.IsNullOrWhiteSpace(currentStuNumber))
                        cboStudentNumber.Items.Add(currentStuNumber);
                    cboStudentNumber.Items.Add(t.Result.ToString());
                }

                //2018/3/15 穎驊註解，因應高雄小組 [09-04][02] 轉入生學號怎麼了? 項目， 日後 轉入學生的建議號碼，一律是該班用過的最大號碼加一(不管學生的狀態)，
                //如此一來，系統建議提供的建議學號，就不會有重覆的問題
                if (_ClassID != null && cboSeatNo.Text != "")
                {
                    cmd = string.Format("select id,seat_no from student where ref_class_id='{0}'order by seat_no", _ClassID);

                    DataTable seatNoList = Query.Select(cmd);

                    int seatno = 0;
                    seatno_Max = 0;
                    foreach (DataRow row in seatNoList.Rows)
                    {
                        if (int.TryParse(row["seat_no"].ToString(), out seatno))
                        {
                            //取最大號碼
                            if (seatno > seatno_Max)
                            {
                                seatno_Max = seatno;
                            }
                        }

                        sameClassStudentIDList.Add(row["id"].ToString());
                    }

                    List<K12.Data.UpdateRecordRecord> updateRecod_List = K12.Data.UpdateRecord.SelectByStudentIDs(sameClassStudentIDList);

                    //整理入學年度的字典
                    Dictionary<int?, int> entrySchoolYearDict = new Dictionary<int?, int>();

                    foreach (K12.Data.UpdateRecordRecord ur in updateRecod_List)
                    {
                        //為新生異動，整理個學年度入學的人數
                        if (ur.UpdateCode == "1")
                        {
                            if (!entrySchoolYearDict.ContainsKey(ur.SchoolYear))
                            {
                                entrySchoolYearDict.Add(ur.SchoolYear, 1);
                            }
                            else
                            {
                                entrySchoolYearDict[ur.SchoolYear]++;
                            }
                        }
                    }

                    int? majorEntrySchoolYear = 0;
                    int count = 0;

                    //找尋最多人入學的那一年
                    foreach (KeyValuePair<int?, int> p in entrySchoolYearDict)
                    {
                        if (p.Value > count)
                        {
                            majorEntrySchoolYear = p.Key;
                            count = p.Value;
                        }
                    }

                    string majorEntrySchoolYear_string = "" + majorEntrySchoolYear;

                    // 入學年超過三碼 只取後兩碼(如106 >> 06)
                    if (majorEntrySchoolYear_string.Length > 2)
                    {
                        majorEntrySchoolYear_string = majorEntrySchoolYear_string.Remove(0, 1);
                    }

                    // 產生學號的性別碼(男=1,女=2)
                    int genderCode;
                    if (cboGender.Text == "男")
                    {
                        genderCode = 1;
                    }
                    else
                    {
                        genderCode = 2;
                    }

                    cmd = string.Format("select * from class where id='{0}'", _ClassID);

                    DataTable classList = Query.Select(cmd);

                    //抓班級名稱
                    string className = "" + classList.Rows[0]["class_name"];

                    int className_int;
                    //  抓取 學號的班級序號
                    string classOrder = "";

                    // 2018/3/27 穎驊註解，假如抓出來的班級名稱無法順利的轉成int 型別，代表其班級命名方式
                    // 有異於目前95%以上的高雄國中班級編碼模式(101、202、303等等)
                    // 可能為1A、國一忠、三年一班 這種格式
                    //如果出現此格式，則去抓它的班級排列序號
                    if (!int.TryParse(className, out className_int))
                    {

                        classOrder = "" + classList.Rows[0]["display_order"];

                        // 假如班級序號僅有一碼，補零(1>>01)
                        if (classOrder.Length == 1)
                        {
                            classOrder = "0" + classOrder;
                        }

                    }
                    else
                    {
                        classOrder = "" + className_int;

                        //取後兩碼
                        if (classOrder.Length > 2)
                        {
                            classOrder = classOrder.Remove(0, 1);
                        }
                    }


                    // 假如建議座號僅有一碼，補零(1>>01)
                    string seatno_Max_string = "" + (int.Parse(cboSeatNo.Text));

                    if (seatno_Max_string.Length == 1)
                    {
                        seatno_Max_string = "0" + seatno_Max_string;
                    }

                    string suggestStudentNumber = majorEntrySchoolYear_string + genderCode + classOrder + seatno_Max_string;

                    // 加入建議學號
                    cboStudentNumber.Items.Add(suggestStudentNumber);
                }



            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        #endregion

        /// <summary>
        /// 取得學生基本資料及地址記錄
        /// </summary>
        /// <param name="stuBrief"></param>
        private void SelectStudentAddressRecord(dynamic stuBrief)
        {
            if (string.IsNullOrWhiteSpace(stuBrief["@ID"])) //沒有執行過轉入流程。
            {
                //根據身份證字號來查詢
                Tuple<StudentRecord, AddressRecord> Record = QueryByIDNumber(stuBrief.IDNumber);

                SRecord = Record.Item1;
                ARecord = Record.Item2;
            }
            else
            {
                //從指定的 ID 中找該學生及學生地址
                SRecord = K12.Data.Student.SelectByID(stuBrief["@ID"]);
                ARecord = K12.Data.Address.SelectByStudentID(stuBrief["@ID"]);
            }
        }

        private void FromOriginData()
        {
            txtOName.Text = SRecord.Name;
            txtOIDNumber.Text = SRecord.IDNumber;
            cboOGender.Text = SRecord.Gender;
            dtOBirthday.Text = SRecord.Birthday.HasValue ? SRecord.Birthday.Value.ToString("yyyy/MM/dd") : "";
            txtOSeatNo.Text = SRecord.SeatNo + "";
            txtOStudentNumber.Text = SRecord.StudentNumber;
            txtOClass.Text = CRecord == null ? "" : CRecord.Name;

            txtOZipCode.Text = ARecord.PermanentZipCode;
            txtOCounty.Text = ARecord.PermanentCounty;
            txtOTown.Text = ARecord.PermanentTown;
            txtODistrict.Text = ARecord.PermanentDistrict;
            txtOArea.Text = ARecord.PermanentArea;
            txtODetail.Text = ARecord.PermanentDetail;
        }

        /// <summary>
        /// 將資料填入畫面
        /// </summary>
        /// <param name="student"></param>
        /// <param name="address"></param>
        private void FromTransferData(dynamic student, dynamic address)
        {
            txtName.Text = student.Name;
            txtIDNumber.Text = student.IDNumber;
            cboGender.Text = student.Gender;
            dtBirthday.Text = student.Birthdate;

            //<PermanentAddress>
            //  <AddressList>
            //    <Address>
            //      <ZipCode>303</ZipCode>
            //      <County>新竹縣</County>
            //      <Town>湖口鄉</Town>
            //      <District />
            //      <Area />
            //      <DetailAddress>嶺尾8號</DetailAddress>
            //    </Address>
            //  </AddressList>
            //</PermanentAddress>

            txtZipCode.Text = address.ZipCode;
            cmbCounty.Text = address.County;
            cmbTown.Text = address.Town;
            txtDistrict.Text = address.District;
            txtArea.Text = address.Area;
            txtDetail.Text = address.DetailAddress;
        }

        private void SingleMode()
        {
            gpTransfer.Location = new System.Drawing.Point(12, 12);
            Size = new System.Drawing.Size(316, 621);
        }

        /// <summary>
        /// 從資料庫中查詢該身分證號的學生，如果不存在則回傳 Null。
        /// </summary>
        private Tuple<StudentRecord, AddressRecord> QueryByIDNumber(string idNumber)
        {
            //除了刪除狀況的學生。
            string cmd = "select id,name,id_number,status from student where id_number='{0}' and status !=256";
            cmd = string.Format(cmd, idNumber);

            DataTable dt = Query.Select(cmd);

            if (dt.Rows.Count > 0)
            {
                string StudentID = dt.Rows[0]["id"] + "";

                StudentRecord vSRecord = Student.SelectByID(StudentID);
                AddressRecord vARecord = Address.SelectByStudentID(StudentID);

                return new Tuple<StudentRecord, AddressRecord>(vSRecord, vARecord);
            }
            else
                return new Tuple<StudentRecord, AddressRecord>(null, null);
        }

        protected override ContinueDirection? OnNextButtonClick()
        {
            try
            {
                string stuId = "0";
                if (SRecord != null)
                    stuId = SRecord.ID;

                errors.SetError(txtIDNumber, "");
                if (IsIDNumberExists(stuId, txtIDNumber.Text))
                {
                    errors.SetError(txtIDNumber, "身分證號重覆!");
                    return null;
                }

                errors.SetError(cboStudentNumber, "");
                if (IsStudentNumberExists(stuId, cboStudentNumber.Text))
                {
                    errors.SetError(cboStudentNumber, "學號重覆!");
                    return null;
                }

                errors.SetError(txtClass, "");
                if (string.IsNullOrWhiteSpace(txtClass.Text))
                {
                    errors.SetError(txtClass, "班級必填!");
                    return null;
                }

                //將資料寫回 XmlData。
                XElement student = XmlData.Element("Student");
                student.Element("Name").Value = txtName.Text;
                student.Element("IDNumber").Value = txtIDNumber.Text;
                student.Element("Birthdate").Value = dtBirthday.Text;
                student.Element("Gender").Value = cboGender.Text;

                XElement paddress = XmlData
                    .Element("StudentComplete")
                    .Element("PermanentAddress")
                    .Element("AddressList");

                if (paddress != null)
                {
                    paddress = paddress.Element("Address");

                    if (paddress.Element("ZipCode") == null)
                        paddress.Add(new XElement("ZipCode", txtZipCode.Text));
                    else
                        paddress.Element("ZipCode").Value = txtZipCode.Text;

                    if (paddress.Element("County") == null)
                        paddress.Add(new XElement("County", cmbCounty.Text));
                    else
                        paddress.Element("County").Value = cmbCounty.Text;

                    if (paddress.Element("Town") == null)
                        paddress.Add(new XElement("Town", cmbTown.Text));
                    else
                        paddress.Element("Town").Value = cmbTown.Text;

                    if (paddress.Element("District") == null)
                        paddress.Add(new XElement("District", txtDistrict.Text));
                    else
                        paddress.Element("District").Value = txtDistrict.Text;

                    if (paddress.Element("Area") == null)
                        paddress.Add(new XElement("Area", txtArea.Text));
                    else
                        paddress.Element("Area").Value = txtArea.Text;

                    if (paddress.Element("DetailAddress") == null)
                        paddress.Add(new XElement("DetailAddress", txtDetail.Text));
                    else
                        paddress.Element("DetailAddress").Value = txtDetail.Text;
                }
                else
                {
                    paddress = new XElement("Address");
                    paddress.Add(new XElement("ZipCode", txtZipCode.Text));
                    paddress.Add(new XElement("County", cmbCounty.Text));
                    paddress.Add(new XElement("Town", cmbTown.Text));
                    paddress.Add(new XElement("District", txtDistrict.Text));
                    paddress.Add(new XElement("Area", txtArea.Text));
                    paddress.Add(new XElement("DetailAddress", txtDetail.Text));

                    XmlData
                        .Element("StudentComplete")
                        .Element("PermanentAddress").Add(new XElement("AddressList", paddress));
                }
                //<StudentComplete Processor="StudentComplete">
                // <PermanentAddress>
                //   <AddressList>
                //     <Address>
                //       <ZipCode>310</ZipCode>
                //       <County>新竹縣</County>
                //       <Town>竹東鎮</Town>
                //       <District />
                //       <Area />
                //       <DetailAddress>北岸19號</DetailAddress>
                //     </Address>
                //   </AddressList>
                // </PermanentAddress>
                // </StudentComplete>

                if (SRecord != null)
                {
                    UpdateData(SRecord);
                    K12.Data.Student.Update(SRecord);
                    ARecord = K12.Data.Address.SelectByStudentID(SRecord.ID);
                    #region 更新學生地址
                    ARecord.Permanent.ZipCode = txtZipCode.Text;
                    ARecord.Permanent.County = cmbCounty.Text;
                    ARecord.Permanent.Town = cmbTown.Text;
                    ARecord.Permanent.District = txtDistrict.Text;
                    ARecord.Permanent.Area = txtArea.Text;
                    ARecord.Permanent.Detail = txtDetail.Text;

                    K12.Data.Address.Update(ARecord);

                    ARecord = K12.Data.Address.SelectByStudentID(ARecord.RefStudentID);

                    #endregion

                    student.SetAttributeValue("ID", SRecord.ID); //將 Xml 上標示新增的學生編號。
                    Arguments[Consts.StudentID] = SRecord.ID;
                }
                else
                {
                    SRecord = new StudentRecord();
                    UpdateData(SRecord);
                    string newid = K12.Data.Student.Insert(SRecord);

                    ////新竹市的國中，需要多呼叫數位學生證資料同步的 Service。
                    ////新增的狀態下才呼叫，以免重覆呼叫。
                    //if (Program.CurrentMode == Program.Hsinchu)
                    //    CallTransferInWS();

                    #region 更新學生地址
                    ARecord = K12.Data.Address.SelectByStudentID(newid);
                    ARecord.RefStudentID = newid;
                    ARecord.Permanent.ZipCode = txtZipCode.Text;
                    ARecord.Permanent.County = cmbCounty.Text;
                    ARecord.Permanent.Town = cmbTown.Text;
                    ARecord.Permanent.District = txtDistrict.Text;
                    ARecord.Permanent.Area = txtArea.Text;
                    ARecord.Permanent.Detail = txtDetail.Text;

                    K12.Data.Address.Update(ARecord);

                    ARecord = K12.Data.Address.SelectByStudentID(ARecord.RefStudentID);
                    #endregion

                    FISCA.Features.Invoke("StudentSyncAllBackground");

                    student.SetAttributeValue("ID", newid); //將 Xml 上標示新增的學生編號。
                    Arguments[Consts.StudentID] = newid;
                }

                //將最新狀況寫回資料庫中。
                Record.ModifiedContent = XmlData.ToString();
                Record.RefStudentID = Arguments[Consts.StudentID] + "";
                Record.Save();


                // 新增至班級學生變更                
                UDT_ClassSpecial StudSpec = UDTTransfer.AddClassSpecStudent(SRecord.ID, "", SRecord.RefClassID, "", txtClass.Text, txtClass.Text, "", "");

                // 傳送至局端
                string errMsg = Utility.SendData("自動轉入", SRecord.IDNumber, SRecord.StudentNumber, SRecord.Name, cboGender.Text, txtOClass.Text, cboSeatNo.Text, txtClass.Text, "", "", SRecord.ID, SRecord.RefClassID, StudSpec.ClassComment);
                if (errMsg != "")
                    FISCA.Presentation.Controls.MsgBox.Show(errMsg);

                //設定所有 Processor 的學生編號。
                foreach (TransferProcessor tp in TransferProcessor.Processors)
                    tp.SetStudentId(Arguments[Consts.StudentID] + "");

                //註冊一個事件引發模組
                EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentTransferStudentBriefItem");
                eh(this, EventArgs.Empty);

                return ContinueDirection.Next;
            }
            catch (Exception ex)
            {
                RTOut.WriteError(ex);
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void UpdateData(StudentRecord srecord)
        {
            srecord.Name = txtName.Text;
            srecord.IDNumber = txtIDNumber.Text;
            srecord.Birthday = dtBirthday.Value;
            srecord.Gender = cboGender.Text;
            srecord.StudentNumber = cboStudentNumber.Text;

            int seatNo;
            if (int.TryParse(cboSeatNo.Text, out seatNo))
                srecord.SeatNo = seatNo;
            else
                srecord.SeatNo = null;

            if (_ClassID == null)
                foreach (ClassRecord cr in Class.SelectAll())
                {
                    if (cr.Name == txtClass.Text)
                    {
                        _ClassID = cr.ID;
                        break;
                    }
                }

            srecord.RefClassID = _ClassID;
            (Arguments[Consts.TransferInGridItem] as StatusForm.TransferInItem).ClassName = txtClass.Text;
        }

        private bool IsIDNumberExists(string selfPrimaryKey, string idNumber)
        {
            string cmd = string.Format("select id from student where id_number='{0}' and id not in('{1}') and status='1'", idNumber, selfPrimaryKey);
            DataTable dt = Query.Select(cmd);
            return dt.Rows.Count > 0;
        }

        private bool IsStudentNumberExists(string selfPrimaryKey, string idNumber)
        {
            string cmd = string.Format("select id from student where student_number='{0}' and id not in('{1}') and status='1'", idNumber, selfPrimaryKey);
            DataTable dt = Query.Select(cmd);
            return dt.Rows.Count > 0;
        }

        private bool SuspenSelectedValueChanged = false;


        #region 新竹市專用 WS Call
        private void CallTransferInWS()
        {
            TransferInRecord record = Record;

            try
            {
                string contract = "StudentTransferHsinchuSpecial";
                string service = "SyncTransferIn";

                SecurityToken token = (DSAServices.DefaultDataSource.SecurityToken as SessionToken).OriginToken;
                Connection conn = DSAServices.DefaultDataSource.AsContract(contract, token);
                XElement econtent = XElement.Parse(record.ModifiedContent);

                FISCA.DSAClient.XmlHelper req = new FISCA.DSAClient.XmlHelper("<Request/>");
                req.AddElement(".", "TargetSchool", GetSchoolCode());
                req.AddElement(".", "Writer", string.Format("{0}:{1}", DSAServices.AccessPoint, DSAServices.UserAccount));
                req.AddElement(".", "StudentID", econtent.XPathSelectElement("Student").ElementText("IDNumber"));
                req.AddElement(".", "StudentNumber", SRecord.StudentNumber);
                req.AddElement(".", "Grade", SRecord.Class.GradeYear + "");
                req.AddElement(".", "ClassName", SRecord.Class.Name.Substring(1));

                RTOut.WriteLine(req.XmlString);
                RTOut.WriteLine(conn.SendRequest(service, new Envelope(req)).XmlString);
            }
            catch (Exception ex)
            {
                RTOut.WriteError(ex);
            }
        }

        private static string FormatBirthday(XElement econtent)
        {
            string strBirthday = econtent.XPathSelectElement("Student").ElementText("Birthdate");
            string formatedBirthday = "";
            DateTime birthday;

            if (DateTime.TryParse(strBirthday, out birthday))
                formatedBirthday = string.Format("{0:0000}{1:00}{2:00}", birthday.Year, birthday.Month, birthday.Day);

            return formatedBirthday;
        }

        private static string GetSchoolCode()
        {
            string code = "000000";

            XElement schools = XElement.Parse(Properties.Resources.jh);

            var result = from school in schools.Elements()
                         where school.AttributeText("DSNS") == DSAServices.AccessPoint
                         select school.AttributeText("Code");

            foreach (string school in result)
                code = school;


            return code;
        }
        #endregion

        private void cboGradeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboGradeYear.Text))
            {
                // 當學生沒有原班級才處理，有原班回到原班
                if (CRecord == null)
                {
                    _ClassID = "";
                    txtClass.Text = "";
                    Dictionary<string, string> grClassDict = Utility.GetClassNameFirst(cboGradeYear.Text);

                    foreach (string name in grClassDict.Keys)
                    {
                        // 取得班級名稱
                        txtClass.Text = name;
                        _ClassID = grClassDict[name];
                        break;
                    }
                }
                cboSeatNo.Text = "";
                // 取得座號
                FillEmptySeatNos(SRecord != null ? SRecord.SeatNo : null, _ClassID);

                cboStudentNumber.Text = "";
                // 處理學號
                if (SRecord != null)
                    FillStudentNumberLast(SRecord.StudentNumber);
                else
                    FillStudentNumberLast(string.Empty);
            }
        }

        //選擇號碼後，帶出學號
        private void cboSeatNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 處理學號
            if (SRecord != null)
                FillStudentNumberLast(SRecord.StudentNumber);
            else
                FillStudentNumberLast(string.Empty);
        }
    }
}