using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Button waterButton;
    [SerializeField] private Button fertilizeButton;
    [SerializeField] private PlantSeedButton plantButton;
    [SerializeField] private Button nextButton;

    void Start()
    {
        nextButton.gameObject.SetActive(false);

        waterButton.interactable = !Gardener.Instance.PlantModel.IsWatered;
        fertilizeButton.interactable = !Gardener.Instance.PlantModel.IsFertilized;

        if(Gardener.Instance.PlantModel.Stage == 0)
        {
            SetButtonGameObjectActivity(false);
        }
        else
        {
            SetButtonGameObjectActivity(true);
        }

        if (Gardener.Instance.PlantModel.Stage == Gardener.Instance.PlantController.FinalStage)
        {
            OnFinalStageReached();
        }


        waterButton.onClick.AddListener(Gardener.Instance.WaterPlant);
        fertilizeButton.onClick.AddListener(Gardener.Instance.FertilizePlant);
        plantButton.OnSeedTap += OnSeedButtonClick;

        Gardener.Instance.IsWateredEvent += () => waterButton.interactable = false;
        Gardener.Instance.IsFertilizedEvent += () => fertilizeButton.interactable = false;

        Gardener.Instance.PlantController.GrowStageReached += OnStageReached;
        Gardener.Instance.PlantController.FinalStageReached += OnFinalStageReached;

        nextButton.onClick.AddListener(Gardener.Instance.StartGameCycle);

        Gardener.Instance.GameCycleStarted += OnGameCycleStarted;
    }

    private void OnStageReached()
    {
        waterButton.interactable = true;
        fertilizeButton.interactable = true;
    }

    private void OnSeedButtonClick()
    {
        Gardener.Instance.Plant();

        SetButtonGameObjectActivity(true);
    }

    private void SetButtonGameObjectActivity(bool value)
    {
        waterButton.gameObject.SetActive(value);
        fertilizeButton.gameObject.SetActive(value);
        plantButton.gameObject.SetActive(!value);
    }

    private void OnGameCycleStarted()
    {
        nextButton.gameObject.SetActive(false);
        SetButtonGameObjectActivity(false);
    }

    private void OnFinalStageReached()
    {
        waterButton.gameObject.SetActive(false);
        fertilizeButton.gameObject.SetActive(false);
        plantButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }
}
