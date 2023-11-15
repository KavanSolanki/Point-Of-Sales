using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class CategoryModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Category ct;
        public CategoryModule(Category ct)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            this.ct = ct;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Clear()
        {
            txtCategory.Clear();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtCategory.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Insert Brand
            try
            {
                if (string.IsNullOrEmpty(txtCategory.Text))
                {
                    MessageBox.Show("Please Enter Category Name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtCategory.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show(txtCategory.Text + " is not allowed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this category?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblCategory(category)VALUES(@category)", cn);
                        cm.Parameters.AddWithValue("@category", txtCategory.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Category has been sucessfully saved.", "Point of Sales");
                        Clear();
                        ct.LoadCategory();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("CategoryModule/btnSave_Click", ex.Message, linenumber);
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
                if (string.IsNullOrEmpty(txtCategory.Text))
                {
                    MessageBox.Show("Please Enter Category Name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtCategory.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show(txtCategory.Text + " is not allowed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to update this category?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblCategory SET category=@category WHERE id='" + lblId.Text + "' ", cn);
                        cm.Parameters.AddWithValue("@category", txtCategory.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Category has been sucessfully Updated.", "Point of Sales");
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
                dbcon.Error("CategoryModule/btnUpdate_Click", ex.Message, linenumber);
            }
        }
        private void CategoryModule_Load(object sender, EventArgs e)
        {

        }
    }
}
