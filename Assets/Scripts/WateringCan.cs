using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WateringCan : Tool
{
    #region Fields

    public TileBase wateredTile;
    public int maxWater;

    private int water;

    #endregion

    #region Methods

    private void Start() { water = maxWater / 2; }

    protected override void Action(Vector2Int pos) {
        if (water > 0 && GameManager.Instance.GetBottomTile(pos).tileType == TileType.Hoed)
        {
            GameManager.Instance.SetBottomTile(pos, wateredTile);
            water--;
        }
    }

    #endregion
}
