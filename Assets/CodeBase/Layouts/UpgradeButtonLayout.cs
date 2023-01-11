using System;
using CodeBase.Configs;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Layouts
{
  public class UpgradeButtonLayout : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _income;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _boughtText;
    [SerializeField] private ButtonLayout _button;
    
    private BusinessLayoutTexts _texts;

    public void Initialize(BusinessLayoutTexts texts, UpgradeProgression config, bool isBought)
    {
      _texts = texts;
      _boughtText.text = _texts.BoughtText;
      
      SetNameText(config.Name);
      SetIncomePercantage(config.Multiply);
      SetCost(config.Cost);
      
      SetIsBought(isBought);
    }

    public void AddOnClickListener(Action action) => 
        _button.AddOnClickListener(action.Invoke);

    public void SetIsBought(bool bought)
    {
      if (bought)
      {
        _boughtText.gameObject.SetActive(true);
        _cost.gameObject.SetActive(false);
      }
      else
      {
        _boughtText.gameObject.SetActive(false);
        _cost.gameObject.SetActive(true);
      }
    }

    private void SetNameText(string headerName) =>
        _name.text = _texts.GetUpgradeNameFormat(headerName);

    private void SetIncomePercantage(float multiply) =>
        _income.text = _texts.GetUpgradeIncomeFormat(multiply * 100);

    private void SetCost(float cost) =>
        _cost.text = _texts.GetCostFormat(cost);
  }
}