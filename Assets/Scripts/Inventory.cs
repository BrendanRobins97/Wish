// File: Inventory.cs
// Contributors: Brendan Robinson
// Date Created: 11/11/2019
// Date Last Modified: 11/11/2019

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Fields

    public Player player;
    public GameObject actionBarHighlight;
    public List<ActionBarIcon> actionBarIcons;

    private List<int> items;
    private List<int> amounts;

    private int index;
    private int prevIndex;
    private int maxSlots = 36;
    #endregion

    #region Methods

    private void Start() {
        items = new List<int>();
        amounts = new List<int>();

        for (int i = 0; i < maxSlots; i++) {
            items.Add(0);
            amounts.Add(1);
        }

        AddItem(ItemID.Axe);
        AddItem(ItemID.Pickaxe);
        AddItem(ItemID.Hoe);
        AddItem(ItemID.WateringCan);

        index = 0;
        prevIndex = 0;

        UpdateActionBar();

        actionBarHighlight.transform.position = actionBarIcons[index].transform.position;

        UpdateUseItem();
    }

    private void UpdateActionBar() {
        for (int i = 0; i < actionBarIcons.Count; i++)
        {
            actionBarIcons[i].SetImage(items[i]);
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            index = Utilities.Mod(index - 1, 12);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            index = Utilities.Mod(index + 1, 12);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            index = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            index = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            index = 2;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            index = 3;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            index = 4;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            index = 5;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            index = 6;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            index = 7;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            index = 8;
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            index = 9;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            index = 10;
        }
        if (Input.GetKey(KeyCode.Equals))
        {
            index = 11;
        }
        if (prevIndex != index)
        {
            UpdateUseItem();
            prevIndex = index;
        }
        actionBarHighlight.transform.position = actionBarIcons[index].transform.position;
    }

    public virtual void AddItem(int item, int amount = 1, int slot = 0)
    {
        ItemData itemData = ItemDatabase.GetItemData(item);
        if (itemData == null) { return; }

        for (int i = slot; i < maxSlots; i++)
        {
            if (items[i].Equals(item)) {
                amounts[i] += amount;

                return;
            }
        }

        for (int i = slot; i < maxSlots; i++)
        {
            if (items[i] == 0)
            {
                items[i] = item;
                amounts[i] = amount;
                return;
            }
        }
    }

    private void UpdateUseItem() {
        if (player.useItem.childCount > 0) {
            Destroy(player.useItem.GetChild(0).gameObject);
        }
        if (items[index] != 0) {
            Instantiate(ItemDatabase.GetItemData(items[index]).useItem, player.useItem.transform.position,
                Quaternion.identity, player.useItem.transform).player = player;
        }
        
    }

    #endregion

}