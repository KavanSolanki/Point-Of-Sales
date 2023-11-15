using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class ProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        StockIn stockIn;
        public ProductStockIn(StockIn stk)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            stockIn = stk;
            LoadProduct();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadProduct()
        {
            try
            {
                int i = 0;
                dgvProduct.Rows.Clear();
                cm = new SqlCommand("SELECT pcode, pdesc, qty FROM tblProduct WHERE pdesc LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
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
                dbcon.Error("ProductStockIn/LoadProduct", ex.Message, linenumber);
            }

        }
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (stockIn.txtStockInBy.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Stock in By Name", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    stockIn.txtStockInBy.Focus();
                    this.Dispose();
                }
                if (MessageBox.Show("Add this Item?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblStockIn(refno,pcode,sdate,stockinby,supplierid)VALUES(@refno,@pcode,@sdate,@stockinby,@supplierid)", cn);
                        cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                        cm.Parameters.AddWithValue("@pcode", dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@sdate", stockIn.dtStockIn.Value);
                        cm.Parameters.AddWithValue("@stockinby", stockIn.txtStockInBy.Text);
                        cm.Parameters.AddWithValue("@supplierid", stockIn.lblId.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        stockIn.LoadStockIn();
                        MessageBox.Show("Sucessfully added", "Point of sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        var st = new System.Diagnostics.StackTrace(ex, true);
                        var frame = st.GetFrame(st.FrameCount - 1);
                        var linenumber = frame.GetFileLineNumber();
                        dbcon.Error("ProductStockIn/dgvProduct_CellContentClick", ex.Message, linenumber);
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
