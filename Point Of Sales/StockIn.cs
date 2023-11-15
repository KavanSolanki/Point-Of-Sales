using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class StockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public StockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadSupplier();
            GetRefNo();
        }

        public void GetRefNo()
        {
            Random random = new Random();
            txtRefNo.Clear();
            txtRefNo.Text += random.Next();
        }

        public void LoadSupplier()
        {
            try
            {
                cbSupplier.Items.Clear();
                cbSupplier.DataSource = dbcon.getTable("SELECT * FROM tblSupplier");
                cbSupplier.DisplayMember = "supplier";
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/LoadSupplier", ex.Message, linenumber);
            }
        }

        public void LoadStockIn()
        {
            try
            {
                int i = 0;
                dgvStockIn.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM vwStockIn WHERE refno LIKE '" + txtRefNo.Text + "' AND status LIKE 'Pending'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvStockIn.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[8].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/LoadStockIn", ex.Message, linenumber);
            }
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblSupplier WHERE supplier LIKE '" + cbSupplier.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblId.Text = dr["id"].ToString();
                    txtConPerson.Text = dr["contactperson"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/cbSupplier_SelectedIndexChanged", ex.Message, linenumber);
            }
        }

        private void cbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void LinGenerate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetRefNo();
        }

        private void LinProduct_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductStockIn productStockIn = new ProductStockIn(this);
            productStockIn.ShowDialog();
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStockIn.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to save this record?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dgvStockIn.Rows.Count; i++)
                        {
                            //update product quantity
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblProduct SET qty = qty + " + int.Parse(dgvStockIn.Rows[i].Cells[5].Value.ToString()) + " WHERE pcode LIKE '" + dgvStockIn.Rows[i].Cells[3].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            //update stockin quantity
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblStockIn SET qty = qty + " + int.Parse(dgvStockIn.Rows[i].Cells[5].Value.ToString()) + ", status='Done' WHERE id LIKE '" + dgvStockIn.Rows[i].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }
                        Clear();
                        LoadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/btnEntry_Click", ex.Message, linenumber);
            }
        }
        public void Clear()
        {
            txtRefNo.Clear();
            txtStockInBy.Clear();
            dtStockIn.Value = DateTime.Now;
        }

        private void dgvStockIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvStockIn.Columns[e.ColumnIndex].Name;
            try
            {
                if (colName == "Delete")
                {
                    if (MessageBox.Show("Remove this item?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("DELETE FROM tblStockIn WHERE id='" + dgvStockIn.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Iteam has been sucessfully removed", "Point of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/dgvStockIn_CellContentClick", ex.Message, linenumber);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                dgvInStockHistory.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM vwStockIn WHERE sdate BETWEEN '" + dtFrom.Value.ToString("yyyy-MM-dd") + "' AND '" + dtTo.Value.ToString("yyyy-MM-dd") + "'AND status LIKE 'Done'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvInStockHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr["supplier"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("StockIn/btnLoad_Click", ex.Message, linenumber);
            }
        }
    }
}
