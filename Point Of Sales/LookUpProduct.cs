using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class LookUpProduct : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        Cashier cashier;
        public LookUpProduct(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
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
                cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tblProduct AS p INNER JOIN tblBrand AS b ON b.id = p.bid INNER JOIN tblCategory AS c on c.id = p.cid WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProduct.Rows.Add(i,
                        dr[0].ToString(),
                        dr[1].ToString(),
                        dr[2].ToString(),
                        dr[3].ToString(),
                        dr[4].ToString(),
                        dr[5].ToString(),
                        dr[6].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("LookUpProduct/LoadProduct", ex.Message, linenumber);
            }
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvProduct.Columns[e.ColumnIndex].Name;
                if (colName == "Select")
                {
                    Qty qty = new Qty(cashier);
                    qty.ProductDetails(dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString()), cashier.lblTranNo.Text, int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString()));
                    qty.ShowDialog();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("LookUpProduct/dgvProduct_CellContentClick", ex.Message, linenumber);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void LookUpProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}