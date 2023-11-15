using System;
using System.Windows.Forms;

namespace Point_Of_Sales
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int startpos = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            ProgressIndi.Start();
            if (startpos > 50)
            {
                Login login = new Login();
                ProgressIndi.Stop();
                timer.Stop();
                this.Hide();
                login.Show();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
