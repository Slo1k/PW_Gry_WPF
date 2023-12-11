using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBFile
{
    internal class DAOFile : IDAO
    {
        private List<IProducer> producers;
        private List<IGame> games;

        private const string FILE_PRODUCERS = "producers.bin";
        private const string FILE_GAMES = "games.bin";

        public DAOFile()
        {
            try
            {
                producers = Serializer.Deserialize<IProducer>(FILE_PRODUCERS);
                games = Serializer.Deserialize<IGame>(FILE_GAMES);
            }
            catch (Exception)
            {
                producers = new List<IProducer>();


                AddNewProducer(new ProducerDBFile() { Id = 1, Name = "FromSoftware", Country = "Japan" });
                AddNewProducer(new ProducerDBFile() { Id = 2, Name = "Capcom", Country = "Japan" });
                AddNewProducer(new ProducerDBFile() { Id = 2, Name = "Remedy Entertainment", Country = "Finland" });

                SaveProducers();

                games = new List<IGame>();

                AddNewGame(new GameDBFile() { Id = 1, Title = "Elden Ring", Genre = CORE.GameGenre.RPG, ReleaseYear = 2022, Score = 96, Producer = producers[0] });
                AddNewGame(new GameDBFile() { Id = 2, Title = "Armored Core VI", Genre = CORE.GameGenre.Action, ReleaseYear = 2023, Score = 86, Producer = producers[0] });
                AddNewGame(new GameDBFile() { Id = 3, Title = "Resident Evil 4", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 93, Producer = producers[1] });
                AddNewGame(new GameDBFile() { Id = 4, Title = "Alan Wake 2", Genre = CORE.GameGenre.Horror, ReleaseYear = 2023, Score = 88, Producer = producers[2] });

                SaveGames();
            }
        }

        private void SaveProducers()
        {
            Serializer.Serialize(FILE_PRODUCERS, producers);
        }

        private void SaveGames()
        {
            Serializer.Serialize(FILE_GAMES, games);
        }

        public IProducer AddNewProducer(IProducer producer)
        {
            producer.Id = producers.Max(prod => prod.Id) + 1;
            producers.Add(producer);
            SaveProducers();
            return producer;
        }

        public IGame AddNewGame(IGame game)
        {
            game.Id = games.Max(gm => gm.Id) + 1;
            games.Add(game);
            SaveGames();
            return game;
        }

        public IEnumerable<IProducer> GetAllProducers() => producers;
        public IEnumerable<IGame> GetAllGames() => games;

        public void RemoveProducer(int id)
        {
            producers.RemoveAll(prod => prod.Id.Equals(id));
            SaveProducers() ;
        }

        public void RemoveGame(int id)
        {
            games.RemoveAll(gm => gm.Id.Equals(id));
            SaveGames();
        }

        public void UpdateProducer(IProducer producer)
        {
            var index = producers.FindIndex(prod => prod.Id.Equals(producer.Id));
            if (index == -1)
            {
                producers[index] = producer;
            }
            SaveProducers();
        }

        public void UpdateGame(IGame game)
        {
            var index = games.FindIndex(gm => gm.Id.Equals(game.Id));
            if (index == -1)
            {
                games[index] = game;
            }
            SaveGames();
        }
    }
}
