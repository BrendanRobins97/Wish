using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    public bool big = false;
    public int health;
    public Sprite stump;

    [HideInInspector]
    public Vector2Int position;

    private Color color;

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (big) {
            GameManager.Instance.DeleteObject(position, new Vector2Int(2, 2));
            Pickup.Spawn(position.x + 1, position.y + 1, ItemID.Rock);
        }
        else {
            GameManager.Instance.DeleteObject(position);
            Pickup.Spawn(position.x, position.y, ItemID.Rock);
        }
    }
}
