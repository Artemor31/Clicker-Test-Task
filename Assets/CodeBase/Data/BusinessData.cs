using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class BusinessData : SaveData
  {
    public int Level;
    public float ProgressTime;
    public List<int> UpgradesLevels;

    public BusinessData()
    {
      ProgressTime = 0;
      Level = 0;
      UpgradesLevels = new List<int>();
    }

    public int LevelForUpgrade(int upgrade) => 
        upgrade < UpgradesLevels.Count ? UpgradesLevels[upgrade] : 0;
  
    public bool BoughtUpgradeWithIndex(int index)
    {
      if (index >= UpgradesLevels.Count) return false;
      return UpgradesLevels[index] != 0;
    }

    public void BuyUpgrade(int index)
    {
      if (UpgradesLevels.Count > index)
      {
        UpgradesLevels[index]++;
      }
      else
      {
        int diff = 1 + index - UpgradesLevels.Count;

        for (int i = 0; i < diff; i++)
        {
          UpgradesLevels.Add(new int());
        }
        
        UpgradesLevels[index]++;
      }
    }
  }
}