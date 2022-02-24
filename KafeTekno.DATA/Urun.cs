using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeTekno.DATA
{
    public class Urun
    {
        public decimal BirimFiyat { get; set; }
        public string UrunAd { get; set; }
        public override string ToString()
        {
            return $"{UrunAd} ({BirimFiyat:c2})";
        }
    }
}
