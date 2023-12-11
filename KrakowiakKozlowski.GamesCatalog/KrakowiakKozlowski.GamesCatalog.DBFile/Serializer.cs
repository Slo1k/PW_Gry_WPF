using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KrakowiakKozlowski.GamesCatalog.DBFile
{
    internal class Serializer
    {
        public static void Serialize<T>(string fileName, List<T> producers)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                new BinaryFormatter().Serialize(fs, producers);
            }
        }

        public static List<T> Deserialize<T>(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return (List<T>)new BinaryFormatter().Deserialize(fs);
            }
        }
    }
}
