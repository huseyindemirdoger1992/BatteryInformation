using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PowerStatus ps = SystemInformation.PowerStatus;
            int b = (int)(ps.BatteryLifePercent * 100);
            label1.Text = $@"Doluluk Oranı: %{((int)(ps.BatteryLifePercent * 100)).ToString()}";
            label2.Text = $@"Güç Kablosu: {ps.PowerLineStatus.ToString()}";
            progressBar1.Value = (int)(ps.BatteryLifePercent * 100);
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery");
            foreach (ManagementObject mo in mos.Get())
            {
                label3.Text = $@"Batarya Adı: {mo["Name"].ToString()}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            System.Management.ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject mo in collection)
            {
                foreach (PropertyData property in mo.Properties)
                {
                    listBox1.Items.Add($@"{property.Name} , {property.Value}");
                }
            }

        }
    }
}
