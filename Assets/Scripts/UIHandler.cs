// File: UIHandler.cs
// Contributors: Brendan Robinson
// Date Created: 11/20/2019
// Date Last Modified: 11/20/2019

using UnityEngine;

public class UIHandler : MonoBehaviour {

    #region Constants

    public static UIHandler Instance;

    #endregion

    #region Fields

    public GameObject cookingUI;

    #endregion

    #region Methods

    private void Awake() {
        if (!Instance) { Instance = this; }
        else { Destroy(this); }
        cookingUI.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            cookingUI.SetActive(false);
        }
    }

    public void OpenCookingUI() {
        cookingUI.SetActive(true);
    }

    public void CloseCookingUI() {
        cookingUI.SetActive(false);
    }

    #endregion

}