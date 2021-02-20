using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace EncryptStringTest
{
    public partial class frmStringEncryption : Form
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["FomemaDBConnection"].ConnectionString;
        public frmStringEncryption()
        {
            InitializeComponent();
        }

        private void butEncrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxString.Text))
            {
                try
                {
                    textBoxEncrypted.Text = Encrypt.EncryptString(textBoxString.Text);
                    textBoxString.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void butDecrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxEncrypted.Text))
            {
                try
                {
                    textBoxString.Text = Encrypt.DecryptString(textBoxEncrypted.Text);
                    textBoxEncrypted.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable("UniqueIDListofNumber");
            dataTable.Columns.Add("SrNo", typeof(int));
            dataTable.Columns["SrNo"].AutoIncrement = true;
            dataTable.Columns["SrNo"].AutoIncrementSeed = 1;
            dataTable.Columns["SrNo"].AutoIncrementStep = 1;

            dataTable.Columns.Add("Original Number", typeof(string));
            dataTable.Columns.Add("Encrypted Number", typeof(string));
            dataTable.Columns.Add("Url Link", typeof(string));

            for (long i = Convert.ToInt64(txtStartNo.Text); i < Convert.ToInt64(txtStartNo.Text) + Convert.ToInt64(txtCount.Text); i++)
            {
                DataRow dr = dataTable.NewRow();
                dr["Original Number"] = i.ToString();
                string encString = Encrypt.EncryptString(i.ToString());
                dr["Encrypted Number"] = encString;
                dr["Url Link"] = "securebiometrics.live/Candidate/CandidateMedicalCard?id=" + HttpUtility.UrlEncode(encString);
                dataTable.Rows.Add(dr);
            }

            //securebiometrics.live/Candidate/CandidateMedicalCard?id=111111111

            //----------------- Insert into Database

            if (dataTable.Rows.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "FomemaMedicalCardList";
                        sqlBulkCopy.ColumnMappings.Add("Original Number", "CardNumber");
                        sqlBulkCopy.ColumnMappings.Add("Encrypted Number", "CardNumberEncrypt");
                        sqlBulkCopy.ColumnMappings.Add("Url Link", "CardUrl");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dataTable);
                        con.Close();
                    }
                }
            }

            //------------------


            txtPath.Text = Path.Combine(Application.StartupPath, "ExcelFile_" + DateTime.Now.ToString("ddMMyyyHHmmssfffff") + ".xlsx");

            FileInfo newFile = new FileInfo(txtPath.Text);

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                ws.Cells.AutoFitColumns();
                pck.Save();
            }
            btnRefreshStart_Click(null, null);
            MessageBox.Show("File Exported Successfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPath.Text))
            {
                Process.Start(txtPath.Text);
            }
            else
            {
                MessageBox.Show("File Export First and Try Again.");
            }
        }

        private void frmStringEncryption_Load(object sender, EventArgs e)
        {
            btnRefreshStart_Click(null, null);
        }

        private void btnRefreshStart_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select MAX(CardNumber) as CardNumber from FomemaMedicalCardList;", connStr))
                {
                    DataTable dtMaxNumber = new DataTable();
                    sda.Fill(dtMaxNumber);
                    if (dtMaxNumber.Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(dtMaxNumber.Rows[0]["CardNumber"])))
                    {
                        txtStartNo.Text = Convert.ToString(Convert.ToInt64(dtMaxNumber.Rows[0]["CardNumber"]) + 1);
                    }
                    else
                    {
                        txtStartNo.Text = "111111111";
                    }
                }
            }
            txtCount.Text = "100";
        }
    }
}
