using KafeTekno.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeTekno.UI
{
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri _db;

        public GecmisSiparislerForm(KafeVeri db)
        {
            _db = db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;
            //dgvSiparisDetaylari.DataSource = 
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisDetaylari.SelectedRows.Count == 0)
            {
                dgvSiparisDetaylari.DataSource = null;
            }
            else
            {
                Siparis siparis = (Siparis)dgvSiparisDetaylari.SelectedRows[0].DataBoundItem;
                dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar;
            }
        }
    }
}
