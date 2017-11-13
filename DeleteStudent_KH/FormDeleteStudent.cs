using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using JHSchool.Data;
using K12.Presentation;
using FISCA.Presentation;

namespace DeleteStudent_KH
{
    public partial class FormDeleteStudent : BaseForm
    {

        private JHStudentRecord studRec;

        public FormDeleteStudent()
        {
            List<string> SelectedList = new List<string>();

            SelectedList =  K12.Presentation.NLDPanels.Student.SelectedSource;

            studRec = JHSchool.Data.JHStudent.SelectByID(SelectedList[0]);
            
            InitializeComponent();
        }

        private void FormDeleteStudent_Load(object sender, EventArgs e)
        {
            // 2017/11/9 穎驊註解，因應高雄小組項目 [06-01][03] 修改學生狀態沒有上傳局端 而新增的Code
            if (studRec != null)
            {
                try
                {
                    bool chkSendSpec = false;

                    string StudStatus = studRec.StatusStr;
                    //string NewStudStatus = ((StatusItem)button.Tag).Text;

                    string NewStudStatus = "刪除";

                    // 2017/11/9 穎驊註解 從畢業或離校、休學、刪除，變成一般 叫做"特殊狀態更動"，而從一般狀態到刪除是"一般狀態更動" 以本例而言，都是一般狀態更動
                    if ((StudStatus == "畢業或離校" || StudStatus == "休學" || StudStatus == "刪除") && NewStudStatus == "一般")
                        chkSendSpec = true;

                    chkSendSpec = false;

                    StudentChangeStatus_KH.sendMessage smg = new StudentChangeStatus_KH.sendMessage(studRec.Name, StudStatus, NewStudStatus, chkSendSpec);

                    if (smg.ShowDialog() == DialogResult.Yes)
                    {
                        string log = "學生「" + studRec.Name + "」狀態已";
                        log += "由「" + studRec.StatusStr + "」變更為「刪除」";

                        studRec.Status = K12.Data.StudentRecord.StudentStatus.刪除;

                        // 檢查同狀態要身分證或學號相同時，無法變更
                        List<string> checkIDNumber = new List<string>();
                        List<string> checkSnum = new List<string>();

                        foreach (K12.Data.StudentRecord sr in K12.Data.Student.SelectAll())
                        {
                            if (sr.Status == studRec.Status)
                            {
                                if (!string.IsNullOrEmpty(sr.StudentNumber))
                                    checkSnum.Add(sr.StudentNumber.Trim());
                                if (!string.IsNullOrEmpty(sr.IDNumber))
                                    checkIDNumber.Add(sr.IDNumber.Trim());
                            }
                        }

                        if (checkSnum.Contains(studRec.StudentNumber.Trim()))
                        {
                            MsgBox.Show("在" + studRec.Status.ToString() + "狀態學號有重複無法變更.");
                            return;
                        }

                        if (checkIDNumber.Contains(studRec.IDNumber.Trim()))
                        {
                            MsgBox.Show("在" + studRec.Status.ToString() + "狀態身分證號有重複無法變更.");
                            return;
                        }

                        // 傳送到局端
                        string action = "一般狀態變更";

                        // 特殊狀態
                        if (chkSendSpec)
                            action = "特殊狀態變更";

                        string ClassName = "";
                        if (studRec.Class != null)
                            ClassName = studRec.Class.Name;
                        StudentChangeStatus_KH.Utility.SendData(action, ClassName, studRec.Name, studRec.StudentNumber, studRec.IDNumber, StudStatus, NewStudStatus, smg.GetMessage());

                        K12.Data.Student.Update(studRec);
                        FISCA.LogAgent.ApplicationLog.Log("學生狀態", "變更", "student", studRec.ID, log);

                        //註冊一個事件引發模組
                        EventHandler eh = FISCA.InteractionService.PublishEvent("KH_StudentChangeStatus");
                        eh(this, EventArgs.Empty);
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("目前無法移到刪除");
                }
                catch
                {
                    MotherForm.SetStatusBarMessage("變更狀態失敗，可能發生原因為學號或身分證號在刪除" + "學生中已經存在，請檢查學生資料。");
                    return;
                }
            }


            // 這個form 僅是用來達到 刪除學生 的背景， 在上面程式碼都執行完後，會需要自動刪除。
            this.Close();
        }
    }
}
