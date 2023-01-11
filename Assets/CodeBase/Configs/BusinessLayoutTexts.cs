using UnityEngine;

namespace CodeBase.Configs
{
  [CreateAssetMenu(menuName = "Create BusinessLayoutTexts", fileName = "BusinessLayoutTexts", order = 0)]
  public class BusinessLayoutTexts : ScriptableObject
  {
    [SerializeField][TextArea] private string _upgradeName;
    [SerializeField][TextArea] private string _level;
    [SerializeField][TextArea] private string _income;
    [SerializeField][TextArea] private string _cost;
    [SerializeField][TextArea] private string _upgradeIncome;
    [SerializeField][TextArea] private string _boughtText;
    [SerializeField] [TextArea] private string _playerSoftFormat;
    [SerializeField] [TextArea] private string _levelUpFormat;
    
    public string BoughtText => _boughtText;

    public string GetUpgradeNameFormat(string name) => 
        string.Format(_upgradeName, name);

    public string GetlevelFormat(float level) => 
        string.Format(_level, level);
    
    public string GetIncomeFormat(float income) => 
        string.Format(_income, income);
    
    public string GetCostFormat(float cost) => 
        string.Format(_cost, cost);
    
    public string GetUpgradeIncomeFormat(float income) => 
        string.Format(_upgradeIncome, income);
    
    public string GetPlayerSoftFormat(float soft) => 
        string.Format(_playerSoftFormat, soft);
    
    public string GetLevelUpFormat(float cost) => 
        string.Format(_levelUpFormat, cost);

    private void OnValidate()
    {
      return;
      string upgradeNameFormat = GetUpgradeNameFormat("NAME");
      Debug.Log(upgradeNameFormat);

      string getlevelFormat = GetlevelFormat(12);
      Debug.Log(getlevelFormat);

      string incomeFormat = GetIncomeFormat(12);
      Debug.Log(incomeFormat);

      string costFormat = GetCostFormat(12);
      Debug.Log(costFormat);
      
      string upgradeIncomeFormat = GetUpgradeIncomeFormat(12);
      Debug.Log(upgradeIncomeFormat);
    }
  }
}