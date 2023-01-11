using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.Layouts
{
  public class BusinessWindowLayout : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _playerSoft;
    [SerializeField] private BusinessLayout _businessLayoutPrefab;
    [SerializeField] private Transform _spawnParent;

    private List<BusinessLayout> _business = new();

    public BusinessLayout CreateNewLayout()
    {
      var businessLayout = Instantiate(_businessLayoutPrefab, Vector3.zero, Quaternion.identity, _spawnParent);
      _business.Add(businessLayout);
      return businessLayout;
    }

    public void SetPlayerSoft(string value) => 
        _playerSoft.text = value;
  }
}