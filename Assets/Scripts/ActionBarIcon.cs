// File: ActionBarIcon.cs
// Contributors: Brendan Robinson
// Date Created: 11/11/2019
// Date Last Modified: 11/11/2019

using UnityEngine;
using UnityEngine.UI;

public class ActionBarIcon : MonoBehaviour {

    #region Fields

    public Image itemImage;

    #endregion

    #region Methods

    public void SetImage(int id) {
        if (id == 0) { itemImage.color = Color.clear; }
        else {
            itemImage.color = Color.white;
            itemImage.sprite = ItemDatabase.GetItemData(id).icon;
        }
    }

    #endregion

}