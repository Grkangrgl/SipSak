using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SipSak
{
    public partial class Form2 : Form
    {
        public string uygulama_adi = "";
        public string veri = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = uygulama_adi;
            textBox2.Text = veri;
        }
    }
}
