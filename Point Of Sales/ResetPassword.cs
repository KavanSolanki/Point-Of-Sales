using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class ResetPassword : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        UserAccount UserAccount;
        public ResetPassword(UserAccount user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            UserAccount = user;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtNpass.Text != txtResPass.Text)
            {
                MessageBox.Show("The password you typed do not match. Type the password for this account in both text boxes.", "Add User Wizard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Reset password?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dbcon.ExecuteQuery("UPDATE tblUser SET password = '" + txtNpass.Text + "'WHERE username = '" + UserAccount.username + "'");
                    MessageBox.Show("Password has been successfully reset", "Reset Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ResetPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
