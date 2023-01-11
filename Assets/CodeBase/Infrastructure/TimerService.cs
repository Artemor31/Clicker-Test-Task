namespace CodeBase.Infrastructure
{
  public interface ITimerService
  {
    Timer CreateTimer(float timeSpan);
    Timer CreateEndlessTimer(float timeSpan);
    Timer CreateEndlessTimer(float timeSpan, float startTime);
  }

  public class TimerService : ITimerService
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public TimerService(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

    public Timer CreateTimer(float timeSpan) => 
        new Timer(timeSpan, _coroutineRunner, false);
    
    public Timer CreateEndlessTimer(float timeSpan) => 
        new Timer(timeSpan, _coroutineRunner, true);
    
    public Timer CreateEndlessTimer(float timeSpan, float startTime) => 
        new Timer(timeSpan, _coroutineRunner, true, startTime);
  }
}