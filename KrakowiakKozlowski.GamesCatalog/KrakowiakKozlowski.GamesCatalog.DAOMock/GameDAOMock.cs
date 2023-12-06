using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DAOMock
{
    [Serializable]
    public class GameDAOMock: IGame
    {
        public int Id {  get; set; }
        public IProducer Producer { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Score { get; set; }

        public GameGenre Genre { get; set; }
    }
}
