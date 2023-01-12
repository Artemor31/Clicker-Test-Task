using System;

namespace CodeBase.Infrastructure
{
  public interface ILifeCycle
  {
    event Action ApplicationQuited;
  }
}