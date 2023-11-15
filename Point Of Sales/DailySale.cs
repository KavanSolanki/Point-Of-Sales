using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class DailySale : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public string solduser;
        MainForm main;
        public DailySale(MainForm mn)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            main = mn;
            LoadCashier();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCashier()
        {
            try
            {
                cboCashier.Items.Clear();
                cboCashier.Items.Add("All Cashier");
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblUser WHERE role LIKE 'Cashier'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboCashier.Items.Add(dr["username"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("DailySale/LoadCashier", ex.Message, linenumber);
            }
        }
        public void LoadSold()
        {
            try
            {
                int i = 0;
                double total = 0;
                dgvSold.Rows.Clear();
                cn.Open();
                if (cboCashier.Text == "All Cashier")
                {
                    cm = new SqlCommand("SELECT C.ID, C.TRASNNO, C.PCODE, P.PDESC, C.PRICE, C.QTY, C.DISC, C.TOTAL FROM TBLCART AS C INNER JOIN TBLPRODUCT AS P ON C.PCODE = P.PCODE WHERE STATUS LIKE 'SOLD' AND CONVERT(VARCHAR(20),SDATE,103) BETWEEN '" + dtFrom.Value + "' AND '" + dtTo.Value + "'", cn);
                }
                else
                {
                    cm = new SqlCommand("SELECT C.ID, C.TRASNNO, C.PCODE, P.PDESC, C.PRICE, C.QTY, C.DISC, C.TOTAL FROM TBLCART AS C INNER JOIN TBLPRODUCT AS P ON C.PCODE = P.PCODE WHERE STATUS LIKE 'SOLD' AND CONVERT(VARCHAR(20),SDATE,103) BETWEEN '" + dtFrom.Value.ToString() + "'  AND  '" + dtTo.Value.ToString() + "' AND CASHIER LIKE '" + cboCashier.Text + "'", cn);
                }
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += double.Parse(dr["total"].ToString());
                    dgvSold.Rows.Add(i, dr["ID"].ToString(), dr["TRASNNO"].ToString(), dr["PCODE"].ToString(), dr["PDESC"].ToString(), dr["PRICE"].ToString(), dr["QTY"].ToString(), dr["DISC"].ToString(), dr["TOTAL"].ToString());
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                dr.Close();
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("DailySale/LoadSold", ex.Message, linenumber);
            }
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadSold();
        }

        private void DailySale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
