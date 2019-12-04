using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private const float spawnForce = 100f;

    private int item;
    private int amount;

    private Player player;
    private Rigidbody2D rb;

    private float startTime;

    public static GameObject Spawn(float x, float y, int id, bool homeIn = false)
    {
        GameObject pickup = Instantiate(Prefabs.Instance.Pickup, new Vector2(x + 0.5f, y + 0.5f), Quaternion.identity);
        pickup.GetComponent<Pickup>().Initialize(id, 1);
        return pickup;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-spawnForce, spawnForce), Random.Range(-spawnForce, spawnForce)));
        player = FindObjectOfType<Player>();
    }

    public void Initialize(int item, int amount) {
        this.item = item;
        this.amount = amount;
        startTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - startTime < 0.2f)
        {
            return;
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(goToPlayer());
        }
    }

    private IEnumerator goToPlayer()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        // Add force towards player
        float distance = 10;
        int count = 0;
        do
        {
            Vector3 dir = player.transform.position + new Vector3(0, 0.25f) - transform.position;
            distance = dir.magnitude;
            rb.velocity = dir.normalized * (2 + count);
            count++;
            yield return new WaitForFixedUpdate();
        }
        while (distance > 3f || count <= 8);

        // Add item to inventory and destroy pickup
        player.inventory.AddItem(item, amount);
        Destroy(gameObject);
    }
}
