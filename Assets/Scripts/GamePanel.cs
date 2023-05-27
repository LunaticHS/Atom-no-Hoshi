using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GamePanel : MonoBehaviour
{
    public Button homeBtn;

    private void Awake()
    {
        homeBtn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(0);
        });
    }
}
