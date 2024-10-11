using Cysharp.Threading.Tasks;
using System;

public class Timer
{
    private int currentTime;
    private int timerTime;

    public int CurrentTime => currentTime;

    public Action TimerIsOver;
    public Action<int> CurrentTimeIsChanged;

    public void SetTimer(int timeInSecunds)
    {
        timerTime = timeInSecunds;
        currentTime = timerTime;
        CurrentTimeIsChanged?.Invoke(currentTime);
    }

    public void StartTimer()
    {
        currentTime = timerTime;
        TimerAsync().Forget();
    }

    private async UniTask TimerAsync()
    {
        while(currentTime > 0)
        {
            await UniTask.Delay(1000);

            currentTime--;
            CurrentTimeIsChanged?.Invoke(currentTime);
        }

        TimerIsOver?.Invoke();
    }
}
