using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Product product;
        public ProductModule(Product pd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadBrand();
            LoadCategory();
            this.product = pd;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCategory()
        {
            try
            {
                cboCategory.Items.Clear();
                cboCategory.DataSource = dbcon.getTable("SELECT * FROM tblCategory");
                cboCategory.DisplayMember = "category";
                cboCategory.ValueMember = "id";
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("ProductModule/LoadCategory", ex.Message, linenumber);
            }

        }
        public void LoadBrand()
        {
            try
            {
                cboBrand.Items.Clear();
                cboBrand.DataSource = dbcon.getTable("SELECT * FROM tblBrand");
                cboBrand.DisplayMember = "brand";
                cboBrand.ValueMember = "id";
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("ProductModule/LoadCategory", ex.Message, linenumber);
            }
        }
        public void Clear()
        {
            txtPcode.Clear();
            txtBarcode.Clear();
            txtPdesc.Clear();
            txtPrice.Clear();
            cboBrand.SelectedIndex = 0;
            cboCategory.SelectedIndex = 0;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtPcode.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPcode.Text) && string.IsNullOrEmpty(txtBarcode.Text) && string.IsNullOrEmpty(txtPdesc.Text)  && string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Please enter all Data", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtPcode.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtBarcode.Text, @"[0-9][0-9]") && !Regex.IsMatch(txtPdesc.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtPdesc.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show("This is not valid!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to save this product?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblProduct (pcode,barcode,pdesc,bid,cid,price) VALUES(@pcode,@barcode,@pdesc,@bid,@cid,@price)", cn);
                        cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                        cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                        cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                        cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                        cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                        cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been sucessfully saved.", "Point of Sales");
                        Clear();
                        product.LoadProduct();
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                dbcon.Error("ProductModule/btnSave_Click", ex.Message, linenumber);
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
                if (string.IsNullOrEmpty(txtPcode.Text) && string.IsNullOrEmpty(txtBarcode.Text) && string.IsNullOrEmpty(txtPdesc.Text) && string.IsNullOrEmpty(txtPrice.Text))
                {
                    MessageBox.Show("Please Enter  all Data", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!Regex.IsMatch(txtPcode.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtBarcode.Text, @"[0-9][0-9]") && !Regex.IsMatch(txtPdesc.Text, @"[A-Za-z][A-Za-z]") && !Regex.IsMatch(txtPdesc.Text, @"[A-Za-z][A-Za-z]"))
                {
                    MessageBox.Show("This is not valid!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to update this product?", "Point of Sales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblProduct SET barcode=@barcode,pdesc=@pdesc,bid=@bid,cid=@cid,price=@price WHERE pcode='" + txtPcode.Text + "' ", cn);
                        cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                        cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                        cm.Parameters.AddWithValue("@bid", cboBrand.SelectedValue);
                        cm.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                        cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been sucessfully Updated.", "Point of Sales");
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
                dbcon.Error("ProductModule/btnUpdate_Click", ex.Message, linenumber);
            }
        }
    }
}
