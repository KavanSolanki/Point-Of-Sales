using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Brand : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadBrand();
        }
        public void LoadBrand()
        {
            try
            {
                int i = 0;
                dgvBrand.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblBrand ORDER BY id DESC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvBrand.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Brand/LoadBrand", ex.Message, linenumber);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BrandModule brandModuleForm = new BrandModule(this);
            brandModuleForm.ShowDialog();
        }

        private void dgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvBrand.Columns[e.ColumnIndex].Name;
                if (colName == "Delete")
                {
                    try
                    {
                        if (MessageBox.Show("Are you sure you want to delete this brand!", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cn.Open();
                            cm = new SqlCommand("DELETE FROM tblBrand WHERE id='" + dgvBrand[1, e.RowIndex].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Brand has been sucessfully deleted.", "Point of Sales");
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new System.Diagnostics.StackTrace(ex, true);
                        var frame = st.GetFrame(st.FrameCount - 1);
                        var linenumber = frame.GetFileLineNumber();
                        dbcon.Error("Brand/dgvBrand_CellContentClick", ex.Message, linenumber);
                    }
                }
                else if (colName == "Edit")
                {
                    BrandModule brandModule = new BrandModule(this);
                    brandModule.lblId.Text = dgvBrand[1, e.RowIndex].Value.ToString();
                    brandModule.txtBrand.Text = dgvBrand[2, e.RowIndex].Value.ToString();
                    brandModule.btnSave.Enabled = false;
                    brandModule.btnUpdate.Enabled = true;
                    brandModule.ShowDialog();
                }
                LoadBrand();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Brand/dgvBrand_CellContentClick", ex.Message, linenumber);
            }
        }
    }
}
