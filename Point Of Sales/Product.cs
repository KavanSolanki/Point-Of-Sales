using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Product : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule(this);
            productModule.ShowDialog();
        }
        public void LoadProduct()
        {
            try
            {
                int i = 0;
                dgvProduct.Rows.Clear();
                cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price FROM tblProduct AS p INNER JOIN tblBrand AS b ON b.id = p.bid INNER JOIN tblCategory AS c on c.id = p.cid WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), double.Parse(dr[5].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Product/LoadProduct", ex.Message, linenumber);
            }
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                try
                {
                    ProductModule productModule = new ProductModule(this);
                    productModule.txtPcode.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                    productModule.txtBarcode.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                    productModule.txtPdesc.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                    productModule.cboBrand.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                    productModule.cboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                    productModule.cboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                    productModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                    //productModule.UDReOrder.Value = int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString());
                    productModule.txtPcode.Enabled = false;
                    productModule.btnSave.Enabled = false;
                    productModule.btnUpdate.Enabled = true;
                    productModule.ShowDialog();
                }
                catch (Exception ex)
                {
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    var frame = st.GetFrame(st.FrameCount - 1);
                    var linenumber = frame.GetFileLineNumber();
                    dbcon.Error("Cashier/dgvProduct_CellContentClick", ex.Message, linenumber);
                }
            }
            else if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete this product!", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("DELETE FROM tblProduct WHERE pcode='" + dgvProduct[1, e.RowIndex].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been sucessfully deleted.", "Point of Sales");
                    }
                }
                catch (Exception ex)
                {
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    var frame = st.GetFrame(st.FrameCount - 1);
                    var linenumber = frame.GetFileLineNumber();
                    dbcon.Error("Product/dgvProduct_CellContentClick", ex.Message, linenumber);
                }
            }
            LoadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
