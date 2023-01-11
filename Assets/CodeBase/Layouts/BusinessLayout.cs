using System;
using System.Collections.Generic;
using CodeBase.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Layouts
{
  public class BusinessLayout : MonoBehaviour
  {
    public IReadOnlyList<UpgradeButtonLayout> UpgradeLayouts => _upgradeLayouts;
    
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _income;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private List<UpgradeButtonLayout> _upgradeLayouts;
    [SerializeField] private ButtonLayout _levelUp;
    [SerializeField] private Image _progress;

    private BusinessLayoutTexts _texts;

    public void Initialize(BusinessLayoutTexts texts) => 
        _texts = texts;

    public void SetName(string name) => 
        _name.text = name;

    public void SetLevel(int level) => 
        _level.text = _texts.GetlevelFormat(level);

    public void SetIncome(float income) => 
        _income.text = _texts.GetIncomeFormat(income);
    
    public void SetCost(float cost) => 
        _cost.text = _texts.GetLevelUpFormat(cost);

    public void AddLevelUpListener(Action onLevelUp) => 
        _levelUp.AddOnClickListener(onLevelUp.Invoke);

    public void SetLevelUpInteractable(bool isMaxLevel) => 
        _levelUp.SetInteractable(isMaxLevel);

    public void SetProgress(float percantage) => 
        _progress.fillAmount = percantage;
  }
}