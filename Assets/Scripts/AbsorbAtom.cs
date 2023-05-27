using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbAtom : AtomItem
{
    private bool absorbing = false;
    public GameObject circle;

    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        //如果不在atoms里,添加到atoms
        int index = levelManager.Atoms.IndexOf(this.gameObject);
        if (index == -1) levelManager.Atoms.Add(this.gameObject);

        m_selfTransform = transform;
        UpdatePosition(rowIndex, columIndex);

        //circle的sprite render不可见
        circle.GetComponent<SpriteRenderer>().enabled = false;

    }

    public new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>().free)
        {
            if (!absorbing)
            {
                //获取子弹的方向
                int vx = collision.gameObject.GetComponent<Bullet>().deltax;
                int vy = collision.gameObject.GetComponent<Bullet>().deltay;

                addBullet(-vx, -vy);
                absorbing = true;
                circle.GetComponent<SpriteRenderer>().enabled = true;
                //destroy 子弹
                Destroy(collision.gameObject);
            }
            else
            {
                //调用爆炸方法
                explosion();
                //destroy 子弹
                Destroy(collision.gameObject);
            }
        }
    }
}
