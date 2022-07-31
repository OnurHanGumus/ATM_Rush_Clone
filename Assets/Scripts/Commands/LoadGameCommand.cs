using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using System.Threading.Tasks;
using Keys;

namespace Commands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveLoadStates saveLoadStates)
        {
            if (!ES3.FileExists()) return 0;
            if (saveLoadStates == SaveLoadStates.Level) return ES3.Load<int>("Level");
            else if (saveLoadStates == SaveLoadStates.Money) return ES3.Load<int>("Money");
            else return 0;
        }
    }
}
