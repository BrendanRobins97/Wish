using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Tool {

    public int power = 1;

    protected override void Action(Vector2Int pos) {
        
        if (GameManager.Instance.HasObject(pos) && GameManager.Instance.GetObject(pos).GetComponent<Tree>() != null) {
            GameManager.Instance.GetObject(pos).GetComponent<Tree>().Hit(power);
        }
    }

}
