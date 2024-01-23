using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.DAOSQL.DO
{
    [Table("Producers")]
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public IProducer ToIProducer()
        {
            var prod = new Prod { Id = Id, Name = Name, Country = Country };
            return prod;
        }
    }

	class Prod : IProducer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
	}
}
