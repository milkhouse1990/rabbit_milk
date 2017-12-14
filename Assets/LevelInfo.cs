using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public List<LevelItem> items;
    public Rect[] Rooms;
    public LevelInfo()
    {
        items = new List<LevelItem>();
    }
}