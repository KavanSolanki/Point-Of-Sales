using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Recept : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        string store;
        string address;
        string GstNo;
        Cashier cashier;
        public Recept(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
            LoadStore();
        }
        public void LoadStore()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblStore", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    store = dr["StoreName"].ToString();
                    address = dr["StoreAddress"].ToString();
                    GstNo = dr["GSTNO"].ToString();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Recept/LoadStore", ex.Message, linenumber);
            }
        }
        private void Recept_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
        public void LoadRecept(string pcash, string pchange)
        {
            ReportDataSource rptDataSourece;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptRecept.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();
                cn.Open();
                da.SelectCommand = new SqlCommand("SELECT C.id,C.trasnno,C.price,C.qty,C.disc,C.total,C.sdate,C.status,P.pdesc FROM tblCart AS C INNER JOIN tblProduct AS P ON p.pcode = C.pcode WHERE C.trasnno LIKE '"+cashier.lblTranNo.Text+"'", cn);
                da.Fill(ds.Tables["dtRecept"]);
                cn.Close();

                ReportParameter pVatable = new ReportParameter("pVatable", cashier.lblVatable.Text);
                ReportParameter pVat = new ReportParameter("pVat", cashier.lblVat.Text);
                ReportParameter pDiscount = new ReportParameter("pDiscount", cashier.lblDiscount.Text);
                ReportParameter pTotal = new ReportParameter("pTotal", cashier.lblDisplayTotal.Text);
                ReportParameter pCash = new ReportParameter("pCash", pcash);
                ReportParameter pChange = new ReportParameter("pChange", pchange);
                ReportParameter pStore = new ReportParameter("pStore", store);
                ReportParameter pAddress = new ReportParameter("pAddress", address);
                ReportParameter pGST = new ReportParameter("pGST", GstNo);
                ReportParameter pTransaction = new ReportParameter("pTransaction", cashier.lblTranNo.Text);
                ReportParameter pCashier = new ReportParameter("pCashier", cashier.lblUsername.Text);

                reportViewer1.LocalReport.SetParameters(pVatable);
                reportViewer1.LocalReport.SetParameters(pVat);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pGST);

                rptDataSourece = new ReportDataSource("DataSet1", ds.Tables["dtRecept"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSourece);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Recept/LoadRecept", ex.Message, linenumber);
            }
        }
    }
}
