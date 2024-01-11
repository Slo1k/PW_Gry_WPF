using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.BL
{
    public class DataAccess
    {
        public IDAO DAO { get; set; }

        public DataAccess(string libraryName)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type typeToCreate = null;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }

            DAO = (IDAO)Activator.CreateInstance(typeToCreate, null);
        }

        public IEnumerable<IGame> Games
        {
            get { return DAO.GetAllGames(); }
        }
        public IEnumerable<IProducer> Producers
        {
            get { return DAO.GetAllProducers(); }
        }

        public IGame AddNewGame(IGame game) => DAO.AddNewGame(game);

        public IProducer AddNewProducer(IProducer producer) => DAO.AddNewProducer(producer);

        public void UpdateGame(IGame game) => DAO.UpdateGame(game);

        public void UpdateProducer(IProducer producer) => DAO.UpdateProducer(producer);

        public void RemoveGame(int id) => DAO.RemoveGame(id);


        public void RemoveProducer(int id) => DAO.RemoveProducer(id);

        public IGame GetGameById(int id) => DAO.GetGameById(id);

        public IProducer GetProducerById(int id) => DAO.GetProducerById(id);

    }
}
