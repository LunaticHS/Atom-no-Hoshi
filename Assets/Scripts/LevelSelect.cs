using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //a list of buttons
    public Button[] levelBtns;

    private void Awake()
    {
        //对每一个button进行关卡监听
        for (int i = 1; i < levelBtns.Length; i++)
        {
            int levelIndex = i;
            levelBtns[i].onClick.AddListener(() =>
            {
                // 点击关卡按钮，进入Game场景
                SceneManager.LoadScene(levelIndex + 1);
            });
        }
    }
}
