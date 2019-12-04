// File: Player.cs
// Contributors: Brendan Robinson
// Date Created: 11/11/2019
// Date Last Modified: 11/11/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, South, East, West }
public class Player : MonoBehaviour {

    #region Fields

    public float moveSpeed;
    [HideInInspector]
    public List<float> moveSpeedMultipliers = new List<float>();
    public Transform useItem;
    public Inventory inventory;

    [HideInInspector]
    public Direction facingDirection;
    [HideInInspector]
    public Direction northSouthFacingDirection;
    [HideInInspector]
    public Statistics playerStatistics;


    [HideInInspector]
    public bool overrideFacingDirection = false;
    [HideInInspector]
    public bool pause = false;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Methods

    public Vector2Int Position => new Vector2Int((int)transform.position.x, (int)(transform.position.y + 0.125f));

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStatistics = new Statistics();
    }

    private void Update() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (pause) {
            input = Vector2.zero;
        }
        rb.velocity = input * moveSpeed;
        foreach (float moveSpeedMultiplier in moveSpeedMultipliers) { rb.velocity *= moveSpeedMultiplier; }
        UpdateFacingDirection();

        if (Input.GetButtonDown("Fire1")) {
            useItem.GetComponentInChildren<UseItem>()?.Use1();;
        }
        if (facingDirection == Direction.West) { spriteRenderer.flipX = true; }
        if (facingDirection == Direction.East) { spriteRenderer.flipX = false; }

    }

    public void Pause(float duration) { StartCoroutine(PauseCoroutine(duration)); }

    private IEnumerator PauseCoroutine(float duration) {
        pause = true;
        yield return new WaitForSeconds(duration);
        pause = false;
    }

    public void UpdateFacingDirection() {

        if (!overrideFacingDirection) {
            // Update the facing direction based off the velocity
            if (rb.velocity.y > 0.01) {
                facingDirection = Direction.North;
                northSouthFacingDirection = Direction.North;
            }
            if (rb.velocity.y < -0.01) {
                facingDirection = Direction.South;
                northSouthFacingDirection = Direction.South;
            }

            if (rb.velocity.x > 0.01) { facingDirection = Direction.East; }
            if (rb.velocity.x < -0.01) { facingDirection = Direction.West; }
        }

        switch (facingDirection) {
            case Direction.North:
                animator.SetBool("North", true);
                animator.SetBool("South", false);
                animator.SetBool("East", false);
                animator.SetBool("West", false);
                break;
            case Direction.South:
                animator.SetBool("North", false);
                animator.SetBool("South", true);
                animator.SetBool("East", false);
                animator.SetBool("West", false);
                break;
            case Direction.East:
                animator.SetBool("North", false);
                animator.SetBool("South", false);
                animator.SetBool("East", true);
                animator.SetBool("West", false);
                break;
            case Direction.West:
                animator.SetBool("North", false);
                animator.SetBool("South", false);
                animator.SetBool("East", false);
                animator.SetBool("West", true);
                break;

        }
    }
    #endregion

}