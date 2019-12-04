using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    public SpriteRenderer screenFade;
    public List<Link> links;

    private Player player;
    private BoolTimer portalCooldown;

    private void Start() {
        player = GameManager.Instance.player;
        portalCooldown = gameObject.AddComponent<BoolTimer>().Constructor(false);

    }
    private void Update() {
        if (portalCooldown.Value) { return; }
        for (int i = 0; i < links.Count; i++) {
            if (player.Position == links[i].portal1.location && player.facingDirection == links[i].portal1.facingDirection) {
                if (links[i].portal1.portalType == PortalType.Automatic || Input.GetMouseButtonDown(1)) {
                    Teleport(links[i].portal2);
                    break;
                }
                
            }
            if (player.Position == links[i].portal2.location && player.facingDirection == links[i].portal2.facingDirection)
            {
                if (links[i].portal2.portalType == PortalType.Automatic || Input.GetMouseButtonDown(1))
                {
                    Teleport(links[i].portal1);
                    break;
                }
            }
        }
    }

    private void Teleport(Portal portal) {
        
        StartCoroutine(ScreenFadeCoroutine(new Vector2(portal.location.x + 0.5f, portal.location.y + 0.5f)));
        portalCooldown.UpdateValue(2f, true);
    }

    private IEnumerator ScreenFadeCoroutine(Vector2 playerDestination) {
        const float frames = 18f;
        player.pause = true;
        for (int i = 0; i <= frames; i++) {
            screenFade.color = new Color(0, 0, 0, i / frames);
            yield return new WaitForFixedUpdate();
        }
        player.transform.position = playerDestination;
        for (int i = 0; i <= frames; i++)
        {
            screenFade.color = new Color(0, 0, 0, (frames - i) / frames);
            yield return new WaitForFixedUpdate();
        }
        player.pause = false;
    }

}

[System.Serializable]
public enum PortalType { Automatic, Confirm }

[System.Serializable]
public struct Portal {

    public PortalType portalType;
    public Vector2Int location;
    public Direction facingDirection;

}

[System.Serializable]
public struct Link {

    public Portal portal1;
    public Portal portal2;

}
