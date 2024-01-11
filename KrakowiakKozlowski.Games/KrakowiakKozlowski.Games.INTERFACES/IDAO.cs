namespace KrakowiakKozlowski.Games.INTERFACES
{
    public interface IDAO
    {
        IEnumerable<IGame> GetAllGames();
        IEnumerable<IProducer> GetAllProducers();

		IGame AddNewGame(IGame game);
		IProducer AddNewProducer(IProducer producer);

		void UpdateGame(IGame game);
        void UpdateProducer(IProducer producer);

        void RemoveGame(int id);

        void RemoveProducer(int id);

		IGame GetGameById(int id);
		IProducer GetProducerById(int id);
	}
}
