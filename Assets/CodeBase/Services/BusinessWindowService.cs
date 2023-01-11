using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure;

namespace CodeBase.Services
{
  public class BusinessWindowService
  {
    public IReadOnlyList<BusinessLayoutService> LayoutServices => _layoutServices;
    
    private readonly PlayerResourcesProvider _playerResources;
    private readonly List<BusinessLayoutService> _layoutServices = new();

    public BusinessWindowService(List<BusinessConfig> configs, IDataStorageService dataStorageService,
                                 PlayerResourcesProvider playerResources, ITimerService timerService,
                                 ILifeCycle lifeCycle)
    {
      _playerResources = playerResources;
      
      foreach (var config in configs)
      {
        var service = new BusinessLayoutService(config, dataStorageService, _playerResources, timerService, lifeCycle);
        _layoutServices.Add(service);
      }
    }
  }
}