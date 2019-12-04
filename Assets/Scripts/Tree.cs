using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
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

    public void Die() {
        GameManager.Instance.DeleteObject(position);
    }
}
