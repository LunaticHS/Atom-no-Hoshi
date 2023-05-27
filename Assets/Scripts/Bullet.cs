using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int rowIndex;
    public int columIndex;
    public int deltax = 0;
    public int deltay = 0;

    public bool free = false;


    public LevelManager levelManager;
    private Transform m_selfTransform;
    void Awake()
    {
        m_selfTransform = transform;
        //为自己的gameObject添加Bullet tag
        free = false;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnDestroy()
    {
        levelManager.bulletnum--;
    }

    public void UpdatePosition()
    {
        if (!free)
        {
            float k = 1f;
            if(deltax  == 0 || deltay == 0)
            {
                k = 1.414f;
            }
            float newx = (columIndex - levelManager.col / 2f) * levelManager.size + (1) * levelManager.size / 2f + deltax * k * 1f * 0.45f;
            float newy = (rowIndex - levelManager.row / 2f) * levelManager.size + (1) * levelManager.size / 2f + deltay * k * 1f * 0.45f;
            // 设置坐标
            var targetPos = new Vector3(newx, newy, -2);
            m_selfTransform.localPosition = targetPos;
        }
    }

    public void goFree()
    {
        //bulletnum
        levelManager.bulletnum++;
        //free
        free = true;
        //获取速度
        var speed = GetComponent<Rigidbody2D>();
        //设置速度
        speed.velocity = new Vector2(deltax, deltay) * 3f;
        if (deltax == 0 || deltay == 0) speed.velocity *= 1.414f;
        //log速度
        //Debug.Log(speed.velocity);
        //不受重力影响
        speed.gravityScale = 0;
    }

    public void SetPos(int row,int colum)
    {
        //输出log
        //Debug.Log("SetPos");
        this.rowIndex = row;
        this.columIndex = colum;
        return;
    }

    public void SetDirection(int deltax,int deltay)
    {
        //输出log
        //debug.Log("SetDirection");
        this.deltax = deltax;
        this.deltay = deltay;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (!free)
        {
            UpdatePosition();
        }
        if (free)
        {
            //判断位置
            if (m_selfTransform.localPosition.x/2 > levelManager.col / 2f * levelManager.size 
                || m_selfTransform.localPosition.x/2 < -levelManager.col / 2f * levelManager.size 
                || m_selfTransform.localPosition.y/2 > levelManager.row / 2f * levelManager.size 
                || m_selfTransform.localPosition.y/2 < -levelManager.row / 2f * levelManager.size)
            {
                //销毁
                Destroy(gameObject);
            }
        }
    }
}
