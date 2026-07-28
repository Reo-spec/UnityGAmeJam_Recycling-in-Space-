using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{

    void Update()

    {

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Button();
        }

    }

    public void Button()
    {
        SceneManager.LoadScene("Title");
    }
}
