using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Supplier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Supplier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadSupplier();
        }
        public void LoadSupplier()
        {
            try
            {
                int i = 0;
                dgvSupplier.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblSupplier ORDER BY id DESC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvSupplier.Rows.Add(i, dr["id"].ToString(), dr["supplier"].ToString(), dr["address"].ToString(), dr["contactperson"].ToString(), dr["phone"].ToString(), dr["email"].ToString(), dr["fax"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Supplier/LoadSupplier", ex.Message, linenumber);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierModule supplierModule = new SupplierModule(this);
            supplierModule.ShowDialog();
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSupplier.Columns[e.ColumnIndex].Name;
            try
            {
                if (colName == "Edit")
                {
                    SupplierModule supplierModule = new SupplierModule(this);
                    supplierModule.lblId.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                    supplierModule.txtSupplier.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                    supplierModule.txtAddress.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                    supplierModule.txtConPerson.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                    supplierModule.txtPhone.Text = dgvSupplier.Rows[e.RowIndex].Cells[5].Value.ToString();
                    supplierModule.txtEmail.Text = dgvSupplier.Rows[e.RowIndex].Cells[6].Value.ToString();
                    supplierModule.txtFaxNo.Text = dgvSupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

                    supplierModule.btnSave.Enabled = false;
                    supplierModule.btnUpdate.Enabled = true;
                    supplierModule.ShowDialog();
                }
                else if (colName == "Delete")
                {
                    try
                    {

                        if (MessageBox.Show("Are you sure you want to delete this Supplier!", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cn.Open();
                            cm = new SqlCommand("DELETE FROM tblSupplier WHERE id='" + dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Supplier has been sucessfully deleted.", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new System.Diagnostics.StackTrace(ex, true);
                        var frame = st.GetFrame(st.FrameCount - 1);
                        var linenumber = frame.GetFileLineNumber();
                        dbcon.Error("Supplier/dgvSupplier_CellContentClick/colName->Delete", ex.Message, linenumber);
                    }
                }
                LoadSupplier();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Supplier/dgvSupplier_CellContentClick", ex.Message, linenumber);
            }
        }
    }
}