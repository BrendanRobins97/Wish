// File: ItemData.cs
// Author: Brendan Robinson
// Date Created: 08/04/2018
// Date Last Modified: 05/13/2019
// Description: 

using System;
using UnityEditor;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class ItemData : ScriptableObject
{

    #region Fields

    public new string name;
    public Sprite icon;
    public int id;
    public int stackSize;
    public UseItem useItem;
    public float sellPrice;

    #endregion

    #region Methods

    public virtual void Awake() { }

#if UNITY_EDITOR
    public void UpdateName()
    {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, id + " - " + name);
    }
#endif

    #endregion

}