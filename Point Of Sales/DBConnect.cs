using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    class DBConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;
        public string myConnection()
        {
            con = @"";
            return con;
        }
        public DataTable getTable(string que)
        {
            cn.ConnectionString = myConnection();
            cn.Open();
            cm = new SqlCommand(que, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            cn.Close();
            return table;
        }
        public void ExecuteQuery(String sql)
        {
            try
            {
                cn.ConnectionString = myConnection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                Error("DBConnect/ExecuteQuery", ex.Message, linenumber);
            }
        }
        public void Error(string ErrorName, string ErrorMsg, int linenumber)
        {
            try
            {
                cn.ConnectionString = myConnection();
                cn.Open();
                cm = new SqlCommand("INSERT INTO tblErrorLog(ErrorDate,ErrorName,ErrorMsg,ErrorLine)VALUES(@ErrorDate,@ErrorName,@ErrorMsg,@ErrorLine)", cn);
                cm.Parameters.AddWithValue("@ErrorDate", DateTime.Now);
                cm.Parameters.AddWithValue("@ErrorName", ErrorName.ToString());
                cm.Parameters.AddWithValue("@ErrorMsg", ErrorMsg.ToString());
                cm.Parameters.AddWithValue("@ErrorLine", linenumber.ToString());
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
        public double ExtractData(string sql)
        {
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = myConnection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                double data = double.Parse(cm.ExecuteScalar().ToString());
                cn.Close();
                return data;
            }
            catch (Exception ex)
            {
                var st = new System.Diagnostics.StackTrace(ex, true);
                var frame = st.GetFrame(st.FrameCount - 1);
                var linenumber = frame.GetFileLineNumber();
                Error("DBConnect/ExtractData", ex.Message, linenumber);
                return 0;
            }
        }
    }
}