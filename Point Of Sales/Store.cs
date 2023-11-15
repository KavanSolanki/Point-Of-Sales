using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Store : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        bool havestoreinfo = false;
        public Store()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadStore();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save store details?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    if (havestoreinfo)
                    {
                        dbcon.ExecuteQuery("UPDATE tblStore SET StoreName = '" + txtStName.Text + "', StoreAddress= '" + txtAddress.Text + "',GSTNO='"+ txtGstNo.Text+ "'");
                    }
                    else
                    {
                        dbcon.ExecuteQuery("INSERT INTO tblStore (StoreName,StoreAddress,GSTNO) VALUES ('" + txtStName.Text + "','" + txtAddress.Text + "','" + txtGstNo.Text + "')");
                    }
                    MessageBox.Show("Store detail has been successfully saved!", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Store/btnSave_Click", ex.Message, linenumber);
            }
        }
        public void LoadStore()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblStore", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    havestoreinfo = true;
                    txtStName.Text = dr["StoreName"].ToString();
                    txtAddress.Text = dr["StoreAddress"].ToString();
                    txtGstNo.Text = dr["GSTNO"].ToString();
                }
                else
                {
                    txtStName.Clear();
                    txtAddress.Clear();
                    txtGstNo.Clear();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Store/LoadStore", ex.Message, linenumber);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Store_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
