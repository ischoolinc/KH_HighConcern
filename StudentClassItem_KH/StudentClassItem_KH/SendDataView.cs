using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StudentClassItem_KH
{
    public partial class SendDataView : FISCA.Presentation.Controls.BaseForm
    {
        public SendDataView()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void SendDataView_Load(object sender, EventArgs e)
        {

        }

        private void LoadDefaultData()
        { 
            dtEndDate.Value=DateTime.Now;
            dtBeginDate.Value = DateTime.Now.AddDays(-7);
        }


        private bool CheckData()
        {
            bool pass = true;

            return pass;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            if (CheckData())
            {
                
               XElement rspElm=Utility.QuerySendData(dtBeginDate.Value, dtEndDate.Value);
               if (rspElm != null)
               {
                   if (rspElm.Element("Envelope") != null)
                       if(rspElm.Element("Envelope").Element("Body") !=null)
                           if (rspElm.Element("Envelope").Element("Body").Element("Response") != null)
                           {
                               foreach (XElement elm in rspElm.Element("Envelope").Element("Body").Element("Response").Elements("SchoolLog"))
                               { 
                               
                               }
                           
                           }
               
               }

            }
            btnQuery.Enabled = true;
        }
    }
}
