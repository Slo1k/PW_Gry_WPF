using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakowiakKozlowski.Games.INTERFACES
{
	public interface IProducer
	{
		int Id { get; set; }
		string Name { get; set; }
		string Country { get; set; }
	}
}
