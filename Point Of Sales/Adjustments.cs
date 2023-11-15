using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Adjustments : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        MainForm main;
        int _qty;
        public Adjustments(MainForm mn)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            main = mn;
            ReferenceNo();
            LoadStock();
            lblUsername.Text = main.lblUsername.Text;
        }
        public void ReferenceNo()
        {
            Random random = new Random();
            lblRefNo.Text = random.Next().ToString();
        }
        public void LoadStock()
        {
            try
            {
                int i = 0;
                dgvAdjustment.Rows.Clear();
                cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tblProduct AS p INNER JOIN tblBrand AS b ON b.id = p.bid INNER JOIN tblCategory AS c on c.id = p.cid WHERE CONCAT(p.pdesc, b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvAdjustment.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Adjustments/LoadStock", ex.Message, linenumber);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStock();
        }
        public void Clear()
        {
            lblDesc.Text = "";
            lblPcode.Text = "";
            txtQty.Clear();
            txtRemark.Clear();
            cbAction.Text = "";
            ReferenceNo();
        }

        private void dgvAdjustment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvAdjustment.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                lblPcode.Text = dgvAdjustment.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblDesc.Text = dgvAdjustment.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dgvAdjustment.Rows[e.RowIndex].Cells[4].Value.ToString() + " " + dgvAdjustment.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dgvAdjustment.Rows[e.RowIndex].Cells[7].Value.ToString());
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAction.Text == "")
                {
                    MessageBox.Show("Please select action for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbAction.Focus();
                    return;
                }
                if (txtQty.Text == "")
                {
                    MessageBox.Show("Please input quantity  for add or reduce.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }
                if (txtRemark.Text == "")
                {
                    MessageBox.Show("Need reason for stock adjustment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRemark.Focus();
                    return;
                }
                if (int.Parse(txtQty.Text) > _qty)
                {
                    MessageBox.Show("Stock on hand quantity should be greater than adjustment quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbAction.Text == "Remove From Inventory")
                {
                    dbcon.ExecuteQuery("UPDATE tblProduct SET qty = (qty - " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }
                else if (cbAction.Text == "Add To Inventory")
                {
                    dbcon.ExecuteQuery("UPDATE tblProduct SET qty = (qty + " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + lblPcode.Text + "'");
                }
                try
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblAdjustment(referenceno, pcode, qty, action, remarks, sdate, [user]) VALUES (@referenceno, @pcode, @qty, @action, @remarks, @sdate, @user)", cn);
                    cm.Parameters.AddWithValue("@referenceno", lblRefNo.Text);
                    cm.Parameters.AddWithValue("@pcode", lblPcode.Text);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@action", cbAction.Text);
                    cm.Parameters.AddWithValue("@remarks", txtRemark.Text);
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now.ToShortDateString());
                    cm.Parameters.AddWithValue("@user", lblUsername.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    var frame = st.GetFrame(st.FrameCount - 1);
                    var linenumber = frame.GetFileLineNumber();
                    dbcon.Error("Adjustments/INSERTQUE", ex.Message, linenumber);
                }
                MessageBox.Show("Stock Has Been Successfully Adjusted.", "Process completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStock();
                Clear();
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Adjustments/btnSave_Click", ex.Message, linenumber);
            }
        }
    }
}
