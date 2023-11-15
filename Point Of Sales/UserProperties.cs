using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class UserProperties : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        UserAccount account;
        public string username;
        public UserProperties(UserAccount user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            account = user;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure you want to change this account properties?", "Change Properties", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblUser SET name=@name, role=@role, isactive=@isactive WHERE username='" + username + "'", cn);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@role", cbRole.Text);
                    cm.Parameters.AddWithValue("@isactive", cbActivate.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Account properties has been successfully changed!", "Update Properties", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    account.LoadUser();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("UserProperties/btnApply_Click", ex.Message, linenumber);
            }
        }
    }
}
