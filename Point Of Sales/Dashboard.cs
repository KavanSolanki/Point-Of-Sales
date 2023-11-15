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
    public partial class Dashboard : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadChart();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            lblDalySale.Text = dbcon.ExtractData("SELECT ISNULL(SUM(total),0) AS total FROM tblCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + sdate + "' AND '" + sdate + "'").ToString("#,##0.00");
            lblTotalProduct.Text = dbcon.ExtractData("SELECT COUNT(*) FROM tblProduct").ToString("#,##0");
            lblStockOnHand.Text = dbcon.ExtractData("SELECT COUNT(*) FROM tblCategory").ToString("#,##0");
            LoadChart();

        }
        public void LoadChart()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT DATENAME(MONTH,sdate) AS [Month],SUM(total) AS [Sales] FROM tblCart GROUP BY DATENAME(MONTH,sdate) ORDER BY DATENAME(MONTH,sdate)", cn);
                DataSet ds = new DataSet();
                SqlDataAdapter adapt = new SqlDataAdapter(cm);
                adapt.Fill(ds);
                chart1.DataSource = ds;
                chart1.Series["Sales"].XValueMember = "Month";
                chart1.Series["Sales"].YValueMembers = "Sales";
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Dashboard/LoadChart", ex.Message, linenumber);
            }
        }
    }
}
