using System.Collections.Generic;
using UnityEngine;

public class ConsumablesKeeper : MonoBehaviour
{
    private List<Consumables> waterPool = new();
    private List<Consumables> fertilizerPool = new();
    private List<Consumables> currentConsumablesPool = new();


    [SerializeField] private Consumables waterPrefab;
    [SerializeField] private Consumables fertilizerPrefab;

    [SerializeField] private Transform waterPoolParent;
    [SerializeField] private Transform fertilizerPoolParent;

    [SerializeField] private int startPoolElementQuantity;

    private void Start()
    {
        InintConsumablesKeeper();
    }

    public void InintConsumablesKeeper()
    {
        for (int i = 0; i < startPoolElementQuantity; i++)
        {
            waterPool.Add(GetSetedConsumables(ConsumablesType.Water));
            fertilizerPool.Add(GetSetedConsumables(ConsumablesType.Fertilizer));
        }
    }

    public void SetCurrentPool(ConsumablesType type)
    {
        switch (type)
        {
            case ConsumablesType.Water:
                currentConsumablesPool = waterPool;
                break;
            case ConsumablesType.Fertilizer:
                currentConsumablesPool = fertilizerPool;
                break;
        }
    }

    private Consumables GetSetedConsumables(ConsumablesType type)
    {
        Consumables currentConsumables = null;

        switch (type)
        {
            case ConsumablesType.Water:
                currentConsumables = Instantiate(waterPrefab, waterPoolParent);
                break;
            case ConsumablesType.Fertilizer:
                currentConsumables = Instantiate(fertilizerPrefab, fertilizerPoolParent);
                break;
        }
        
        currentConsumables.gameObject.SetActive(false);

        return currentConsumables;
    }

    public Consumables GetConsumablesFromPool()
    {
        for(int i = 0; i < currentConsumablesPool.Count; i++)
        {
            if (!currentConsumablesPool[i].gameObject.activeSelf)
            {
                return currentConsumablesPool[i];
            }

            if(i == currentConsumablesPool.Count - 1)
            {
                Consumables additionalFruit = GetSetedConsumables(currentConsumablesPool[0].Type);
                currentConsumablesPool.Add(additionalFruit);

                return additionalFruit;
            }
        }

        return null;
    }
}
