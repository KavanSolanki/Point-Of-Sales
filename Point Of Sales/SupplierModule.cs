using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class SupplierModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Supplier supplier;
        public SupplierModule(Supplier sp)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            this.supplier = sp;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Clear()
        {
            txtSupplier.Clear();
            txtAddress.Clear();
            txtConPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtFaxNo.Clear();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtSupplier.Focus();

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplier.Text) && string.IsNullOrEmpty(txtAddress.Text) && string.IsNullOrEmpty(txtConPerson.Text) && string.IsNullOrEmpty(txtPhone.Text) && string.IsNullOrEmpty(txtFaxNo.Text) && string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Please Enter  all Data", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtSupplier.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtAddress.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtConPerson.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtFaxNo.Text, @"[0-9][0-9]") && !Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$"))
                {
                    MessageBox.Show("This is not valid!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this supplier?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblSupplier (supplier ,address ,contactperson ,phone ,email ,fax) VALUES (@supplier,@address ,@contactperson,@phone,@email,@fax)", cn);
                        cm.Parameters.AddWithValue("@supplier", txtSupplier.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@contactperson", txtConPerson.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@email", txtEmail.Text);
                        cm.Parameters.AddWithValue("@fax", txtEmail.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Supplier has been sucessfully saved.", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        supplier.LoadSupplier();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("SupplierModule/btnSave_Click", ex.Message, linenumber);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this supplier?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblSupplier SET supplier=@supplier,address=@address,contactperson=@contactperson,phone=@phone,email=@email,fax=@fax WHERE id='" + lblId.Text + "' ", cn);
                    cm.Parameters.AddWithValue("@supplier", txtSupplier.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtConPerson.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmail.Text);
                    cm.Parameters.AddWithValue("@fax", txtFaxNo.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Supplier has been sucessfully Updated.", "Point of Sales");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {   
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("SupplierModule/btnUpdate_Click", ex.Message, linenumber);
            }
        }
    }
}
