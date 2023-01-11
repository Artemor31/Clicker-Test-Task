using CodeBase.Configs;
using CodeBase.Layouts;
using CodeBase.Services;

namespace CodeBase.Presenters
{
  public class BusinessLayoutPresenter
  {
    private readonly BusinessLayout _businessLayout;
    private readonly BusinessLayoutService _service;

    public BusinessLayoutPresenter(BusinessLayout businessLayout, 
                                   BusinessLayoutService service,
                                   BusinessLayoutTexts texts)
    {
      _businessLayout = businessLayout;
      _service = service;

      InitLayout();
      InitializeUpgrades(texts);

      _service.ProgressChanged += ServiceOnProgressChanged;
    }

    private void InitLayout()
    {
      _businessLayout.AddLevelUpListener(OnLevelUp);
      _businessLayout.SetName(_service.Config.Name);
      _businessLayout.SetLevel(_service.CurrentLevel());
      _businessLayout.SetLevelUpInteractable(_service.LevelIsMax == false);
      _businessLayout.SetIncome(_service.CurrentIncome());
      _businessLayout.SetCost(_service.CurrentCost());
    }

    private void ServiceOnProgressChanged(float current, float max)
    {
      float percantage = current / max;
      _businessLayout.SetProgress(percantage);
    }

    private void InitializeUpgrades(BusinessLayoutTexts texts)
    {
      for (var i = 0; i < _service.Config.Upgrades.Count; i++)
      {
        var upgrade = _service.Config.Upgrades[i];
        var layout = _businessLayout.UpgradeLayouts[i];

        layout.Initialize(texts, upgrade, _service.IsBought(upgrade));
        
        int index = i;
        layout.AddOnClickListener(()=> OnUpgradeClicked(index));
      }
    }

    private void OnUpgradeClicked(int index)
    {
      bool bought = _service.TryBuyUpgrade(index);
      _businessLayout.UpgradeLayouts[index].SetIsBought(bought);
      _businessLayout.SetIncome(_service.CurrentIncome());
    }

    private void OnLevelUp()
    {
      var newLevel = _service.TryLevelUp();
      _businessLayout.SetLevel(newLevel);
      _businessLayout.SetLevelUpInteractable(_service.LevelIsMax == false);
      _businessLayout.SetIncome(_service.CurrentIncome());
      _businessLayout.SetCost(_service.CurrentCost());
    }
  }
}