using System;

[Serializable]
public class PlantModel
{
    public PlantType PlantType;
    public int Stage;
    public bool IsWatered;
    public bool IsFertilized;
    public long StageStartDateTimeTicks;

    public PlantModel(PlantType plantType)
    {
        PlantType = plantType;
        Stage = 0;
        IsWatered = false;
        IsFertilized = false;
        StageStartDateTimeTicks = DateTime.Now.Ticks;
    }
}
