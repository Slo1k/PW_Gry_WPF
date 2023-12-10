using System;
using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.INTERFACES
{
    public interface IGame
    {
        int Id { get; set;  }
        IProducer Producer { get; set; }
        string Title { get; set; }
        int ReleaseYear { get; set; }
        int Score { get; set; }

        GameGenre Genre { get; set; }
    }
}
