using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Tool {

    public int power;
    protected override void Action(Vector2Int pos) {
        if (GameManager.Instance.HasTopTile(pos) && GameManager.Instance.GetTopTile(pos).tileType == TileType.Rock)
        {
            //Pickup.Spawn(pos.x, pos.y, ItemID.Rock);
            //GameManager.Instance.DeleteTopTile(pos);
        }
        if (GameManager.Instance.HasObject(pos) && GameManager.Instance.GetObject(pos).GetComponent<Rock>() != null)
        {
            GameManager.Instance.GetObject(pos).GetComponent<Rock>().Hit(power);
        }
    }

}
