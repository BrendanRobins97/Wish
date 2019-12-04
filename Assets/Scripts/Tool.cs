using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : UseItem {

    public float moveSpeedMultiplier = 1f;

    private bool swinging;

    #region Methods

    public override void Use1()
    {
        base.Use1();
        if (swinging) { return; }
        Vector2Int pos = Utilities.MousePosition();
        Direction direction;
        Vector2Int mouseDirection = Utilities.MousePosition() - player.Position;
        if (mouseDirection == Vector2Int.zero) { direction = player.facingDirection; }
        else if (mouseDirection.x > 0 && mouseDirection.x < 2 && Mathf.Abs(mouseDirection.y) < 2) { direction = Direction.East; }
        else if (mouseDirection.x < 0 && mouseDirection.x > -2 && Mathf.Abs(mouseDirection.y) < 2) { direction = Direction.West; }
        else if (mouseDirection.y > 0 && mouseDirection.y < 2 && Mathf.Abs(mouseDirection.x) < 2) { direction = Direction.North; }
        else if (mouseDirection.y < 0 && mouseDirection.y > -2 && Mathf.Abs(mouseDirection.x) < 2) { direction = Direction.South; }
        else
        {
            pos = player.Position;
            direction = player.facingDirection;
            switch (player.facingDirection)
            {
                case Direction.North:
                    pos += Vector2Int.up;
                    break;
                case Direction.South:
                    pos += Vector2Int.down;
                    break;
                case Direction.East:
                    pos += Vector2Int.right;
                    break;
                case Direction.West:
                    pos += Vector2Int.left;
                    break;
            }
        }
        if (!swinging) {
            StartCoroutine(Swing(pos, direction));
        }
    }

    private IEnumerator Swing(Vector2Int pos, Direction direction) {
        swinging = true;
        player.moveSpeedMultipliers.Add(moveSpeedMultiplier);
        player.overrideFacingDirection = true;
        //player.pause = true;
        player.facingDirection = direction;
        player.UpdateFacingDirection();
        yield return new WaitForSeconds(0.35f);
        Action(pos);
        yield return new WaitForSeconds(0.2f);
        player.overrideFacingDirection = false;
        //player.pause = false;
        swinging = false;
        player.moveSpeedMultipliers.Remove(moveSpeedMultiplier);
    }

    protected virtual void Action(Vector2Int pos) {

    }

    #endregion
}
