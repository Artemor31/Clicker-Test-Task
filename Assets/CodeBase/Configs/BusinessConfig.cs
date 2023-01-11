using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Configs
{
  [CreateAssetMenu(menuName = "Create BusinessConfig", fileName = "BusinessConfig", order = 0)]
  public class BusinessConfig : ScriptableObject
  {
    [field: SerializeField] public string Key { get; private set; }

    public string Name;
    public float IncomeDelay;
    public float Cost;
    public float BaseIncome;
    public int MaxLevel;
    public List<UpgradeProgression> Upgrades;
  }
}