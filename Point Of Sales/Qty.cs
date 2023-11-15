using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Qty : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        private string pcode;
        private double price;
        private string transno;
        private int qty;
        Cashier cashier;
        public Qty(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
        }
        public void ProductDetails(string pcode, double price, string transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && (txtQty.Text != string.Empty))
            {
                try
                {
                    string id = "";
                    int cart_qty = 0;
                    bool found = false;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblCart WHERE trasnno=@trasnno AND pcode=@pcode", cn);
                    cm.Parameters.AddWithValue("@trasnno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
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
                        cm = new SqlCommand("UPDATE tblCart SET qty=(qty +" + int.Parse(txtQty.Text) + ") WHERE id='" + id + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
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
                        cm.Parameters.AddWithValue("@trasnno", transno);
                        cm.Parameters.AddWithValue("@pcode", pcode);
                        cm.Parameters.AddWithValue("@price", price);
                        cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                        cm.Parameters.AddWithValue("@cashier", cashier.lblUsername.Text);
                        cm.ExecuteNonQuery();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cn.Close();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    cn.Close();
                    var st = new System.Diagnostics.StackTrace(ex, true);
                    var frame = st.GetFrame(st.FrameCount - 1);
                    var linenumber = frame.GetFileLineNumber();
                    dbcon.Error("Qty/txtQty_KeyPress", ex.Message, linenumber);
                }
            }
        }
    }
}
