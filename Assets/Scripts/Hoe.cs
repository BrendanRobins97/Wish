// File: Hoe.cs
// Contributors: Brendan Robinson
// Date Created: 11/12/2019
// Date Last Modified: 11/12/2019

using UnityEngine;
using UnityEngine.Tilemaps;

public class Hoe : Tool {

    #region Fields

    public TileBase hoedTile;

    #endregion

    #region Methods

    protected override void Action(Vector2Int pos) {
        base.Action(pos);
        if (!GameManager.Instance.HasTopTile(pos) && 
            (GameManager.Instance.GetBottomTile(pos).tileType == TileType.Dirt ||
            GameManager.Instance.GetBottomTile(pos).tileType == TileType.Water)) {
            GameManager.Instance.SetBottomTile(pos, hoedTile);
        }
    }

    #endregion

}