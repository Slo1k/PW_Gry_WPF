using KrakowiakKozlowski.GamesCatalog.CORE;
using KrakowiakKozlowski.GamesCatalog.INTERFACES;

namespace KrakowiakKozlowski.GamesCatalog.DAOMock
{
    [Serializable]
    public class ProducerDAOMock: IProducer
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public string Country {  get; set; }
    }
}
