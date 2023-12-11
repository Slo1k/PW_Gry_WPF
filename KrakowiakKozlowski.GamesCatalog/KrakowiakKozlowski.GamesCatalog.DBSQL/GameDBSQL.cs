using System.ComponentModel.DataAnnotations;
using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DBSQL
{
    public class GameDBSQL
    {
        [Key]
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public GameGenre Genre { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Score { get; set; }

        public IGame ToIGame(List<ProducerDBSQL> producers)
        {
            return new Game() { Id = Id, Title = Title, ReleaseYear = ReleaseYear, Score = Score, Genre = Genre, Producer = producers.Single(producer => producer.Id.Equals(ProducerId)).ToIProducer() };
        }
    }

    class Game: IGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Score { get; set; }
        public GameGenre Genre { get; set; }
        public IProducer Producer { get; set; }
    }
}
