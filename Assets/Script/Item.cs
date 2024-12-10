using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite image;
    public int id;

    public List<CountMultiplier> itemCountMultiplier;
}

[System.Serializable]
public class CountMultiplier
{
    public int itemCount;
    public int countMultiplier;
}
