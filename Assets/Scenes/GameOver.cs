using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{

    void Update()

    {

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            StartBtn();
        }

    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Title");
    }
}
