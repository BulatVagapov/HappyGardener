using System;
using UnityEngine;
using UnityEngine.UI;

public class ChestScreen : SingletonBase<ChestScreen>
{
    private const string KeyForSaving = "GrowedPlants";
    [SerializeField] private ChestPlantScreen[] plantScreens;
    private ChestArrayKeeper growedPlantIndexies;
    private int currentVisiblePlantScreen = 0;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openButton;


    protected override void Awake()
    {
        base.Awake();
        LoadGrowedInexies();
    }

    private void Start()
    {
        openButton.gameObject.SetActive(false);

        for (int i = 0; i < growedPlantIndexies.GrowedPlantIndexies.Length; i++)
        {
            if (growedPlantIndexies.GrowedPlantIndexies[i] == 1)
            {
                openButton.gameObject.SetActive(true);
                break;
            }
        }

        SetPlantScreens();
        nextButton.onClick.AddListener(OnNextButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        Gardener.Instance.PlantController.FinalStageReached += AddPlantInChest;
        openButton.onClick.AddListener(OpenChest);

        for (int i = 1; i < plantScreens.Length; i++)
        {
            plantScreens[i].gameObject.SetActive(false);
        }

        plantScreens[0].gameObject.SetActive(true);

        backButton.gameObject.SetActive(false);

        OnCloseButtonClick();
    }

    private void LoadGrowedInexies()
    {
        growedPlantIndexies = JsonUtility.FromJson<ChestArrayKeeper>(PlayerPrefs.GetString(KeyForSaving));

        if(growedPlantIndexies == null)
        {
            growedPlantIndexies = new();
            growedPlantIndexies.GrowedPlantIndexies = new int[Enum.GetValues(typeof(PlantType)).Length];

        }
    }

    private void SetPlantScreens()
    {
        for(int i = 0; i < plantScreens.Length; i++)
        {
            if(growedPlantIndexies.GrowedPlantIndexies[i] == 1)
            {
                plantScreens[i].SetImageColor(Color.white);
            }
            else
            {
                plantScreens[i].SetImageColor(Color.black);
            }

            plantScreens[i].PlantType = (PlantType)i;
        }
    }

    public void AddPlantInChest()
    {
        for (int i = 0; i < plantScreens.Length; i++)
        {
            if (plantScreens[i].PlantType.Equals(Gardener.Instance.PlantModel.PlantType))
            {
                if(growedPlantIndexies.GrowedPlantIndexies[i] == 0)
                {
                    growedPlantIndexies.GrowedPlantIndexies[i] = 1;
                    SaveGrowedPlantIndexies();
                    return;
                }
            }
        }
    }

    private void OnNextButtonClick()
    {
        if (currentVisiblePlantScreen == plantScreens.Length - 1) return;

        if (currentVisiblePlantScreen == 0) backButton.gameObject.SetActive(true);

        plantScreens[currentVisiblePlantScreen].gameObject.SetActive(false);
        plantScreens[currentVisiblePlantScreen + 1].gameObject.SetActive(true);
        currentVisiblePlantScreen++;

        if (currentVisiblePlantScreen == plantScreens.Length - 1) nextButton.gameObject.SetActive(false);
    }

    private void OnBackButtonClick()
    {
        if (currentVisiblePlantScreen == 0) return;

        if (currentVisiblePlantScreen == plantScreens.Length - 1) nextButton.gameObject.SetActive(true);

        plantScreens[currentVisiblePlantScreen].gameObject.SetActive(false);
        plantScreens[currentVisiblePlantScreen - 1].gameObject.SetActive(true);
        currentVisiblePlantScreen--;

        if (currentVisiblePlantScreen == 0) backButton.gameObject.SetActive(false);
    }

    public void OpenChest()
    {
        for (int i = 0; i < plantScreens.Length; i++)
        {
            if (growedPlantIndexies.GrowedPlantIndexies[i] == 1)
            {
                plantScreens[i].SetImageColor(Color.white);
            }
            else
            {
                plantScreens[i].SetImageColor(Color.black);
            }
        }

        gameObject.SetActive(true);
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }

    private void SaveGrowedPlantIndexies()
    {
        PlayerPrefs.SetString(KeyForSaving, JsonUtility.ToJson(growedPlantIndexies));
    }
}
