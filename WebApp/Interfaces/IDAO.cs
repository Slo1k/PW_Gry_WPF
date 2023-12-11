using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakowiakKozlowski.GamesCatalog.INTERFACES
{
    public interface IDAO
    {
        IEnumerable<IGame> GetAllGames();
        IEnumerable<IProducer> GetAllProducers();
        IGame AddNewGame(IGame game);
        IProducer AddNewProducer(IProducer producer);

        void RemoveGame(int id);
        void RemoveProducer(int id);

        void UpdateGame(IGame game);
        void UpdateProducer(IProducer producer);

        IGame GetGameById(int id);
        IProducer GetProducerById(int id);
    }
}
