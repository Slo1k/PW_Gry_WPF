using KrakowiakKozlowski.Games.CORE;
using KrakowiakKozlowski.Games.INTERFACES;

namespace KrakowiakKozlowski.Games.INTERFACES
{
    public interface IDAO
    {
        IEnumerable<IGame> GetAllGames();
        IEnumerable<IProducer> GetAllProducers();

		IGame AddNewGame(int id, string title, int releaseYear, GameGenre genre, int score, int producerId);
		IProducer AddNewProducer(int id, string name, string country);

		void UpdateGame(IGame game);
        void UpdateProducer(IProducer producer);

        void RemoveGame(int id);

        void RemoveProducer(int id);

		IGame GetGameById(int id);
		IProducer GetProducerById(int id);
	}
}