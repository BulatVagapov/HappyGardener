using System;
using UnityEngine;

public class Gardener : SingletonBase<Gardener>
{
    private const string PlaneDataSaveKey = "PlantData";
    private const string WaterQuantityDataSaveKey = "WaterQuantityData";
    private const string FertilizerQuantityDataSaveKey = "FertilizerQuantityData";
    private int plantsCount;
    [SerializeField] private PlantController plantController;

    private PlantModel plantModel;

    [SerializeField] private int waterQuantity;
    [SerializeField] private int fertilizerQuantity;
    [SerializeField] private WaterAndFertilizerQuantityPerStageDictionary waterAndFertilizerNeedfullQuantity;

    public PlantController PlantController => plantController;
    public PlantModel PlantModel => plantModel;

    public  WaterAndFertilizerQuantityPerStageDictionary WaterAndFertilizerNeedfullQuantity => waterAndFertilizerNeedfullQuantity;

    public event Action GameCycleStarted;
    public event Action PlantIsPlanted;
    
    public event Action IsWateredEvent;
    public event Action IsFertilizedEvent;

    public event Action NeedMoreWaterEvent;
    public event Action NeedMoreFertilizerEvent;

    protected override void Awake()
    {
        base.Awake();

        GetPlantModel();
        plantController.SetPlantModel(plantModel);
        plantController.InitPlantController();
        waterAndFertilizerNeedfullQuantity.InitDictionary();
        plantsCount = Enum.GetValues(typeof(PlantType)).Length;
        LoadWaterAndFertilizerData();
    }

    public void StartGameCycle()
    {
        plantModel = new PlantModel((PlantType)UnityEngine.Random.Range(0, plantsCount));
        plantController.StartGameCycle(plantModel);
        GameCycleStarted?.Invoke();
    }

    public void Plant()
    {
        if (plantModel.Stage != 0) return;
        plantController.OnNewGrowStageIsReached();
        PlantIsPlanted?.Invoke();
    }

    public void WaterPlant()
    {
        if(waterQuantity < waterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[plantModel.PlantType].WaterAndFertilizerQuantities[plantModel.Stage].WaterQuantity)
        {
            NeedMoreWaterEvent?.Invoke();
            return;
        }
        
        plantController.Water();
        waterQuantity -= waterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[plantModel.PlantType].WaterAndFertilizerQuantities[plantModel.Stage].WaterQuantity;
        PlayerPrefs.SetInt(WaterQuantityDataSaveKey, waterQuantity);
        IsWateredEvent?.Invoke();
    }

    public void FertilizePlant()
    {
        if(fertilizerQuantity < waterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[plantModel.PlantType].WaterAndFertilizerQuantities[plantModel.Stage].FertilizerQuantity)
        {
            NeedMoreFertilizerEvent?.Invoke();
            return;
        }
        
        plantController.Fertilize();
        fertilizerQuantity -= waterAndFertilizerNeedfullQuantity.WaterAndFertilizerQuantityDictionary[plantModel.PlantType].WaterAndFertilizerQuantities[plantModel.Stage].FertilizerQuantity;
        PlayerPrefs.SetInt(FertilizerQuantityDataSaveKey, fertilizerQuantity);
        IsFertilizedEvent?.Invoke();
    }

    private void GetPlantModel()
    {
        plantModel = JsonUtility.FromJson<PlantModel>(PlayerPrefs.GetString(PlaneDataSaveKey));

        if (plantModel == null)
            plantModel = new PlantModel(0);
    }

    public void AddConsumables(ConsumablesType type, int quantity)
    {
        switch (type)
        {
            case ConsumablesType.Water:
                waterQuantity += quantity;
                PlayerPrefs.SetInt(WaterQuantityDataSaveKey, waterQuantity);
                break;
            case ConsumablesType.Fertilizer:
                fertilizerQuantity += quantity;
                PlayerPrefs.SetInt(FertilizerQuantityDataSaveKey, fertilizerQuantity);
                break;
        }
    }

    public void SavePlantModel()
    {
        PlayerPrefs.SetString(PlaneDataSaveKey, JsonUtility.ToJson(plantModel));
    }

    private void LoadWaterAndFertilizerData()
    {
        waterQuantity = PlayerPrefs.GetInt(WaterQuantityDataSaveKey, 10);
        fertilizerQuantity = PlayerPrefs.GetInt(FertilizerQuantityDataSaveKey, 10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) PlayerPrefs.DeleteAll();
    }
}
