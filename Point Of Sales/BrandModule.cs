using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class BrandModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Brand brand;
        public BrandModule(Brand brand)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            this.brand = brand;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Insert Brand
            try
            {
                if (string.IsNullOrEmpty(txtBrand.Text))
                {
                    MessageBox.Show("Please Enter Brand Name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtBrand.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show(txtBrand.Text + " is not allowed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this brand?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblBrand(brand)VALUES(@brand)", cn);
                        cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Brand has been sucessfully saved.", "Point of Sales");
                        Clear();
                        brand.LoadBrand();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("BrandModule/Brand-Insert", ex.Message, linenumber);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtBrand.Clear();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtBrand.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBrand.Text))
                {
                    MessageBox.Show("Please Enter Brand Name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtBrand.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show(txtBrand.Text + " is not allowed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to update this brand?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblBrand SET brand=@brand WHERE id='" + lblId.Text + "' ", cn);
                        cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Brand has been sucessfully Updated.", "Point of Sales");
                        Clear();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("BrandModule/Brand-Insert", ex.Message, linenumber);
            }
        }
    }
}
