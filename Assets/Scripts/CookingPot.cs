// File: CookingPot.cs
// Contributors: Brendan Robinson
// Date Created: 11/20/2019
// Date Last Modified: 11/20/2019

public class CookingPot : Decoration {

    #region Methods

    protected override void Interact() { UIHandler.Instance.OpenCookingUI(); }

    protected override void Exit() { UIHandler.Instance.CloseCookingUI(); }

    #endregion

}