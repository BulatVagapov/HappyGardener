using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRulesScreen : MonoBehaviour
{
    [SerializeField] private Image consmableImage;
    [SerializeField] private TMP_Text rulesText;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite fertilizerSprite;

    private string waterRulesText = "Catch as much water as you can!";
    private string fertilizeRulesText = "Catch as much fertilizer as you can!";


    public void SetMiniGameRulesScreen(ConsumablesType type)
    {
        switch (type)
        {
            case ConsumablesType.Water:
                consmableImage.sprite = waterSprite;
                rulesText.text = waterRulesText;
                break;
            case ConsumablesType.Fertilizer:
                consmableImage.sprite = fertilizerSprite;
                rulesText.text = fertilizeRulesText;
                break;
        }
    }
}
