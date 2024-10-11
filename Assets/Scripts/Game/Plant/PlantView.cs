using UnityEngine;

public class PlantView : MonoBehaviour
{
    public PlantTypeImageDictionary plantsImageDictionary;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        plantsImageDictionary.InitDictopnary();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    public void SetPlantViewWhenStageReached(PlantType plantType, int plantStage)
    {
        spriteRenderer.sprite = plantsImageDictionary.PlantsTypeImagesDictionary[plantType].PlantsImages[plantStage];
    }
}
