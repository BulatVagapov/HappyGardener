using UnityEngine;
using UnityEngine.UI;

public class ChestPlantScreen : MonoBehaviour
{
    [SerializeField] private Image plantImage;

    public PlantType PlantType;

    public void SetImageColor(Color color)
    {
        plantImage.color = color;
    }
}
