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
    public partial class SiparisForm : Form
    {
        private readonly Siparis _siparis;
        private readonly KafeVeri _veri;
        private readonly BindingList<SiparisDetay> _blSiparisDetay;

        public SiparisForm(Siparis siparis, KafeVeri veri)
        {
            _siparis = siparis;
            _veri = veri;
            _blSiparisDetay = new BindingList<SiparisDetay>(_siparis.SiparisDetaylar);
            InitializeComponent();
            cboUrun.DataSource = veri.Urunler;
            dgvSiparisDetaylar.DataSource = _blSiparisDetay;
            _blSiparisDetay.ListChanged += _blSiparisDetay_ListChanged;
            MasaNoGuncelle();            
        }

        private void _blSiparisDetay_ListChanged(object sender, ListChangedEventArgs e)
        {
            OdemeTutariniGuncelle();
        }

        private void OdemeTutariniGuncelle()
        {
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo:00} (Açılış zamanı : {_siparis.AcilisZamani})";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cboUrun.SelectedIndex == -1) return;
            Urun urun = (Urun)cboUrun.SelectedItem;

            SiparisDetay sd = new SiparisDetay()
            { 
                UrunAd = urun.UrunAd, 
                BirimFiyat = urun.BirimFiyat, 
                Adet = (int)nudAdet.Value 
            };
            _blSiparisDetay.Add(sd);
            EkleFormunuSifirla();
        }

        private void EkleFormunuSifirla()
        {
            cboUrun.SelectedIndex = 0;
            nudAdet.Value = 1;
        }

        private void dgvSiparisDetaylar_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            
            DialogResult dr = MessageBox.Show(
                text : "Silmek istediğinize emin misiniz ?",
                caption : "Detay Silme Onayı", 
                buttons : MessageBoxButtons.YesNo,
                icon : MessageBoxIcon.Warning,
                defaultButton : MessageBoxDefaultButton.Button2);        //9. i overload

            e.Cancel = dr == DialogResult.No;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOdemelAl_Click(object sender, EventArgs e)
        {
            _siparis.Durum = SiparisDurum.Odendi;
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar = _siparis.ToplamTutar();
            _veri.AktifSiparisler.Remove(_siparis);
            _veri.GecmisSiparisler.Add(_siparis);
            Close();
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            _siparis.Durum = SiparisDurum.Iptal;
            _siparis.KapanisZamani = DateTime.Now;
            _veri.AktifSiparisler.Remove(_siparis);
            _veri.GecmisSiparisler.Add(_siparis);
            Close();
        }
    }
}
