using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpamPing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string tr;
        int sayi=0;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            tr = textBox1.Text;
            sayi = Convert.ToInt32(numericUpDown1.Value);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var ping = new Ping();
            sayi--;
            listBox1.Items.Add(sayi);
            if (sayi == 0)
            {
                timer1.Stop();
                MessageBox.Show("Ping İşlemi Sona Erdi...!");
                textBox1.Clear();
                listBox1.Items.Clear();
                numericUpDown1.Value = 0;
            }
            else
            {
                listBox1.Items.Add("*********************************");
                var cevap = ping.Send(tr);
                if (cevap.Status == IPStatus.Success)
                {
                    listBox1.Items.Add("Server Ayakta...!");
                    listBox1.Items.Add("IP Adres: "+cevap.Address);
                    listBox1.Items.Add("TimeToLife: " + cevap.Options.Ttl);
                    listBox1.Items.Add("Durum: " + cevap.Status);
                    listBox1.Items.Add("Ping Zamanı: " + DateTime.Now);
                    listBox1.Items.Add("IP Adres: " + cevap.Address);
                    listBox1.Items.Add("Time: " + cevap.RoundtripTime);
                }
                else
                {
                    listBox1.Items.Add("Server Düştü...!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string mycomputer = Dns.GetHostName();
            label4.Text = "Bilgisayar Adım: " + mycomputer;

            IPAddress ip = Dns.GetHostByName(mycomputer).AddressList[0];
            label5.Text = "IP Adresim: " + ip.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
