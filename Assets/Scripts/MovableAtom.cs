using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableAtom : AtomItem
{
    private void OnMouseDown()
    {
        Debug.Log("MovableAtom clicked");
        levelManager.cur_movable_atom = this.gameObject;
    }
}
