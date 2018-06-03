using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string[] ways = new string[4] { "S", "E", "N", "W" };
        int[] arrayHW = new int[6];
        List<string> prohod = new List<string>();
        List<string> prohodOut = new List<string>();
        string cur_or = "S";
        string[,] arrayLab = new string[1000, 1000];
        int cur_x = 0;
        int cur_y = 0;
        int max;
        string filePathOpen = "";
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();


        public void MakeTable()
        {
            prohod.Add("1000"); prohod.Add("0100"); prohod.Add("1100"); prohod.Add("0010");
            prohod.Add("1010"); prohod.Add("0110"); prohod.Add("1110"); prohod.Add("0001");
            prohod.Add("1001"); prohod.Add("0101"); prohod.Add("1101"); prohod.Add("0011");
            prohod.Add("1011"); prohod.Add("0111"); prohod.Add("1111");
        }

        public Form1()
        {
            InitializeComponent();
            MakeTable();
            prohodOut = prohod;
        }

        public string FileTxt() //считывание
        {
            string line;            
            System.IO.StreamReader file = new System.IO.StreamReader(filePathOpen);
            string inText = "";
            while ((line = file.ReadLine()) != null)
            {
                inText += line + "\n"; 
                //textBox2.Text += line + "\r\n";                
            }

            file.Close();
            return inText;
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            string inText = FileTxt();
            int strCount = int.Parse(inText.Split('\n')[0].ToString());
            string subStr1, subStr2;
            string strTest = ""; int temp = 0; 
           string outStr = "";
            for (int i = 1; i <= strCount; i++)
            {
                subStr1 = inText.Split('\n')[i].ToString().Split(' ')[0].ToString();
                subStr2 = inText.Split('\n')[i].ToString().Split(' ')[1].ToString();
                max = Math.Max(subStr1.Length, subStr2.Length);
                max=max+4;
                cur_or = "S";
                cur_x = max/2;
                cur_y = max-1;
                for (int j = 0; j < max; j++)
                {
                    for (int k = 0; k < max; k++)
                    {
                        arrayLab[j, k] = "xxxx";
                    }
                }
                outStr = "Case #" + i +":"+ "\r\n";
                textBox2.Text += outStr;
                lab_norm(subStr1);
                if(cur_or=="N")
                {
                    cur_or = "S";
                    cur_y--;
                }
                else if(cur_or=="S")
                {
                    cur_or = "N";
                    cur_y++;
                }
                else if (cur_or == "E")
                {
                    cur_or = "W";
                    cur_x--;
                }
                else if (cur_or == "W")
                {
                    cur_or = "E";
                    cur_x++;
                }
                lab_norm(subStr2);
                Output();
            }
        }

        private string lab_norm (string str_in)
        {
           for(int i=1;i<str_in.Length;i++)
            {
                string sub = str_in.Substring(i, 1);
                if (cur_or == "S")
                {
                    if (sub == "R")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 3, '0');
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 1, '0');
                        cur_or = "W";
                    }
                    else if (sub == "W")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 1, '1');
                        if (str_in.Substring(i - 1, 1) != "L")
                        {
                            arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 3, '0');
                        }
                        cur_y = cur_y - 1;
                    }
                    else if (sub == "L")
                    {
                        cur_or = "E";
                    }
                }
                else if (cur_or == "W")
                {
                    if (sub == "R")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 1, '0');
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 2, '0');
                        cur_or = "N";
                    }
                    else if (sub == "W")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 2, '1');
                        if (str_in.Substring(i - 1, 1) != "L")
                        {
                            arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 1, '0');
                        }
                        cur_x = cur_x - 1;
                    }
                    else if (sub == "L")
                    {
                        cur_or = "S";
                    }
                }
                else if (cur_or == "N")
                {
                    if (sub == "R")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 2, '0');
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 0, '0');
                        cur_or = "E";
                    }
                    else if (sub == "W")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 0, '1');
                        if (str_in.Substring(i - 1, 1) != "L")
                        {
                            arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 2, '0');
                        }
                        cur_y = cur_y + 1;
                    }
                    else if (sub == "L")
                    {
                        cur_or = "W";
                    }
                }
                else if (cur_or == "E")
                {
                    if (sub == "R")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 0, '0');
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 3, '0');
                        cur_or = "S";
                    }
                    else if (sub == "W")
                    {
                        arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 3, '1');
                        if (str_in.Substring(i - 1, 1) != "L")
                        {
                            arrayLab[cur_x, cur_y] = ReplaceCharInString3(arrayLab[cur_x, cur_y], 0, '0');
                        }
                        cur_x = cur_x + 1;
                    }
                    else if (sub == "L")
                    {
                        cur_or = "N";
                    }
                }
            }
            return "0";
        }
        
        public string ReplaceCharInString3(string source, int index, Char newSymb)
        {
            char[] chars = source.ToCharArray();
            chars[index] = newSymb;
            string tt = new string(chars);
            return tt;
        }



        private void Output ()
        {
            int start_x=0, start_y=0, end_x=0, end_y=0;
                for (int k = 0; k < max; k++)
                {
                    if(arrayLab[k, max-1] != "xxxx")
                    {
                        start_x = k;
                    break;
                    }
                }
            for (int k = max-1; k >= 0; k--)
            {
                if (arrayLab[k, max-1] != "xxxx")
                {
                    end_x = k;
                    break;
                }
            }
            for (int k = 0; k < max; k++)
            {
                if (arrayLab[max/2, k] != "xxxx")
                {
                    end_y = k;
                    break;
                }
            }
            for (int k = max-1; k >= 0; k--)
            {
                if (arrayLab[max/2, k] != "xxxx")
                {
                    start_y = k;
                    break;
                }
            }
            for (int i=start_y;i>=end_y;i--)
            {
                for (int j = start_x; j <= end_x; j++)
                {
                    arrayLab[j, i]=exchange(arrayLab[j, i]);
                    textBox2.Text += arrayLab[j, i]+" ";
                }
                textBox2.Text += "\r\n";
            }
        }

        private string exchange(string inp)
        {
            for(int i=0;i<15;i++)
            {
                if(inp==prohod[i])
                {
                    if(i<=8)
                        inp=(i+1).ToString();
                    else
                    {
                        if (i == 9)
                            inp = "a";
                        else if (i == 10)
                            inp = "b";
                        else if (i == 11)
                            inp = "c";
                        else if (i == 12)
                            inp = "d";
                        else if (i == 13)
                            inp = "e";
                        else if (i == 14)
                            inp = "f";
                    }
                }
            }
            return inp;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePathOpen = ofd.FileName;
                textBox1.Text = filePathOpen;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, textBox2.Text);
            MessageBox.Show("Файл сохранен");
        }
    }
}
