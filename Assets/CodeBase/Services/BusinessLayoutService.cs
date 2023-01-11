using System;
using CodeBase.Configs;
using CodeBase.Data;
using CodeBase.Infrastructure;
using Timer = CodeBase.Infrastructure.Timer;

namespace CodeBase.Services
{
  public class BusinessLayoutService
  {
    public event Action<float, float> ProgressChanged;
    
    public BusinessConfig Config => _config;
    public bool LevelIsMax => _config.MaxLevel == GetData().Level;

    private readonly BusinessConfig _config;
    private readonly IDataStorageService _dataStorageService;
    private readonly PlayerResourcesProvider _playerResources;
    private readonly ITimerService _timerService;
    private Timer _timer;

    public BusinessLayoutService(BusinessConfig config, IDataStorageService dataStorageService,
                                 PlayerResourcesProvider playerResources, ITimerService timerService,
                                 ILifeCycle lifeCycle)
    {
      _dataStorageService = dataStorageService;
      _playerResources = playerResources;
      _timerService = timerService;
      _config = config;

      InitTimer();
      lifeCycle.ApplicationQuited += SaveProgress;
    }

    private void InitTimer()
    {
      if (GetData().Level == 0) return;

      _timer = _timerService.CreateEndlessTimer(_config.IncomeDelay, GetData().ProgressTime);
      _timer.Ticked += (current, max) => ProgressChanged?.Invoke(current,max);
      _timer.Completed += () => _playerResources.Soft += CurrentIncome();
    }

    public int TryLevelUp()
    {
      var data = GetData();
      float cost = _config.Cost * (data.Level + 1);
      
      if (_playerResources.Soft < cost)
        return data.Level;

      _playerResources.Soft -= cost;
      data.Level++;
      SetData(data);
      
      if (_timer == null) 
        InitTimer();
      
      return data.Level;
    }

    public bool IsBought(UpgradeProgression upgrade)
    {
      int index = _config.Upgrades.FindIndex(u => u.Name == upgrade.Name);
      if (index == -1)
        throw new Exception("Upgrade not found");

      return GetData().BoughtUpgradeWithIndex(index);
    }

    public bool TryBuyUpgrade(int index)
    {
      if (GetData().Level == 0) return false;
      float cost = _config.Upgrades[index].Cost;
      var data = GetData();

      if (data.BoughtUpgradeWithIndex(index)) return true;
      if (_playerResources.Soft < cost) return false;

      _playerResources.Soft -= cost;
      data.BuyUpgrade(index);

      SetData(data);
      
      return true;
    }

    public float CurrentIncome()
    {
      var data = GetData();
      float sum = 0;
      
      for (var i = 0; i < _config.Upgrades.Count; i++)
      {
        if (data.BoughtUpgradeWithIndex(i)) 
          sum += _config.Upgrades[i].Multiply;
      }

      SetData(data);
      return data.Level * _config.BaseIncome * (1 + sum);
    }

    public float CurrentCost() => 
        (GetData().Level + 1) * _config.Cost;

    public int CurrentLevel() => 
        GetData().Level;

    private void SaveProgress()
    {
      if (_timer == null) return;
      var data = GetData();
      data.ProgressTime = _timer.Current;
      SetData(data);
    }

    private void SetData(BusinessData data) => 
        _dataStorageService.SetData(_config.Key, data);

    private BusinessData GetData() => 
        _dataStorageService.GetData<BusinessData>(_config.Key);
  }
}