using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private PlantModel plantModel;
    private PlantView plantView;

    private Timer timer;

    [SerializeField] private int growStageTimeinMinutes;
    [SerializeField] private int finalStage;
    public Timer Timer => timer;

    public Action StartStage;
    public Action GrowStageReached;
    public Action FinalStageReached;

    public bool needTimerText = false;
    public int FinalStage => finalStage;

    public void InitPlantController()
    {
        timer = new Timer();
        timer.TimerIsOver += OnNewGrowStageIsReached;

        plantView = transform.GetComponentInChildren<PlantView>();
        plantView.SetPlantViewWhenStageReached(plantModel.PlantType, plantModel.Stage);
        GrowStageReached += () => plantView.SetPlantViewWhenStageReached(plantModel.PlantType, plantModel.Stage);
        FinalStageReached += () => plantView.SetPlantViewWhenStageReached(plantModel.PlantType, finalStage);

        if (plantModel.IsWatered && plantModel.IsFertilized)
        {
            DateTime savedDateTime = new DateTime(plantModel.StageStartDateTimeTicks);
            
            TimeSpan interval = DateTime.Now - savedDateTime;

            if (interval.TotalSeconds >= growStageTimeinMinutes * 60)
            {
                OnNewGrowStageIsReached();
            }
            else
            {
                SetEndOfGrowStage((growStageTimeinMinutes *60) - (int)interval.TotalSeconds);
                needTimerText = true;
            }
        }
    }

    public void SetPlantModel(PlantModel plantModel)
    {
        this.plantModel = plantModel;
    }

    public void StartGameCycle(PlantModel plantModel)
    {
        SetPlantModel(plantModel);
        plantView.SetPlantViewWhenStageReached(plantModel.PlantType, plantModel.Stage);
        Gardener.Instance.SavePlantModel();
    }


    public void Water()
    {
        plantModel.IsWatered = true;

        if (plantModel.IsFertilized)
        {
            StartGrowStage();
            return;
        }

        Gardener.Instance.SavePlantModel();
    }

    public void Fertilize()
    {
        plantModel.IsFertilized = true;

        if (plantModel.IsWatered)
        {
            StartGrowStage();
            return;
        }

        Gardener.Instance.SavePlantModel();
    }

    private void StartGrowStage()
    {
        plantModel.StageStartDateTimeTicks = DateTime.Now.Ticks;

        Gardener.Instance.SavePlantModel();

        SetEndOfGrowStage(growStageTimeinMinutes *60);
        StartStage?.Invoke();
    }

    private void SetEndOfGrowStage(int timeForGrounStageInSeconds)
    {
        timer.SetTimer(timeForGrounStageInSeconds);
        timer.StartTimer();
    }

    public void OnNewGrowStageIsReached()
    {
        plantModel.IsWatered = false;
        plantModel.IsFertilized = false;
        plantModel.Stage++;
        Gardener.Instance.SavePlantModel();

        if (plantModel.Stage < finalStage)
        {
            GrowStageReached?.Invoke();
        }
        else
        {
            Debug.Log("Current plant " + plantModel.PlantType);
            
            FinalStageReached?.Invoke();
        }
    }
}
