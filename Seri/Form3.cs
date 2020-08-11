using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seri
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < From1.Data.Count; i++) {
                this.chart1.Series[0].Points.AddXY(From1.Data_time.ElementAt(i), From1.Data.ElementAt(i));
            }
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            From1.draw_chart = true;
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            From1.draw_chart = false;
            From1.Data.Clear();
            From1.Data_time.Clear();

            timer1.Stop();
        }
    }
}
