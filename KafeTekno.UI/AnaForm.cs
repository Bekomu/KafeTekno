using KafeTekno.DATA;
using KafeTekno.UI.Properties;
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
    public partial class AnaForm : Form
    {
        KafeVeri db = new KafeVeri();
        public AnaForm()
        {
            InitializeComponent();
            //ImageList iList = new ImageList();
            //iList.Images.Add("bos", Resources.bos);
            //iList.Images.Add("dolu", Resources.dolu);
            //lvwMasalar.Items.Add(new ListViewItem(new[] { "01", "Adana", "Akdeniz" }, "dolu"));
            //lvwMasalar.Items.Add(new ListViewItem(new[] { "06", "Ankara", "İç Anadolu" }, "bos"));
            //lvwMasalar.Items.Add(new ListViewItem(new[] { "35", "İzmir", "Ege" }, "dolu"));
            //lvwMasalar.LargeImageList = iList;
            OrnekUrunleriYukle();
            MasalariOlustur();
        }

        private void MasalariOlustur()
        {
            lvwMasalar.LargeImageList = BuyukImajListesi();
            for (int i = 1; i <= db.MasaAdet; i++)                 // masa adedi 20 diye classta ön tanımlı
            {
                ListViewItem lvi = new ListViewItem("Masa " + i);
                lvi.ImageKey = "bos";
                lvi.Tag = i;
                lvwMasalar.Items.Add(lvi);
            }
        }

        private ImageList BuyukImajListesi()
        {
            ImageList iList = new ImageList();
            iList.ImageSize = new Size(64, 64);
            iList.Images.Add("bos", Resources.bos);
            iList.Images.Add("dolu", Resources.dolu);
            return iList;
        }

        private void OrnekUrunleriYukle()
        {
            db.Urunler.Add(new Urun() { UrunAd = "Kola", BirimFiyat = 7.00m, });
            db.Urunler.Add(new Urun() { UrunAd = "Ayran", BirimFiyat = 5.00m, });
            db.Urunler.Add(new Urun() { UrunAd = "Kahve", BirimFiyat = 11.00m, });
        }

        private void lvwMasalar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = lvwMasalar.SelectedItems[0];
            lvi.ImageKey = "dolu";
            int masaNo = (int)lvi.Tag;
            Siparis siparis = SiparisBulYaDaOlustur(masaNo);
            new SiparisForm(siparis, db).ShowDialog();
            if (siparis.Durum != SiparisDurum.Aktif)
            {
                lvi.ImageKey = "bos";
            }

        }

        private Siparis SiparisBulYaDaOlustur(int masaNo)
        {
            Siparis siparis = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == masaNo);

            if (siparis == null)
            {
                siparis = new Siparis() { MasaNo = masaNo };
                db.AktifSiparisler.Add(siparis);
            }
            return siparis;
        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            new GecmisSiparislerForm(db).ShowDialog();
        }
    }
}
