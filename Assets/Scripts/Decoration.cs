using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    #region Fields

    [HideInInspector]
    public Vector2Int position;
    public Vector2Int size;

    protected Player player;

    #endregion

    #region Methods

    protected virtual void Start()
    {
        player = GameManager.Instance.player;
        position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    protected virtual void Update()
    {
        if (PlayerIsFacing() && Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    protected virtual void Interact() {

    }

    protected virtual void Exit() {

    }

    public bool PlayerIsFacing()
    {
        Vector2Int playerPosition = player.Position;
        // Player is on the right
        if (playerPosition.x - position.x == 1
            && playerPosition.y - position.y <= size.y - 1
            && playerPosition.y - position.y >= 0
            && player.facingDirection == Direction.West) { return true; }
        // Player is on the left
        if (playerPosition.x - position.x == -1
            && playerPosition.y - position.y <= size.y - 1
            && playerPosition.y - position.y >= 0
            && player.facingDirection == Direction.East) { return true; }
        // Player is above
        if (playerPosition.y - position.y == 1
            && playerPosition.x - position.x <= size.x - 1
            && playerPosition.x - position.x >= 0
            && player.northSouthFacingDirection == Direction.South) { return true; }
        // Player is below
        if (playerPosition.x - position.x == -1
            && playerPosition.x - position.x <= size.x - 1
            && playerPosition.x - position.x >= 0
            && player.northSouthFacingDirection == Direction.North) { return true; }
        return false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Exit();
        }
    }
    #endregion
}
