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
            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].BorderWidth = 3;
            this.chart1.Series[0].LegendText = "电压";
            this.chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            this.chart1.Series[0].MarkerColor = Color.Black;


            this.chart2.Series[0].Color = Color.Red;
            this.chart2.Series[0].BorderWidth = 3;
            this.chart2.Series[0].LegendText = "电流";
            this.chart2.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            this.chart2.Series[0].MarkerColor = Color.Black;

            this.chart3.Series[0].Color = Color.Red;
            this.chart3.Series[0].BorderWidth = 3;
            this.chart3.Series[0].LegendText = "速度";
            this.chart3.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            this.chart3.Series[0].MarkerColor = Color.Black;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < From1.Data.Count; i++) {
                this.chart1.Series[0].Points.AddXY(From1.Data_time.ElementAt(i), From1.Data.ElementAt(i));
            }

            this.chart2.Series[0].Points.Clear();
            for (int i = 0; i < From1.Data_Current.Count; i++)
            {
                this.chart2.Series[0].Points.AddXY(From1.Data_time.ElementAt(i), From1.Data_Current.ElementAt(i));
            }

            this.chart3.Series[0].Points.Clear();
            for (int i = 0; i < From1.Data_Speed.Count; i++)
            {
                this.chart3.Series[0].Points.AddXY(From1.Data_time.ElementAt(i), From1.Data_Speed.ElementAt(i));
            }

            this.textBox1.Text = Convert.ToString(From1.Data.LastOrDefault());
            this.textBox2.Text = Convert.ToString(From1.Data_Current.LastOrDefault());
            this.textBox3.Text = Convert.ToString(From1.Data_Postion.LastOrDefault());
            this.textBox4.Text = Convert.ToString(From1.Data_Speed.LastOrDefault());

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
            From1.Data_Current.Clear();
            From1.Data_Postion.Clear();
            From1.Data_Speed.Clear();
            From1.time_add = 0;

            timer1.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
