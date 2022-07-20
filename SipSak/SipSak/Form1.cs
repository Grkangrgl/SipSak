using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SipSak
{
    public partial class Form1 : Form
    {
            DataSet1TableAdapters.KopyalananlarTableAdapter kopyalananlar = new DataSet1TableAdapters.KopyalananlarTableAdapter();
        public Form1()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == kopyalamaislemleri.WM_CLIPBOARDUPDATE)
            {
                var unicode = kopyalamaislemleri.GetUnicodeText();
                uint processId = 0;
                string owner = "";

                IntPtr ownerHwnd = kopyalamaislemleri.GetClipboardOwner();
                if (ownerHwnd != IntPtr.Zero)
                {
                    kopyalamaislemleri.GetWindowThreadProcessId(ownerHwnd, out processId);
                    Process proc = Process.GetProcessById((int)processId);

                    owner = string.Format("{0} ({1})", proc.MainModule.ModuleName, (int)processId);
                }

                if (unicode != null)
                {
                    kopyalananlar.Kaydet(owner, unicode);
                    dataGridView1.DataSource = kopyalananlar.GetKopya();
                }

            }
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.kopyalananlarTableAdapter.FillKopya(this.dataSet1.Kopyalananlar);
            dataGridView1.DataSource = kopyalananlar.GetKopya();
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Uygulama Adı";
            dataGridView1.Columns[2].HeaderText = "İçerik";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            kopyalamaislemleri.AddClipboardFormatListener(Handle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kopyalamaislemleri.RemoveClipboardFormatListener(Handle);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int id = (Int32) dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value;
            kopyalananlar.Sil(id);
            //Silindikten sonra göster
            dataGridView1.DataSource = kopyalananlar.GetKopya();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 bilgi = new Form2();
            bilgi.uygulama_adi = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            bilgi.veri = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            bilgi.Show();
        }
    }
}
