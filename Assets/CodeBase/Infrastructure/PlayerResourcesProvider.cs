using System;
using CodeBase.Data;

namespace CodeBase.Infrastructure
{
  public class PlayerResourcesProvider
  {
    private const string Key = "PlayerResources";
  
    public event Action<float> SoftChanged;

    public float Soft
    {
      get => _dataStorageService.GetData<PlayerData>(Key).Soft;
      set
      {
        var data = _dataStorageService.GetData<PlayerData>(Key);
        data.Soft = value;
        SoftChanged?.Invoke(data.Soft);
        _dataStorageService.SetData(Key, data);
      }
    }
  
    private readonly IDataStorageService _dataStorageService;
    public PlayerResourcesProvider(IDataStorageService dataStorageService)
    {
      _dataStorageService = dataStorageService;
    }

    [Serializable]
    public class PlayerData : SaveData
    {
      public float Soft;
    }
  }
}