using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Seri
{
    public partial class Form3 : Form
    {
        static double last_time = 0;
        //public static Form3 fm3 = null;
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

            if (From1.Data_time.LastOrDefault() != last_time)
            {
                last_time = From1.Data_time.LastOrDefault();
                this.dataGridView1.Rows.Add(From1.Data_True_time.LastOrDefault(), Convert.ToString(From1.Data.LastOrDefault()), Convert.ToString(From1.Data_Current.LastOrDefault()), Convert.ToString(From1.Data_Torque.LastOrDefault()), Convert.ToString(From1.Data_Speed.LastOrDefault()), Convert.ToString(From1.Data_Postion.LastOrDefault()));
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
            From1.Data_Current.Clear();
            From1.Data_Postion.Clear();
            From1.Data_Speed.Clear();
            From1.Data_True_time.Clear();

            timer1.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            From1.draw_chart = false;
            From1.Data.Clear();
            From1.Data_time.Clear();
            From1.Data_Current.Clear();
            From1.Data_Postion.Clear();
            From1.Data_Speed.Clear();
            From1.Data_True_time.Clear();
            From1.time_add = 0;

            timer1.Stop();
        }

        private bool Save_datagrid(DataGridView dataGridView)
        {

            if (dataGridView.Rows.Count == 1)
            {
                MessageBox.Show("数据空","导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.GetEncoding(-0));
                string strLine = "";
                try
                {
                    //表头
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        if (i > 0)
                            strLine += ",";
                        strLine += dataGridView.Columns[i].HeaderText;
                    }
                    strLine.Remove(strLine.Length - 1);
                    sw.WriteLine(strLine);
                    strLine = "";
                    //表的内容
                    for (int j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        strLine = "";
                        int colCount = dataGridView.Columns.Count;
                        for (int k = 0; k < colCount; k++)
                        {
                            if (k > 0 && k < colCount)
                                strLine += ",";
                            if (dataGridView.Rows[j].Cells[k].Value == null)
                                strLine += "";
                            else
                            {
                                string cell = dataGridView.Rows[j].Cells[k].Value.ToString().Trim();
                                //防止里面含有特殊符号
                                cell = cell.Replace("\"", "\"\"");
                                cell = "\"" + cell + "\"";
                                strLine += cell;
                            }
                        }
                        sw.WriteLine(strLine);
                    }
                    sw.Close();
                    stream.Close();
                    MessageBox.Show("数据被导出到：" + saveFileDialog.FileName.ToString(), "导出完毕", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save_datagrid(dataGridView1);
        }

        private bool Save_Image()
        {
            if (From1.time_add == 0)
            {
                MessageBox.Show("数据空", "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            int count = 1;
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.Filter = "Jpg 图片|*.jpg|Bmp 图片|*.bmp|Gif 图片|*.gif|Png 图片|*.png|Wmf  图片|*.wmf";
            saveFileDialog2.FilterIndex = 4;
            saveFileDialog2.RestoreDirectory = true;
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                this.chart1.SaveImage(saveFileDialog2.FileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                string[] str = saveFileDialog2.FileName.Split('.');
                string filename = str[0] + '(' + count.ToString() + ')' + '.' + str[1];
                count = count + 1;
                this.chart2.SaveImage(filename, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                string[] str1 = saveFileDialog2.FileName.Split('.');
                string filename1 = str[0] + '(' + count.ToString() + ')' + '.' + str[1];
                this.chart3.SaveImage(filename1, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                MessageBox.Show("数据被导出到：" + saveFileDialog2.FileName.ToString(), "导出完毕", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            return true;
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            Save_Image();
        }
    }
}
