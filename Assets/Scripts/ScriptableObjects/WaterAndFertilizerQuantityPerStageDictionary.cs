using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterAndFertilizerQuantityPerStageDictionary", menuName = "ScriptableObjects/WaterAndFertilizerQuantityPerStageDictionary")]
public class WaterAndFertilizerQuantityPerStageDictionary : ScriptableObject
{
    [SerializeField] private PlantType[] plantTypes;
    [SerializeField] private PlantStageWaterAndFertilizerStageQuantity[] PlantStageWaterAndFertilizer;

    public Dictionary<PlantType, PlantStageWaterAndFertilizerStageQuantity> WaterAndFertilizerQuantityDictionary;

    public void InitDictionary()
    {
        WaterAndFertilizerQuantityDictionary = new();

        for(int i = 0; i < plantTypes.Length; i++)
        {
            WaterAndFertilizerQuantityDictionary.Add(plantTypes[i], PlantStageWaterAndFertilizer[i]);
        }
    }
}
