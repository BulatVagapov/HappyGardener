using UnityEngine;
using TMPro;

public class StartMiniGameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text miniGameText;

    private string noEnoughtWaterText = "You don’t have enought water to continue. Play a mini game";
    private string noEnoughtFertilizer = "You don’t have enought fertilizer to continue. Play a mini game";

    public void SetStartMiniGameText(ConsumablesType type)
    {
        switch (type)
        {
            case ConsumablesType.Water:
                miniGameText.text = noEnoughtWaterText;
                break;
            case ConsumablesType.Fertilizer:
                miniGameText.text = noEnoughtFertilizer;
                break;
        }
    }
}
