using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Category : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Category()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadCategory();
        }
        public void LoadCategory()
        {
            try
            {
                int i = 0;
                dgvCategory.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblCategory ORDER BY id DESC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
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
                dbcon.Error("Category/LoadCategory", ex.Message, linenumber);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModule categoryModuleForm = new CategoryModule(this);
            categoryModuleForm.ShowDialog();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvCategory.Columns[e.ColumnIndex].Name;
                if (colName == "Delete")
                {
                    try
                    {

                        if (MessageBox.Show("Are you sure you want to delete this category!", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cn.Open();
                            cm = new SqlCommand("DELETE FROM tblCategory WHERE id='" + dgvCategory[1, e.RowIndex].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Category has been sucessfully deleted.", "Point of Sales");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (colName == "Edit")
                {
                    CategoryModule categoryModule = new CategoryModule(this);
                    categoryModule.lblId.Text = dgvCategory[1, e.RowIndex].Value.ToString();
                    categoryModule.txtCategory.Text = dgvCategory[2, e.RowIndex].Value.ToString();
                    categoryModule.btnSave.Enabled = false;
                    categoryModule.btnUpdate.Enabled = true;
                    categoryModule.ShowDialog();
                }
                LoadCategory();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Category/dgvCategory_CellContentClick", ex.Message, linenumber);
            }
        }
    }
}
