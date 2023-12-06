using System.Reflection;
using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(string filePath)
        {
            LoadDatasource(filePath);
        }

        public void LoadDatasource(string filePath)
        {
            if (filePath.EndsWith(".dll"))
                LoadLibrary(filePath);
            else if (filePath.EndsWith(".db"))
                LoadSql(filePath);
        }
        public void LoadSql(string dbPath)
        {
            dao = new DAOSQL(dbPath);
        }

        public void LoadLibrary(string dllPath)
        {
            var typeToCreate = FindDAOType(dllPath);

            if (typeToCreate != null)
            {
                dao = CreateDAOInstance(typeToCreate);
            }
            else
            {
                throw new InvalidOperationException("No compatible IDAO type found in assembly.");
            }
        }

        private Type FindDAOType(string dllPath)
        {
            try
            {
                var assembly = Assembly.UnsafeLoadFrom(dllPath);
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(IDAO).IsAssignableFrom(type))
                    {
                        return type;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load assembly or find IDAO: " + ex.Message);
                throw;
            }

            return null;
        }

        private IDAO CreateDAOInstance(Type daoType)
        {
            try
            {
                return (IDAO)Activator.CreateInstance(daoType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create instance of IDAO: {daoType.Name}\n{ex.Message}");
                throw;
            }
        }

        public IEnumerable<IGame> GetAllGames() => dao.GetAllGames();
        public IEnumerable<IProducer> GetAllProducers() => dao.GetAllProducers();
        public IEnumerable<IGame> GetGame(int id) => dao.GetAllGames().Where(game => game.Id.Equals(id));
        public IEnumerable<IProducer> GetProducer(int id) => dao.GetAllProducers().Where(producer => producer.Id.Equals(id));

        public IEnumerable<IGame> GetGamesByTitle(string title) => dao.GetAllGames().Where(game => game.Title.Contains(title));
        public IEnumerable<IGame> GetGamesByYear(int year) => dao.GetAllGames().Where(game => game.ReleaseYear.Equals(year));

        public IEnumerable<IGame> GetGamesByScore(int score) => dao.GetAllGames().Where(game => game.Score.Equals(score));

        public IEnumerable<IGame> GetGamesByGenre(GameGenre genre) => dao.GetAllGames().Where(game => game.Genre.Equals(genre));

        public IEnumerable<IProducer> GetProducerByName(string name) => dao.GetAllProducers().Where(producer => producer.Name.Contains(name));

        public IEnumerable<IProducer> GetProducerByCountry(string country) => dao.GetAllProducers().Where(producer => producer.Country.Contains(country));

        public void RemoveGame(int id) => dao.RemoveGame(id);
        public void RemoveProducer(int id) => dao.RemoveProducer(id);

        public void CreateOrUpdateGame(IGame game)
        {
            if (game.Id == 0)
            {
                int maxId = dao.GetAllGames().Max(game => game.Id);
                game.Id = maxId + 1;
                dao.AddNewGame(game);
            }
            else
            {
                dao.UpdateGame(game);
            }
        }

        public void CreateOrUpdateProducer(IProducer producer) { 
            if (producer.Id == 0)
            {
                int maxId = dao.GetAllProducers().Max(producer => producer.Id);
                producer.Id = maxId + 1;
                dao.AddNewProducer(producer);
            }
            else
            {
                dao.UpdateProducer(producer);
            }
        }

        public IEnumerable<string> GetAllGamesNames() => from games in dao.GetAllGames() select games.Title;
        public IEnumerable<string> GetAllProducersNames() => from producers in dao.GetAllProducers() select producers.Name;

    }
}
