using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBFile
{
    [Serializable]
    internal class GameDBFile: IGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear {  get; set; }
        public int Score { get; set; }
        public GameGenre Genre { get; set; }
        public IProducer Producer { get; set; }
    }
}
