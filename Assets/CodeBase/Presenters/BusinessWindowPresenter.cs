using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Infrastructure;
using CodeBase.Layouts;
using CodeBase.Services;

namespace CodeBase.Presenters
{
  public class BusinessWindowPresenter
  {
    private readonly BusinessWindowService _windowService;
    private readonly BusinessWindowLayout _windowLayout;
    private readonly BusinessLayoutTexts _texts;

    private List<BusinessLayoutPresenter> _presenters = new();

    public BusinessWindowPresenter(BusinessWindowLayout windowLayout,
                                   BusinessWindowService windowService, 
                                   BusinessLayoutTexts texts,
                                   PlayerResourcesProvider resourcesProvider)
    {
      _texts = texts;
      _windowLayout = windowLayout;
      _windowService = windowService;

      CreateLayoutsAndPresenter();
      InitSoftView(texts, resourcesProvider);
    }

    private void InitSoftView(BusinessLayoutTexts texts, PlayerResourcesProvider resourcesProvider)
    {
      resourcesProvider.SoftChanged += value => _windowLayout.SetPlayerSoft(texts.GetPlayerSoftFormat(value));
      _windowLayout.SetPlayerSoft(texts.GetPlayerSoftFormat(resourcesProvider.Soft));
    }

    private void CreateLayoutsAndPresenter()
    {
      foreach (var service in _windowService.LayoutServices)
      {
        var layout = _windowLayout.CreateNewLayout();
        layout.Initialize(_texts);
        
        var presenter = new BusinessLayoutPresenter(layout, service, _texts);
        _presenters.Add(presenter);
      }
    }
  }
}