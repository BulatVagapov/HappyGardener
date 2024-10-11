using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMiniGameScreen : MonoBehaviour
{
    [SerializeField] private Image consumableImage;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite fertilizerSprite;
    [SerializeField] private TMP_Text resultMiniGameText;

    public void SetImage(ConsumablesType type)
    {
        switch (type)
        {
            case ConsumablesType.Water:
                consumableImage.sprite = waterSprite;
                break;
            case ConsumablesType.Fertilizer:
                consumableImage.sprite = fertilizerSprite;
                break;
        }
    }

    public void SetResultText(string text)
    {
        resultMiniGameText.text = text;
    }
}
