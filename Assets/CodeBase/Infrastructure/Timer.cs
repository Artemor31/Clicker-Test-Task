using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class Timer
  {
    public event Action<float, float> Ticked;
    public event Action Completed;
    public float Current { get; private set; }
    
    private readonly float _target;
    private readonly bool _endless;

    public Timer(float timeSpan, ICoroutineRunner coroutine, bool endless, float startTime = 0)
    {
      Current = startTime;
      _target = timeSpan;
      _endless = endless;

      if (_endless)
        coroutine.StartCoroutine(StartEndlessTimer());
      else
        coroutine.StartCoroutine(StartTimer());
    }

    private IEnumerator StartEndlessTimer()
    {
      while (true)
      {
        Tick();
        yield return null;

        if (Current >= _target)
        {
          Current = 0;
          Completed?.Invoke();
        }
      }
    }

    private IEnumerator StartTimer()
    {
      while (Current < _target)
      {
        Tick();
        yield return null;
      }
    }

    private void Tick()
    {
      Current += Time.deltaTime;
      Ticked?.Invoke(Current, _target);
    }
  }
}