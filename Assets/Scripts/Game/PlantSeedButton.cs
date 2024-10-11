using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSeedButton : MonoBehaviour, IPointerDownHandler
{
    public event Action OnSeedTap;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnSeedTap?.Invoke();
    }
}
