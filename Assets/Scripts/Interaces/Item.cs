using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class Item
{
    [SerializeField] GameObject WorldItem;
    [SerializeField] GameObject HandItem;
    [SerializeField] ItemType type;
    public Item(GameObject _worldPref, GameObject _handPref, ItemType t)
    {
        WorldItem = _handPref;
        HandItem = _worldPref;
        type = t;
    }

    

    public GameObject GetWorldItem()
    {
        return WorldItem;
    }

    public GameObject GetHandItem()
    {
        return HandItem;
    }

    public ItemType GetTypeItem()
    {
        return type;
    }

    public void ClearItem()
    {
        WorldItem = null;
        HandItem = null;
        type = ItemType.none;
    }

    

}