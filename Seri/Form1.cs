﻿using Microsoft.Win32;
using System;
using System.Collections;
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
    public partial class From1 : Form
    {
        string last_port;
        bool show_time = true;
        bool show_hex = false;
        bool stop_show = false;
        bool send_hex = false;
        bool send_enter = false;
        bool auto_send = false;
        bool auto_clear = false;
        public static bool draw_chart = false;
        //bool error = false;
        long rec_num = 0;
        long send_num = 0;

        public static From1 fm1 = null;
        public static Queue<double> Data = new Queue<double>(200);
        public static Queue<double> Data_Current = new Queue<double>(200);
        public static Queue<Int16> Data_Postion = new Queue<Int16>(200);
        public static Queue<double> Data_Speed = new Queue<double>(200);
        public static Queue<Int32> Data_time = new Queue<Int32>(200);
        public static Queue<string> Data_True_time = new Queue<string>(200);
        public static Queue<double> Data_Torque = new Queue<double>(200);
        private int num_limit = 5;//超过100个点则舍去前5个点

        //double Last_time = DateTime.Now.TimeOfDay.TotalSeconds;
        public static Int32 time_add = 0;

        public From1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 11;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 3;
            comboBox5.SelectedIndex = 0;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            

 /*          System.Timers.Timer tim = new System.Timers.Timer(100);
            tim.Elapsed += new System.Timers.ElapsedEventHandler(theout);
            tim.AutoReset = true;
            timer1.Enabled = true;  */

            timer1.Start();
            textBox_rec.WordWrap = true;
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
            toolTip1.SetToolTip(textBox_rec, "接收数据显示");
            toolTip1.SetToolTip(textBox_send, "发送数据显示");

            //用于form2 函数调用
            fm1 = this;

        }


        //启动执行
        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                comboBox2.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox2.Items.Add(sValue);
                }
                if (comboBox2.Items.Count > 0)
                    comboBox2.SelectedIndex = 0;
            }
        }

        //自动清理

        public  void Auto_clear()
        {
            if (auto_clear)
            {
                int length = 0;
                length = textBox_rec.Lines.GetUpperBound(0);
                if (length > 24)
                {
                    textBox_rec.Text = "";
                }
            }
        }

        
        //十六进制显示
        public void ToHex(string buffer,string time)
        {
            char[] valus = buffer.ToArray();
            string buffer2 = "";
            foreach(long letter in valus)
            {
                long valu = Convert.ToInt64(letter);
                buffer2 +=valu.ToString("X2");
                buffer2 += " ";
            }

            if (show_time)
            {
                textBox_rec.AppendText("[");
                textBox_rec.AppendText(time);
                textBox_rec.AppendText("]");
                textBox_rec.AppendText("←收◆ ");
            }
            if (send_hex)
            {
                textBox_rec.AppendText(buffer);
            }
            else
                textBox_rec.AppendText(buffer2);
            if(show_time)
                textBox_rec.AppendText("\r\n");
        }

       //文本显示
        public void Tostring(string buffer, string time)
        {
            Auto_clear();
            if (show_time)
            {
                textBox_rec.AppendText("[");
                textBox_rec.AppendText(time);
                textBox_rec.AppendText("]");
                textBox_rec.AppendText("←收◆ "+buffer+"\r\n");
            }
            else
                textBox_rec.AppendText(buffer);

            

        }

        //十六进制发送
        public static byte[] HexStringToBytes(string hs)
        {
            byte[] a = {0x00};

            //非静态变量处理方法
            /*From1 tc = new From1();
            tc.error = true;            */
            try
            {
                //byte[] mid = Encoding.Default.GetBytes(hs);
                string[] strArr = hs.Trim().Split(' ');
                byte[] b = new byte[strArr.Length];
                //逐个字符变为16进制字节数据
                for (int i = 0; i < strArr.Length; i++)
                {
                    b[i] = Convert.ToByte(strArr[i], 16);
                }
                return b;
            }
            catch (Exception)
            {

                MessageBox.Show("输入正确的十六进制数");

                return a;
            }

        }

        //字节数组转16进制字符串
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                    returnStr += " ";
                }
            }
            return returnStr;
        }

        public void sand_data(string data,bool ishex)
        {
            long num = 0;
            string msg = "";
            try
            {
                //首先判断串口是否开启
                if (serialPort1.IsOpen)
                {
                    //判断是否发送回车换行
                    if (send_enter)
                    {
                        if (ishex)
                            msg = data + " 0D 0A";
                        else
                            msg = data + "\r\n";
                    }
                    else
                        msg = data;
                    if (ishex)
                    {
                        Byte[] hex = HexStringToBytes(msg);
                        serialPort1.Write(hex, 0, hex.Length);
                    }
                    else
                    {   //用于解决不能显示和发送中文
                        /*System.Text.UTF8Encoding unic = new System.Text.UTF8Encoding();
                        Byte[] writeBytes = unic.GetBytes(msg);*/

                        Encoding gb = System.Text.Encoding.GetEncoding("gb2312");
                        byte[] writeBytes = gb.GetBytes(msg);
                        serialPort1.Write(writeBytes, 0, writeBytes.Length);
                    }

                    if (show_time ^ stop_show)
                    {
                        string time = DateTime.Now.TimeOfDay.ToString();
                        time = time.Substring(0, 11);
                        this.Invoke((EventHandler)(delegate
                        {
                            textBox_rec.AppendText("[");
                            textBox_rec.AppendText(time);
                            textBox_rec.AppendText("]");
                            textBox_rec.AppendText("→发◇ " + msg);
                            textBox_rec.AppendText("\r\n");
                        }
                            )
                        );
                    }

                    // 计算发送的数据长度
                    num = System.Text.Encoding.Default.GetBytes(textBox_send.Text).Length;
                    send_num += num;
                    label12.Text = send_num.ToString();
                }
         
            }
            catch (Exception ex)
            {
                //捕获到异常，创建一个新的对象，之前的不可以再用
                serialPort1 = new System.IO.Ports.SerialPort();
                //刷新COM口选项
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
                //响铃并显示异常给用户
                System.Media.SystemSounds.Beep.Play();
                button1.Text = "打开串口";
                button1.BackColor = Color.ForestGreen;
                MessageBox.Show(ex.Message);
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
            }
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //串口扫描
        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者：RtuQ\r本串口助手界面参照野火串口助手\r日期：2019年3月28日");
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void 黑白ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_rec.BackColor = colorDialog1.Color;
                textBox_send.BackColor = colorDialog1.Color;
            }
        }

        private void 绿黑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_rec.ForeColor = colorDialog1.Color;
                textBox_send.ForeColor = colorDialog1.Color;
            }
        }


        //串口接收
        private void SerialPort1(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string buffer = "";
            long num = 0;
            try
            {
                string time = DateTime.Now.TimeOfDay.ToString();
                time = time.Substring(0, 11);

                //等待数据进入缓冲区 不加延时可能发生数据分段现象
                System.Threading.Thread.Sleep(50);

                //用于解决不能显示和发送中文
                /*
                System.Text.ASCIIEncoding unic = new System.Text.ASCIIEncoding();
                string buffer2 = serialPort1.ReadExisting();
                Byte[] readBytes = System.Text.Encoding.Default.GetBytes(buffer2);

                // for (int i = 0; i < readBytes.Length; i++)
                //{
                //   readBytes[i] = (byte)serialPort1.ReadByte();
                // }
                buffer = unic.GetString(readBytes);
                */

                ///*
                int len = serialPort1.BytesToRead;
                byte[] bytes = new byte[len];
                serialPort1.Read(bytes, 0, len);
                if (bytes[0] == 0xEE && bytes[1] == 0xBB && draw_chart == true)
                {
                    //TimeSpan New_time = new TimeSpan(DateTime.Now.Ticks);
                    ////double New_time = DateTime.Now.TimeOfDay.TotalSeconds;
                    //TimeSpan add_time = New_time.Subtract(Last_time).Duration();
                    //double Add_time = add_time.TotalSeconds;
                    ////double Add_time = New_time - Last_time;
                    ///Last_time = New_time;

                    if (len > 12)
                    {
                        double Volte = bytes[2] + (bytes[3] * 0.1);

                        Int16 Postion = bytes[4];
                        Postion = (Int16)(Postion << 8);
                        Postion += bytes[5];

                        double Speed = (bytes[6] + (bytes[7] * 0.1)) / (4.0);

                        Int16 Current = bytes[10];
                        Current = (Int16)(Current << 8);
                        Current += bytes[9];
                        double Current_data = Current * 0.01;

                        Int16 Torque = bytes[11];
                        Torque = (Int16)(Torque << 8);
                        Torque += bytes[12];
                        double Torque_data = Torque * 0.001;

                        time_add = time_add + 20;
                        Data.Enqueue(Volte);
                        Data_Current.Enqueue(Current_data);
                        Data_Postion.Enqueue(Postion);
                        Data_Speed.Enqueue(Speed);
                        Data_time.Enqueue(time_add);
                        Data_Torque.Enqueue(Torque_data);
                        Data_True_time.Enqueue(time);

                    }
                    else
                    {
                        MessageBox.Show("请检查数据长度", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        draw_chart = false;
                    }

                    if (Data.Count > 150)
                    {
                        for (int i = 0; i < num_limit; i++)
                        {
                            Data.Dequeue();
                            Data_Current.Dequeue();
                            Data_Postion.Dequeue();
                            Data_Speed.Dequeue();
                            Data_time.Dequeue();
                            Data_True_time.Dequeue();
                            Data_Torque.Dequeue();
                        }
                    }


                }

                    
          

                if (show_hex)
                    buffer = byteToHexStr(bytes);
                else
                    buffer = System.Text.Encoding.Default.GetString(bytes); 
                    // */
                
                this.Invoke((EventHandler)(delegate
                {
                    if (stop_show == false)
                    {
                            Tostring(buffer, time);
                    }
                }
                   )
                );

                // 计算接受到的数据长度
                num = System.Text.Encoding.Default.GetBytes(textBox_send.Text).Length;
                rec_num += num;
                label11.Text = rec_num.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    button1.ForeColor = Color.Black;
                    button1.Text = "打开串口";
                    button1.BackColor = Color.White;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    comboBox5.Enabled = true;
                    timer1.Start();
                }
                else
                {
                    //串口已经处于关闭状态，则设置好串口属性后打开
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;
                    serialPort1.PortName = comboBox2.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox1.Text);
                    serialPort1.DataBits = Convert.ToInt16(comboBox4.Text);

                    if (comboBox3.Text.Equals("None"))
                        serialPort1.Parity = System.IO.Ports.Parity.None;
                    else if (comboBox3.Text.Equals("Odd"))
                        serialPort1.Parity = System.IO.Ports.Parity.Odd;
                    else if (comboBox3.Text.Equals("Even"))
                        serialPort1.Parity = System.IO.Ports.Parity.Even;
                    else if (comboBox3.Text.Equals("Mark"))
                        serialPort1.Parity = System.IO.Ports.Parity.Mark;
                    else if (comboBox3.Text.Equals("Space"))
                        serialPort1.Parity = System.IO.Ports.Parity.Space;

                    if (comboBox5.Text.Equals("1"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    else if (comboBox5.Text.Equals("1.5"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    else if (comboBox5.Text.Equals("2"))
                        serialPort1.StopBits = System.IO.Ports.StopBits.Two;

                    serialPort1.Open();     //打开串口
                    button1.ForeColor = Color.White;
                    button1.Text = "关闭串口";
                    button1.BackColor = Color.Red;
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                //捕获可能发生的异常并进行处理
                //捕获到异常，创建一个新的对象，之前的不可以再用
                serialPort1 = new System.IO.Ports.SerialPort();
                //刷新COM口选项
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());

                button1.ForeColor = Color.Black;
                button1.Text = "打开串口";
                button1.BackColor = Color.White;
                if (ex.Message.Contains("PortName"))
                    MessageBox.Show("没有串口，请连接串口！！");
                else
                    MessageBox.Show(ex.Message);
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                timer1.Start();
            }
        }




        //串口发送
        private void button2_Click(object sender, EventArgs e)
        {
            if(auto_send == false)
                sand_data(textBox_send.Text,send_hex);
        }

        private void 时间戳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            show_time = !show_time;
        }

        private void scannum(object sender, KeyPressEventArgs e)
        {
            //设置 自动发送 只能输入数字
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {
                auto_send = false;
                timer2.Interval = Convert.ToInt32(textBox1.Text);
                timer2.Start();
            }
            else if(checkBox3.CheckState  == CheckState.Unchecked)
            {
                auto_send = true;
                timer2.Stop();
            }
        }

        //自动发送
        private void timer2_tik(object sender, EventArgs e)
        {
            sand_data(textBox_send.Text, send_hex);
        }

        //自动清理选择
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.CheckState == CheckState.Checked)
            {
                auto_clear = true;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                auto_clear = false;
            }
        }

        //清空接收框
        private void button4_Click(object sender, EventArgs e)
        {
            textBox_rec.Text = "";
        }

        //清空send
        private void button7_Click(object sender, EventArgs e)
        {
            textBox_send.Text = "";
        }
        
        //显示格式
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.CheckState == CheckState.Checked)
            {
                show_hex = true;
            }
            else if (checkBox2.CheckState == CheckState.Unchecked)
            {
                show_hex = false;
            }
        }

        //显示开关
        private void button5_Click(object sender, EventArgs e)
        {
            stop_show = !stop_show;
            if (stop_show)
            {
                button5.Text = "恢复显示";
                button5.ForeColor = Color.White;
                button5.BackColor = Color.Red;
            }
            else
            {
                button5.Text = "停止显示";
                button5.ForeColor = Color.Black;
                button5.BackColor = Color.WhiteSmoke;
            }
        }

        //计数清零
        private void button3_Click(object sender, EventArgs e)
        {
            label11.Text = "0";
            label12.Text = "0";
            rec_num = 0;
            send_num = 0;
        }

        //保存文件
        private void button6_Click(object sender, EventArgs e)
        {
            StreamWriter myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamWriter(saveFileDialog1.FileName);

                myStream.Write(textBox_rec.Text); //写入

                myStream.Close();//关闭流
            }
            
        }

        //打开文件
        private void button8_Click(object sender, EventArgs e)
        {


            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFileDialog1.ValidateNames = true;
            OpenFileDialog1.CheckPathExists = true;
            OpenFileDialog1.CheckFileExists = true;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFileName = OpenFileDialog1.FileName;
                textBox2.Text = strFileName;
                // Open the file to read from.
            using (StreamReader sr = File.OpenText(strFileName))
            {
                string s;
                if ((s = sr.ReadToEnd()) != null)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        textBox_send.AppendText(s);
                    }
                            )
                        );
                    }
            }
            }
            
        }


        //发送hex 选择
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                send_hex = true;
                textBox_send.Text = "01 02 03 04";
            }
            else if (checkBox4.CheckState == CheckState.Unchecked)
            {
                send_hex = false;
                textBox_send.Text = "";
            }
        }

        //关闭窗口
        private void form_close(object sender, FormClosedEventArgs e)
        {
            serialPort1 = new System.IO.Ports.SerialPort();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        //发送回车换行
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                send_enter = true;
            }
            else if (checkBox5.CheckState == CheckState.Unchecked)
            {
                send_enter = false;
            }
        }

        //设置显示字体的大小
        private void 设置接收字体大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
             {
                  textBox_rec.Font = fontDialog1.Font;
                  textBox_send.Font = fontDialog1.Font;
              }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void 多字符串ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox5.CheckState = CheckState.Checked;
            Form2 Fm2 = new Form2();
            Fm2.Show();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox_rec_TextChanged(object sender, EventArgs e)
        {

        }

        private void 绘制图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Fm3 = new Form3();
            Fm3.Show();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            int i = 0;
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                last_port = comboBox2.Text;
                comboBox2.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox2.Items.Add(sValue);
                    if (last_port == sValue)
                        comboBox2.SelectedIndex = i;
                    else
                        comboBox2.SelectedIndex = 0;
                    i++;
                }
            }
        }
    }
}
