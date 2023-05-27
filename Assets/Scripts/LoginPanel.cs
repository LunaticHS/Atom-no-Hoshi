using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour
{
    public Button loginBtn;

    private void Awake()
    {
        loginBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
}
