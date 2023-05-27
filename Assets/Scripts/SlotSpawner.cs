using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    public GameObject iceObj;

    public void makeice(int row, int col, float size)
    {
        iceObj.transform.localScale = Vector3.one * size;
        for (int rowIndex = 0; rowIndex < row; ++rowIndex)
        {
            for (int columIndex = 0; columIndex < col; ++columIndex)
            {
                if (rowIndex % 2 == 1|| columIndex % 2 == 1) continue;
                var obj = Instantiate(iceObj);
                obj.transform.SetParent(iceObj.transform.parent, false);
                var x = (columIndex - col / 2f) * size + size / 2f;
                var y = (rowIndex - row / 2f) * size + size / 2f;
                obj.transform.localPosition = new Vector3(x, y, 0);
                obj.transform.localScale = new Vector3(0.22f,0.22f,0);
                //获取slot
                var slot = obj.GetComponent<Slot>();
                //设置slot的位置
                slot.rowIndex = rowIndex;
                slot.columIndex = columIndex;
            }
        }
        iceObj.SetActive(false);
    }
}
