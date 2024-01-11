using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.DAOMOCK.DO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
    }
}
