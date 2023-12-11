using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBFile
{
    [Serializable]
    internal class ProducerDBFile: IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
