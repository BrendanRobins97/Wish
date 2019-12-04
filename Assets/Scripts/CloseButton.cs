// File: CloseButton.cs
// Contributors: Brendan Robinson
// Date Created: 11/20/2019
// Date Last Modified: 11/20/2019

using UnityEngine;

public class CloseButton : MonoBehaviour {

    #region Fields

    public GameObject gameObjectToDisable;

    #endregion

    #region Methods

    public void Close() { gameObjectToDisable.SetActive(false); }

    #endregion

}