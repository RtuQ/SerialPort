using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seri
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            text1.Text = Settings1.Default.text1;
            // 创建一个 the ToolTip .
            ToolTip toolTip1 = new ToolTip
            {

                // 设置延时.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                // 开启？
                ShowAlways = true
            };

            // 设置需要显示的数据
            toolTip1.SetToolTip(button33, "每一行对应一个字符串");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(check1.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text1.Text,true);
            else
                From1.fm1.sand_data(this.text1.Text,false);
            Settings1.Default.text1 = text1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check2.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text2.Text,true);
            else
                From1.fm1.sand_data(this.text2.Text,false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check3.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text3.Text,true);
            else
                From1.fm1.sand_data(this.text3.Text,false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (check4.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text4.Text, true);
            else
                From1.fm1.sand_data(this.text4.Text, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (check5.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text5.Text, true);
            else
                From1.fm1.sand_data(this.text5.Text, false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (check6.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text6.Text, true);
            else
                From1.fm1.sand_data(this.text6.Text, false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (check7.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text7.Text, true);
            else
                From1.fm1.sand_data(this.text7.Text, false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (check8.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text8.Text, true);
            else
                From1.fm1.sand_data(this.text8.Text, false);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (check9.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text9.Text, true);
            else
                From1.fm1.sand_data(this.text9.Text, false);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (check10.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text10.Text, true);
            else
                From1.fm1.sand_data(this.text10.Text, false);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (check11.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text11.Text, true);
            else
                From1.fm1.sand_data(this.text11.Text, false);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (check12.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text12.Text, true);
            else
                From1.fm1.sand_data(this.text12.Text, false);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (check13.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text13.Text, true);
            else
                From1.fm1.sand_data(this.text13.Text, false);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (check14.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text14.Text, true);
            else
                From1.fm1.sand_data(this.text14.Text, false);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (check15.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text15.Text, true);
            else
                From1.fm1.sand_data(this.text15.Text, false);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (check16.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text16.Text, true);
            else
                From1.fm1.sand_data(this.text16.Text, false);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (check17.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text17.Text, true);
            else
                From1.fm1.sand_data(this.text17.Text, false);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (check18.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text18.Text, true);
            else
                From1.fm1.sand_data(this.text18.Text, false);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (check19.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text19.Text, true);
            else
                From1.fm1.sand_data(this.text19.Text, false);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (check20.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text20.Text, true);
            else
                From1.fm1.sand_data(this.text20.Text, false);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (check21.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text21.Text, true);
            else
                From1.fm1.sand_data(this.text21.Text, false);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (check22.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text22.Text, true);
            else
                From1.fm1.sand_data(this.text22.Text, false);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (check23.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text23.Text, true);
            else
                From1.fm1.sand_data(this.text23.Text, false);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (check24.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text24.Text, true);
            else
                From1.fm1.sand_data(this.text24.Text, false);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (check25.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text25.Text, true);
            else
                From1.fm1.sand_data(this.text25.Text, false);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (check26.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text26.Text, true);
            else
                From1.fm1.sand_data(this.text26.Text, false);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (check27.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text27.Text, true);
            else
                From1.fm1.sand_data(this.text27.Text, false);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (check28.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text28.Text, true);
            else
                From1.fm1.sand_data(this.text28.Text, false);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (check29.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text29.Text, true);
            else
                From1.fm1.sand_data(this.text29.Text, false);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (check30.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text30.Text, true);
            else
                From1.fm1.sand_data(this.text30.Text, false);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (check31.CheckState == CheckState.Checked)
                From1.fm1.sand_data(this.text31.Text, true);
            else
                From1.fm1.sand_data(this.text31.Text, false);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void text7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            string box = "text1";
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFileDialog1.ValidateNames = true;
            OpenFileDialog1.CheckPathExists = true;
            OpenFileDialog1.CheckFileExists = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFileName = OpenFileDialog1.FileName;
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(strFileName))
                {
                    string s;
                    int i = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        switch (i)
                       {
                            case 0:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text1.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 1:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text2.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 2:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text3.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 3:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text4.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 4:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text5.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 5:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text6.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 6:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text7.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 7:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text8.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 8:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text9.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 9:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text10.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 10:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text11.AppendText(s);
                                }
                                    )
                                );
                                break;
                            case 11:
                                this.Invoke((EventHandler)(delegate
                                {
                                    text12.AppendText(s);
                                }
                                    )
                                );
                                break;


                        }
                        i++;
                            
                            
                    }
                }
            }
        }
    }
}
