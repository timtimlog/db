using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            button4.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.Rows.RemoveAt(id);
            button4.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string profession = textBox2.Text;
            string phone_number = textBox3.Text;
            if (name != "" && profession != "" && phone_number != "")
            { dataGridView1.Rows.Add(name, profession, phone_number); textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader read = new StreamReader(Application.StartupPath + @"\db.txt");
            try
            {
                string[] str;
                int num = 0;
                string[] str1 = read.ReadToEnd().Split('\n');
                num = str1.Count();
                dataGridView1.RowCount = num;
                try
                {
                    for (int i = 0; i < num; i++)
                    {
                        str = str1[i].Split('^');
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            string data = str[j].Replace("[separator]", "^");
                            dataGridView1.Rows[i].Cells[j].Value = data;
                        }
                    }
                }
                catch { }
            } catch { }
            finally { read.Close(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\db.txt");
            try
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    try
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            string data = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace("^", "[separator]");
                            writer.Write(data + "^");
                        }
                        writer.WriteLine();
                    }
                    catch { }
                }
            }
            catch { }
            finally { writer.Close(); }
        }
    }
}
