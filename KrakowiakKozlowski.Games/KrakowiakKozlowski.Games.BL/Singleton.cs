﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakowiakKozlowski.Games.BL
{
    public static class Singleton
    {
        public static DataAccess Instance;
        public static void SetDataAccess(string name)
        {
            Instance = new DataAccess(name);
        }
    }
}
