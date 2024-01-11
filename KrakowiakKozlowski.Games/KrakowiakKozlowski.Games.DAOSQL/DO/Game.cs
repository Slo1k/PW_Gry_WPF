using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.DAOSQL.DO
{
	[Table("Games")]
	public class Game
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public int ReleaseYear { get; set; }
		public int Score { get; set; }

		public GameGenre Genre { get; set; }

		public int ProducerID { get; set; }

		public IGame ToIGame(List<Producer> producers)
		{
			return new Gm() { Id = Id, Title = Title, ReleaseYear = ReleaseYear, Score = Score, Genre = Genre, Producer = producers.Single(producer => producer.Id.Equals(ProducerID)).ToIProducer() };
		}
	}

	class Gm: IGame
	{
		public int Id { get; set; }
		public IProducer Producer { get; set; }
		public string Title { get; set; }
		public int ReleaseYear { get; set; }
		public int Score { get; set; }

		public GameGenre Genre { get; set; }
	}
}
