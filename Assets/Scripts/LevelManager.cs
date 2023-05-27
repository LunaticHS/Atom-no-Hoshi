using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> Atoms = new List<GameObject>();
    public TextMeshProUGUI levelText;

    //nextlevel
    public int nextLevel = 1;
    public int thisLevel = 0;

    public string thisLevelName;


    public int row;
    public int col;
    public float size;

    public int bulletnum;

    public int ChanceLeft;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;


    public SlotSpawner iceSpawner;

    public GameObject cur_movable_atom;

    void Start()
    {
        //make ice
        iceSpawner.makeice(row, col, size);
        levelText.text = thisLevelName;
        //设置所有的atom的z为-1
        foreach (var atom in Atoms)
        {
            atom.transform.position = new Vector3(atom.transform.position.x, atom.transform.position.y, -1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(ChanceLeft <= 2)
        {
            life3.SetActive(false);
        }
        if(ChanceLeft <= 1)
        {
            life2.SetActive(false);
        }
        if(ChanceLeft <= 0)
        {
            life1.SetActive(false);
        }
        //检查atoms是否全部isExplosion
        bool isAllExplosion = true;
        foreach (var atom in Atoms)
        {
            if (!atom.GetComponent<AtomItem>().isExplosion)
            {
                isAllExplosion = false;
                break;
            }
        }
        if (isAllExplosion)
        {
            //log
            Debug.Log("All Explosion");
            //等待1000ms
            //加载下一关
            Invoke("LoadNextLevel", 3f);
        }
        else if(ChanceLeft <= 0 && bulletnum == 0)
        {
            //加载失败界面
            if(thisLevel != 0)
            Invoke("LoadThisLevel", 3f);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadThisLevel()
    {
        SceneManager.LoadScene(thisLevel);
    }
}
