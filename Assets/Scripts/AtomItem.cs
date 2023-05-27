using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Collections;

public class AtomItem : MonoBehaviour
{
    public int rowIndex;

    public int columIndex;

    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;
    public bool LeftUp;
    public bool LeftDown;
    public bool RightUp;
    public bool RightDown;

    public bool isExplosion = false;

    private List<GameObject> bullets = new List<GameObject>();

    public GameObject bulletObj;

    public LevelManager levelManager;

    protected Transform m_selfTransform;

    public AudioClip explosionClip;

    protected void addBullet(int vx,int vy)
    {
        if(bulletObj == null)
        {
            return;
        }
        var newbullet = Instantiate(bulletObj);
        //设置子弹的父物体为Bullets
        newbullet.transform.SetParent(GameObject.Find("Bullets").transform);
        //调用子弹的SetPos方法，设置子弹的位置
        newbullet.GetComponent<Bullet>().SetPos(rowIndex, columIndex);
        //设置子弹的位置
        newbullet.GetComponent<Bullet>().UpdatePosition();
        //设置子弹的方向
        newbullet.GetComponent<Bullet>().SetDirection(vx, vy);
        bullets.Add(newbullet);
    }

    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        
        //如果不在atoms里,添加到atoms
        int index = levelManager.Atoms.IndexOf(this.gameObject);
        if (index == -1) levelManager.Atoms.Add(this.gameObject);

        m_selfTransform = transform;
        UpdatePosition(rowIndex, columIndex);
        if (Left) addBullet(-1, 0);
        if (Right) addBullet(1, 0);
        if (Up) addBullet(0, 1);
        if (Down) addBullet(0, -1);
        if (LeftUp) addBullet(-1, 1);
        if (LeftDown) addBullet(-1, -1);
        if (RightUp) addBullet(1, 1);
        if (RightDown) addBullet(1, -1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰撞到的是子弹,并且子弹是free的

        if (collision.gameObject.GetComponent<Bullet>().free)
        {
            //调用爆炸方法
            explosion();
            //destroy 子弹
            Destroy(collision.gameObject);
        }
    }


    public void explosion()
    {
        //如果已经爆炸过了，就不再爆炸
        if (isExplosion)
        {
            return;
        }
        //播放爆炸音效
        AudioSource.PlayClipAtPoint(explosionClip, Vector3.zero);
        //设置已经爆炸
        isExplosion = true;
        //遍历所有的bullets
        for (int i = 0; i < bullets.Count; i++)
        {
            //gofree
            bullets[i].GetComponent<Bullet>().goFree();
        }
        //隐藏sprite render组件
        if (m_selfTransform != null)
        {
            m_selfTransform.gameObject.SetActive(false);
        }
    }

    public void UpdatePosition(int rowIndex, int columIndex)
    {
        this.rowIndex = rowIndex;
        this.columIndex = columIndex;
        float newx = (columIndex - levelManager.col / 2f) * levelManager.size + levelManager.size / 2f;
        float newy = (rowIndex - levelManager.row / 2f) * levelManager.size + levelManager.size / 2f;
        // 设置坐标
        var targetPos = new Vector3(newx, newy, -1);
        m_selfTransform.localPosition = targetPos;
        //更新bullets
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].GetComponent<Bullet>().SetPos(rowIndex, columIndex);
            bullets[i].GetComponent<Bullet>().UpdatePosition();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Atom clicked");
        if (levelManager.ChanceLeft <= 0) return;
        levelManager.ChanceLeft--;
        explosion();
    }
}
