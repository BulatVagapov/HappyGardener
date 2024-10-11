using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text needfullWaterQuantityText;
    [SerializeField] private TMP_Text needfullFertilizerQuantityText;
    [SerializeField] private Button settingsButton;

    [SerializeField] private GameObject tapToSeedText;
    [SerializeField] private GameObject endOfGameCycleText;
    [SerializeField] private GameObject topblock;
    [SerializeField] private GameObject settingsScreen;

    private int hours;
    private int minutes;
    private int seconds;
    private string hoursString;
    private string minutesString;
    private string secondsString;

    private void Awake()
    {
        timerText.gameObject.SetActive(false);
    }

    void Start()
    {
        if(Gardener.Instance.PlantModel.Stage == 0)
        {
            tapToSeedText.SetActive(true);
            topblock.SetActive(false);
        }
        else
        {
            tapToSeedText.SetActive(false);
            topblock.SetActive(true);
            SetConsumablesNeedfullQuantigtyTexts();
        }

        if (Gardener.Instance.PlantModel.Stage == Gardener.Instance.PlantController.FinalStage)
        {
            endOfGameCycleText.SetActive(true);
        }
        else
        {
            endOfGameCycleText.SetActive(false);
        }

        if (Gardener.Instance.PlantController.needTimerText)
            timerText.gameObject.SetActive(true);

        Gardener.Instance.PlantIsPlanted += OnPlantIsPlanted;


        Gardener.Instance.PlantController.StartStage += () => timerText.gameObject.SetActive(true);

        Gardener.Instance.PlantController.GrowStageReached += OnGrownStageReached;

        Gardener.Instance.PlantController.FinalStageReached += OnFinalStageReached;

        Gardener.Instance.GameCycleStarted += OnGameCycleStarted;

        settingsButton.onClick.AddListener(() => settingsScreen.SetActive(true));

        Gardener.Instance.PlantController.Timer.CurrentTimeIsChanged += SetTimerText;
        SetTimerText(Gardener.Instance.PlantController.Timer.CurrentTime);


    }

    private void OnPlantIsPlanted()
    {
        tapToSeedText.SetActive(false);
        topblock.SetActive(true);
    }

    private void OnGameCycleStarted()
    {
        endOfGameCycleText.SetActive(false);
        tapToSeedText.SetActive(true);
        topblock.SetActive(false);
    }

    private void OnGrownStageReached()
    {
        timerText.gameObject.SetActive(false);
        SetConsumablesNeedfullQuantigtyTexts();
    }

    private void OnFinalStageReached()
    {
        timerText.gameObject.SetActive(false);
        endOfGameCycleText.SetActive(true);
    }

    private void SetConsumablesNeedfullQuantigtyTexts()
    {
        needfullWaterQuantityText.text = "x" + Gardener.Instance.WaterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[Gardener.Instance.PlantModel.PlantType].WaterAndFertilizerQuantities[Gardener.Instance.PlantModel.Stage - 1].WaterQuantity;
        needfullFertilizerQuantityText.text = "x" + Gardener.Instance.WaterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[Gardener.Instance.PlantModel.PlantType].WaterAndFertilizerQuantities[Gardener.Instance.PlantModel.Stage - 1].FertilizerQuantity;
    }

    private void SetTimerText(int timeInSeconds)
    {
        hours = timeInSeconds / 3600;
        minutes = (timeInSeconds - (hours * 3600))  / 60;
        seconds = timeInSeconds % 60;

        hoursString = $"{(hours < 10 ? "0" + hours : hours)}";
        minutesString = $"{(minutes < 10 ? "0" + minutes : minutes)}";
        secondsString = $"{(seconds < 10 ? "0" + seconds : seconds)}";

        timerText.text = hoursString + ":" + minutesString + ":" + secondsString;
    }
}
