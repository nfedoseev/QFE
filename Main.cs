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

namespace qfe
{
    public partial class Main : Form
    {
        private String path = Environment.CurrentDirectory + "\\icao.txt";
        private String[] lines;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Main()
        {
            InitializeComponent();
            this.TopMost = true;

            if (!File.Exists(path))
                {
                    String data = "UUDD\r\nUUEE\r\nUUWW";
                    System.IO.StreamWriter file = new System.IO.StreamWriter(path);
                    file.WriteLine(data);

                    file.Close();
                }

            lines = System.IO.File.ReadAllLines(path);
            
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = (5000) * (1) * (1);
            timer.Enabled = true;
            timer.Start();
            updateTable();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            updateTable();
        }

        private void updateTable()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            foreach (String icao in lines)
            {
                try
                {
                    APIResponse wx = API.getWx(icao);
                    this.Text = "QFE" + " (" + wx.time + ")";
                    this.dataGridView1.Rows.Add(wx.icao, wx.wind, wx.value, wx.tl, wx.qnh, wx.qfe);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
