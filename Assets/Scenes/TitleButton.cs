using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{

    void Update()

    {

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            StartBtn();
        }

    }

    void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }
}
