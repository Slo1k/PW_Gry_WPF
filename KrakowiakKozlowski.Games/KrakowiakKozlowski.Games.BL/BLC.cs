using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.INTERFACES;
using Microsoft.Extensions.Configuration;


namespace KrakowiakKozlowski.Games.BL
{
    public class BLC
    {
        private static BLC instance;
        private static readonly object lockObject = new object();
        public IDAO DAO { get; set; }

        private BLC(string libraryName)
        {
            var assembly = Assembly.UnsafeLoadFrom(libraryName);
            var typeToCreate = assembly.GetTypes().FirstOrDefault(type => type.IsAssignableTo(typeof(IDAO)));

            try
            {
                DAO = (IDAO)Activator.CreateInstance(typeToCreate, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create instance of IDao: {typeToCreate}\n{ex.Message}");
            }
        }

        public static BLC Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();
                            var libraryName = configuration["AppSettings:DaoLibraryName"];

                            instance = new BLC(libraryName);
                        }
                    }
                }

                return instance;
            }
        }

        public IEnumerable<IGame> Games
        {
            get { return DAO.GetAllGames(); }
        }
        public IEnumerable<IProducer> Producers
        {
            get { return DAO.GetAllProducers(); }
        }

        public IGame AddNewGame(int id, string title, int releaseYear, GameGenre genre, int score, int producerId) => DAO.AddNewGame(id, title, releaseYear, genre, score, producerId);

        public IProducer AddNewProducer(int id, string name, string country) => DAO.AddNewProducer(id, name, country);

        public void UpdateGame(IGame game) => DAO.UpdateGame(game);

        public void UpdateProducer(IProducer producer) => DAO.UpdateProducer(producer);

        public void RemoveGame(int id) => DAO.RemoveGame(id);


        public void RemoveProducer(int id) => DAO.RemoveProducer(id);

        public IGame GetGameById(int id) => DAO.GetGameById(id);

        public IProducer GetProducerById(int id) => DAO.GetProducerById(id);

    }
}
