// File: Prefabs.cs
// Contributors: Brendan Robinson
// Date Created: 07/07/2019
// Date Last Modified: 07/09/2019

using UnityEngine;

public class Prefabs : MonoBehaviour
{

    #region Constants

    public static Prefabs Instance;

    #endregion

    #region Fields

    public GameObject Pickup;

    #endregion

    #region Methods

    private void Awake() { Instance = this; }

    #endregion

}