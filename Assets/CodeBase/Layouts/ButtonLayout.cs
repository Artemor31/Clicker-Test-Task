using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Layouts
{
  public class ButtonLayout : MonoBehaviour
  {
    [SerializeField] private Button _button;

    public void AddOnClickListener(UnityAction listener)
    {
      GetButtonClickedEvent().AddListener(listener);
    }
  
    public void SetInteractable(bool interactable)
    {
      GetButton().interactable = interactable;
    }
  
    public void RemoveAllListeners()
    {
      GetButtonClickedEvent().RemoveAllListeners();
    }

    private Button.ButtonClickedEvent GetButtonClickedEvent()
    {
      try
      {
        return GetButton().onClick;
      }
      catch (Exception e)
      {
        _button = GetComponent<Button>();
        return GetButton().onClick;
      }
    }

    private Button GetButton()
    {
      return _button;
    }

    protected void OnDestroy()
    {
      RemoveAllListeners();
      _button = null;
    }
  }
}