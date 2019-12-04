using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour {

    [HideInInspector]
    public Player player;

    // Use 1 is left click, the main use
    public virtual void Use1() { }

    // Use 2 is right click, the special use
    public virtual void Use2() { }

    // Use 1 up is when the main use button is released
    public virtual void UseUp1() { }

    // Use 2 up is when the special use button is released
    public virtual void UseUp2() { }
}
