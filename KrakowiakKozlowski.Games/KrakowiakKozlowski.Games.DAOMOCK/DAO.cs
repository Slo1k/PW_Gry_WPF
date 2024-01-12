using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.DAOMOCK.DO;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.DAOMOCK
{
    public class DAO: IDAO
    {
		private List<IGame> games;
		private List<IProducer> producers;

		public DAO()
		{
			producers = new List<IProducer>()
			{
				new Producer() {Id = 1, Name = "FromSoftware", Country = "Japan"},
				new Producer() {Id = 2, Name = "Capcom", Country = "Japan"},
				new Producer() {Id = 3, Name = "Remedy Entertainment", Country = "Finland"}
			};


			games = new List<IGame>()
			{
				new Game() {Id = 1, Title = "Elden Ring", Genre = CORE.GameGenre.RPG, ReleaseYear = 2022, Score = 96, Producer = producers[0]},
				new Game() {Id = 2, Title = "Armored Core VI", Genre = CORE.GameGenre.Action, ReleaseYear = 2023, Score = 86, Producer= producers[0]},
				new Game() {Id = 3, Title = "Resident Evil 4", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 93, Producer=producers[1]},
				new Game() {Id = 4, Title = "Alan Wake 2", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 88, Producer=producers[2]}
			};
		}

		public IEnumerable<IGame> GetAllGames()
		{
			return games;
		}

		public IEnumerable<IProducer> GetAllProducers()
		{
			return producers;
		}

		public IGame AddNewGame(int id, string title, int releaseYear, GameGenre genre, int score, int producerId)
		{
			var prod = producers.FirstOrDefault(x => x.Id == producerId);
			Game newGame = new Game()
			{
				Id = id,
				Title = title,
				ReleaseYear = releaseYear,
				Score = score,
				Genre = genre,
				Producer = prod
			};
			games.Add(newGame);
			return newGame;
		}

		public IProducer AddNewProducer(int id, string name, string country)
		{
			Producer newProducer = new Producer()
			{
				Id = id,
				Name = name,
				Country = country
			};
			producers.Add(newProducer);
			return newProducer;
		}

		public void RemoveGame(int id)
		{
			games.RemoveAll(game => game.Id.Equals(id));
		}

		public void RemoveProducer(int id)
		{
			producers.RemoveAll(producer => producer.Id.Equals(id));
		}

		public void UpdateGame(IGame game)
		{
			var index = games.FindIndex(gm => gm.Id.Equals(game.Id));
			if (index != -1)
			{
				games[index] = game;
			}
		}

		public void UpdateProducer(IProducer producer)
		{
			var index = producers.FindIndex(prd => prd.Id.Equals(producer.Id));
			if (index != -1)
			{
				producers[index] = producer;
			}
		}

		public IGame GetGameById(int id)
		{
			return games.FirstOrDefault(game => game.Id == id);
		}

		public IProducer GetProducerById(int id)
		{
			return producers.FirstOrDefault(producer => producer.Id == id);
		}

	}
}
