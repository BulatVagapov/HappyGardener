using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantsImageDictionary", menuName = "ScriptableObjects/PlantsImageDictionary")]
public class PlantTypeImageDictionary : ScriptableObject
{
    [SerializeField] private PlantType[] plantTypes;
    [SerializeField] private PlantStageImages[] plantStageImages;

    public Dictionary<PlantType, PlantStageImages> PlantsTypeImagesDictionary;

    public void InitDictopnary()
    {
        PlantsTypeImagesDictionary = new();

        for(int i = 0; i < plantTypes.Length; i++)
        {
            PlantsTypeImagesDictionary.Add(plantTypes[i], plantStageImages[i]);
        }
    }
}
