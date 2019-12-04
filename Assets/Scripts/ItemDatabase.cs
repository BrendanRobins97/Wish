// File: ItemDatabase.cs
// Author: Brendan Robinson
// Date Created: 08/04/2018
// Date Last Modified: 01/18/2019
// Description: 

using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemData[] items;
    public static Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();
    public static Dictionary<string, int> ids = new Dictionary<string, int>();

    public static void ConstructDatabase()
    {
        items = new ItemData[ushort.MaxValue];
        ItemData[] itemList = Resources.LoadAll<ItemData>("Items");
        for (int i = 0; i < itemList.Length; i++)
        {
            items[itemList[i].id] = itemList[i];
            itemDictionary.Add(itemList[i].id, itemList[i]);

            ids.Add(itemList[i].name.Trim().ToLower(), itemList[i].id);

            itemList[i].Awake();
        }
    }

    public static bool ValidID(int id)
    {
        if (id == 0)
        {
            return false;
        }

        return ids.ContainsValue(id);
    }

    public static int GetID(string tileName)
    {
        return ids[tileName.Trim().ToLower()];
    }

    public static T GetItemData<T>(int id) where T : ItemData
    {
        if (items[id] is T)
        {
            return (T)items[id];
        }

        return null;
    }

    /*
    public static T GetItemData<T>(Tile tile) where T : ItemData
    {
        return GetItemData<T>(tile.ID);
    }

    public static T GetItemData<T>(Item item) where T : ItemData
    {
        return GetItemData<T>(item.id);
    }
    */
    public static ItemData GetItemData(int id)
    {
        return items[id];
    }

    /*
    public static ItemData GetItemData(Item item)
    {
        return items[item.id];
    }
    

    public static int GetLayer(int id)
    {
        if (items[id] is BlockData)
        {
            BlockData tileData = (BlockData)items[id];
            return tileData.layer;
        }

        if (items[id] is ForegroundData)
        {
            ForegroundData tileData = (ForegroundData)items[id];
            return tileData.layer;
        }

        return 0;
    }
    */
}