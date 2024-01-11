using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KrakowiakKozlowski.Games.INTERFACES;
using KrakowiakKozlowski.Games.CORE;

namespace KrakowiakKozlowski.Games.DAOMOCK.DO
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public IProducer Producer { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Score { get; set; }

        public GameGenre Genre { get; set; }
    }
}
