using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Cashier : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        int qty;
        string id;
        string price;
        public Cashier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            GetTranNo();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void slide(Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top = button.Top;
        }

        private void btnNTran_Click(object sender, EventArgs e)
        {
            slide(btnNTran);
            GetTranNo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch);
            LookUpProduct lookUpProduct = new LookUpProduct(this);
            lookUpProduct.LoadProduct();
            lookUpProduct.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            slide(btnDiscount);
            Discount discount = new Discount(this);
            discount.lbId.Text = id;
            discount.txtTotalPrice.Text = price;
            discount.ShowDialog();
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            slide(btnSettle);
            Settle settle = new Settle(this);
            settle.txtSale.Text = lblDisplayTotal.Text;
            settle.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            slide(btnClear);
            try
            {
                if (MessageBox.Show("Remove all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("Delete from tblCart where trasnno like'" + lblTranNo.Text + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("All items has been successfully remove", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/btnClear_Click", ex.Message, linenumber);
            }
        }

        private void btnDSales_Click(object sender, EventArgs e)
        {
            slide(btnDSales);
            DailySale dailySale = new DailySale(new MainForm());
            dailySale.solduser = lblUsername.Text;
            dailySale.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slide(btnLogout);
            if (dgvCash.Rows.Count > 0)
            {
                MessageBox.Show("Unable to Logout. Please Complete the Transaction", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }

        }

        public void LoadCart()
        {
            try
            {
                Boolean hascart = false;
                int i = 0;
                double total = 0;
                double discount = 0;
                dgvCash.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("SELECT c.id, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode=p.pcode WHERE c.trasnno LIKE @trasnno and c.status LIKE 'Pending'", cn);
                cm.Parameters.AddWithValue("@trasnno", lblTranNo.Text);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Convert.ToDouble(dr["total"].ToString());
                    discount += Convert.ToDouble(dr["disc"].ToString());
                    dgvCash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));//
                    hascart = true;
                }
                dr.Close();
                cn.Close();
                lblSaleTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                if (hascart)
                {
                    btnClear.Enabled = true;
                    btnSettle.Enabled = true;
                    btnDiscount.Enabled = true;
                }
                else
                {
                    btnClear.Enabled = false;
                    btnSettle.Enabled = false;
                    btnDiscount.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/LoadCart", ex.Message, linenumber);
            }

        }
        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblSaleTotal.Text);
            double vat = sales * 0.12; //VAT: 12% of VAT Payable (Output Tax less Input Tax)
            double vatable = sales - vat;
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        public void GetTranNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;
                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 trasnno FROM tblCart WHERE trasnno LIKE '" + sdate + "%' ORDER BY id DESC", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTranNo.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTranNo.Text = transno;
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/GetTranNo", ex.Message, linenumber);
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    string _pcode;
                    double _price;
                    int _qty;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblProduct WHERE barcode ='" + txtBarcode.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);
                        dr.Close();
                        cn.Close();
                        // Insert to cart table
                        AddToCart(_pcode, _price, _qty);
                    }
                    dr.Close();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/txtBarcode_TextChanged", ex.Message, linenumber);
            }
        }
        public void AddToCart(string _pcode, double _price, int _qty)
        {
            try
            {
                string id = "";
                int cart_qty = 0;
                bool found = false;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblCart WHERE trasnno=@trasnno AND pcode=@pcode", cn);
                cm.Parameters.AddWithValue("@trasnno", lblTranNo.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    id = dr["id"].ToString();
                    cart_qty = int.Parse(dr["qty"].ToString());
                    found = true;
                }
                found = false;
                dr.Close();
                cn.Close();
                if (found)
                {
                    if (qty < int.Parse(txtQty.Text) + cart_qty)
                    {
                        MessageBox.Show("Unable to procced remaning qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET qty=(qty +" + qty + ") WHERE id='" + id + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    txtBarcode.SelectionStart = 0;
                    txtBarcode.SelectionLength = txtBarcode.Text.Length;
                    LoadCart();
                }
                else
                {
                    if (qty < int.Parse(txtQty.Text) + cart_qty)
                    {
                        MessageBox.Show("Unable to procced remaning qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblCart (trasnno,pcode,price,qty,sdate,cashier) VALUES (@trasnno,@pcode,@price,@qty,@sdate,@cashier)", cn);
                    cm.Parameters.AddWithValue("@trasnno", lblTranNo.Text);
                    cm.Parameters.AddWithValue("@pcode", _pcode);
                    cm.Parameters.AddWithValue("@price", _price);
                    cm.Parameters.AddWithValue("@qty", _qty);
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", lblUsername.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadCart();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/AddToCart", ex.Message, linenumber);
            }
        }

        private void dgvCash_SelectionChanged(object sender, EventArgs e)
        {
            int i = dgvCash.CurrentRow.Index;
            id = dgvCash[1, i].Value.ToString();
            price = dgvCash[7, i].Value.ToString();
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dgvCash.Columns[e.ColumnIndex].Name;
                if (colName == "Delete")
                {
                    if (MessageBox.Show("Remove this item", "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbcon.ExecuteQuery("Delete from tblCart where id like'" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                        MessageBox.Show("Items has been successfully remove", "Remove item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCart();
                    }
                }
                else if (colName == "colAdd")
                {
                    try
                    {
                        int i = 0;
                        cn.Open();
                        cm = new SqlCommand("SELECT SUM(qty) as qty FROM tblProduct WHERE pcode LIKE'" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "' GROUP BY pcode", cn);
                        i = int.Parse(cm.ExecuteScalar().ToString());
                        cn.Close();
                        if (int.Parse(dgvCash.Rows[e.RowIndex].Cells[5].Value.ToString()) < i)
                        {
                            dbcon.ExecuteQuery("UPDATE tblCart SET qty = qty + " + int.Parse(txtQty.Text) + " WHERE trasnno LIKE '" + lblTranNo.Text + "'  AND pcode LIKE '" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "'");
                            LoadCart();
                        }
                        else
                        {
                            MessageBox.Show("Remaining qty on hand is " + i + "!", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new System.Diagnostics.StackTrace(ex, true);
                        var frame = st.GetFrame(st.FrameCount - 1);
                        var linenumber = frame.GetFileLineNumber();
                        dbcon.Error("Cashier/colAdd", ex.Message, linenumber);
                    }
                }
                else if (colName == "colReduce")
                {
                    try
                    {
                        int i = 0;
                        cn.Open();
                        cm = new SqlCommand("SELECT SUM(qty) as qty FROM tblCart WHERE pcode LIKE'" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "' GROUP BY pcode", cn);
                        i = int.Parse(cm.ExecuteScalar().ToString());
                        cn.Close();
                        if (i > 1)
                        {
                            dbcon.ExecuteQuery("UPDATE tblCart SET qty = qty - " + int.Parse(txtQty.Text) + " WHERE trasnno LIKE '" + lblTranNo.Text + "'  AND pcode LIKE '" + dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString() + "'");
                            LoadCart();
                        }
                        else
                        {
                            MessageBox.Show("Remaining qty on cart is " + i + "!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new System.Diagnostics.StackTrace(ex, true);
                        var frame = st.GetFrame(st.FrameCount - 1);
                        var linenumber = frame.GetFileLineNumber();
                        dbcon.Error("Cashier/colReduce", ex.Message, linenumber);
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("Cashier/dgvCash_CellContentClick", ex.Message, linenumber);
            }
        }
    }
}
