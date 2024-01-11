using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.CORE;

namespace KrakowiakKozlowski.Games.INTERFACES
{
	public interface IGame
	{
		int Id { get; set; }
		IProducer Producer { get; set; }
		string Title { get; set; }
		int ReleaseYear { get; set; }
		int Score { get; set; }

		GameGenre Genre { get; set; }
	}
}
