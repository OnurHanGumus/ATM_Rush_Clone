using Keys;

namespace Commands
{
    public class SaveGameCommand
    {
        public void OnSaveGameData(SaveGameDataParams saveDataParams)
        {
            if (saveDataParams.Level != null)
            {
                ES3.Save("Level", saveDataParams.Level);
            }
            if (saveDataParams.Money != null)
            {
                int totalScore = saveDataParams.Money;
                ES3.Save("Money", totalScore);
            }
        }
    }
}
