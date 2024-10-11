using UnityEngine;
using UnityEngine.EventSystems;

public class Consumables : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ConsumablesType type;
    public ConsumablesType Type => type;

    public void OnPointerDown(PointerEventData eventData)
    {
        MiniGameManager.Instance.GotNumberOfConsumables++;
        gameObject.SetActive(false);
    }
}
