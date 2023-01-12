using System;
using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure;
using CodeBase.Layouts;
using CodeBase.Presenters;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Installers
{
  public class BusinessWindowInstaller : MonoBehaviour, ICoroutineRunner, ILifeCycle
  {
    public event Action ApplicationQuited;
    
    [SerializeField] private BusinessWindowLayout _layout;
    [SerializeField] private BusinessLayoutTexts _layoutTexts;
    [SerializeField] private List<BusinessConfig> _configs;

    private ITimerService _timerService;
    private IDataStorageService _dataStorageService;
    private PlayerResourcesProvider _playerResource;
    private BusinessWindowService _windowService;
    private BusinessWindowPresenter _windowPresenter;


    private void Start()
    {
      _timerService = new TimerService(this);
      _dataStorageService = new DataStorageService();
      _playerResource = new PlayerResourcesProvider(_dataStorageService);

      if (_playerResource.Soft < 1000)
        _playerResource.Soft += 1000;
      
      _windowService = new BusinessWindowService(_configs, _dataStorageService, _playerResource, _timerService, this);
      _windowPresenter = new BusinessWindowPresenter(_layout, _windowService, _layoutTexts, _playerResource);
    }

    public void OnApplicationQuit() => 
        ApplicationQuited?.Invoke();

    public void OnApplicationPause(bool pauseStatus)
    {
      if (pauseStatus)
        ApplicationQuited?.Invoke();
    }
  }
}