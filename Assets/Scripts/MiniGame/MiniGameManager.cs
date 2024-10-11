using System;
using UnityEngine;

public class MiniGameManager : SingletonBase<MiniGameManager>
{
    private Timer miniGameTimer;
    [SerializeField] private ConsumablesKeeper consumablesKeeper;
    [SerializeField] private ConsumablesSpawner consumablesSpawner;
    [SerializeField] private int miniGameTime;

    public ConsumablesType ConsumablesType;

    public int GotNumberOfConsumables;

    public event Action EndOfMiniGameEvent;

    public Timer MiniGameTimer => miniGameTimer;

    public void EndMiniGame()
    {
        consumablesSpawner.StopSpawn();
        Gardener.Instance.AddConsumables(ConsumablesType, GotNumberOfConsumables);
        EndOfMiniGameEvent?.Invoke();
    }

    public void StartMiniGame(ConsumablesType type)
    {
        ConsumablesType = type;
        GotNumberOfConsumables = 0;
        consumablesKeeper.SetCurrentPool(ConsumablesType);
        consumablesSpawner.StartSpawn();
        miniGameTimer.StartTimer();
    }
    protected override void Awake()
    {
        base.Awake();
        miniGameTimer = new Timer();
        miniGameTimer.SetTimer(miniGameTime);
        miniGameTimer.TimerIsOver += EndMiniGame;
    }
}
