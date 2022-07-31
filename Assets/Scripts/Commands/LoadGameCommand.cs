using Enums;

namespace Commands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveLoadStates saveLoadStates)
        {
           
            if (!ES3.FileExists() || !ES3.KeyExists("Level") || !ES3.KeyExists("Money")) return 0;
            if (saveLoadStates == SaveLoadStates.Level) return ES3.Load<int>("Level");
            else if (saveLoadStates == SaveLoadStates.Money) return ES3.Load<int>("Money");
            else return 0;
        }
    }
}
