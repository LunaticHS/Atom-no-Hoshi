using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update

    public int rowIndex, columIndex;
    public LevelManager levelManager;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Slot clicked");
        if (null != levelManager.cur_movable_atom)
        {
            //获取atom的fruit
            var atom = levelManager.cur_movable_atom.GetComponent<AtomItem>();
            atom.UpdatePosition(rowIndex, columIndex);
            levelManager.cur_movable_atom = null;
        }
    }
}
