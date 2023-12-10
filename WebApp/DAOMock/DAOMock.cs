using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DAOMock
{
    public class DAOMock: IDAO
    {
        private List<IGame> games;
        private List<IProducer> producers;

        public DAOMock()
        {
            producers = new List<IProducer>()
            {
                new ProducerDAOMock() {Id = 1, Name = "FromSoftware", Country = "Japan"},
                new ProducerDAOMock() {Id = 2, Name = "Capcom", Country = "Japan"},
                new ProducerDAOMock() {Id = 3, Name = "Remedy Entertainment", Country = "Finland"}
            };


            games = new List<IGame>()
            {
                new GameDAOMock() {Id = 1, Title = "Elden Ring", Genre = CORE.GameGenre.RPG, ReleaseYear = 2022, Score = 96, Producer = producers[0]},
                new GameDAOMock() {Id = 2, Title = "Armored Core VI", Genre = CORE.GameGenre.Action, ReleaseYear = 2023, Score = 86, Producer= producers[0]},
                new GameDAOMock() {Id = 3, Title = "Resident Evil 4", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 93, Producer=producers[1]},
                new GameDAOMock() {Id = 4, Title = "Alan Wake 2", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 88, Producer=producers[2]}
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

        public IGame AddNewGame(IGame game)
        {
            games.Add(game);
            return game;
        }

        public IProducer AddNewProducer(IProducer producer)
        {
            producers.Add(producer);
            return producer;
        }

        public void RemoveGame(int id) {
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

    }


}
