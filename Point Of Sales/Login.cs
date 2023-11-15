using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Login : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public string _pass = "";
        public bool _isActive;
        public Login()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            txtName.Focus();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblCopy.Text = "Copyright © " + System.DateTime.Now.ToString("yyyy") + " Kavan J. Solanki All Rights Reserved";
        }

        private void picEyeOpen_Click(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '\0';
            txtPass.UseSystemPasswordChar = false;
            picEyeOpen.Visible = false;
            picEyeClose.Visible = true;
        }

        private void picEyeClose_Click(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '●';
            txtPass.UseSystemPasswordChar = true;
            picEyeOpen.Visible = true;
            picEyeClose.Visible = false;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _username = "", _name = "", _role = "";
            try
            {
                bool found;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblUser WHERE username=@username AND password=@password", cn);
                cm.Parameters.AddWithValue("@username", txtName.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _name = dr["name"].ToString();
                    _role = dr["role"].ToString();
                    _pass = dr["password"].ToString();
                    _isActive = bool.Parse(dr["isactive"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();
                if (found)
                {
                    if (!_isActive)
                    {
                        MessageBox.Show("Account is inactive. Unable to login", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name, "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Clear();
                        txtPass.Clear();
                        this.Hide();
                        Cashier cashier = new Cashier();
                        cashier.lblUsername.Text = _username;
                        cashier.lblname.Text = _name + " | " + _role;
                        cashier.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + _name, "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Clear();
                        txtPass.Clear();
                        this.Hide();
                        MainForm mainForm = new MainForm();
                        mainForm.lblName.Text = _name;
                        mainForm.lblUsername.Text = _username;
                        mainForm._pass = _pass;
                        mainForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username And Password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Clear();
                    txtPass.Clear();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Login/btnLogin_Click", ex.Message, linenumber);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
