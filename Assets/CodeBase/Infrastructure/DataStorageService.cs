using System;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public interface IDataStorageService
  {
    T GetData<T>(string key) where T : SaveData, new();
    void SetData<T>(string key, T data) where T : SaveData, new();
  }

  public class DataStorageService : IDataStorageService
  {
    public T GetData<T>(string key) where T : SaveData, new()
    {
      if (string.IsNullOrEmpty(key))
        throw new Exception("Key is null or empty");

      string data = PlayerPrefs.GetString(key);

      if (string.IsNullOrEmpty(data))
        return new T();
      
      return JsonUtility.FromJson<T>(data);
    }
    
    public void SetData<T>(string key, T data) where T : SaveData, new()
    {
      if (string.IsNullOrEmpty(key))
        throw new Exception("Key is null or empty");

      string json = JsonUtility.ToJson(data);
      PlayerPrefs.SetString(key, json);
    }
  }
}