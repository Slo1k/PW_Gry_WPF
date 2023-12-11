using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBSQL
{
    public class ProducerDBSQL
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public IProducer ToIProducer()
        {
            return new Producer() { Id = Id, Name = Name, Country = Country };
        }
    }

    class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
