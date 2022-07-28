using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
       public List<LevelData> StageList = new List<LevelData>(3);
    }
}