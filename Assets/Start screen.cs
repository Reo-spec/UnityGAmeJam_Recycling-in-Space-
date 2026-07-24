using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Startscreen : MonoBehaviour
{

    [SerializeField] private GameObject startPanel;



    private bool gameStarted = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f; // ÉQÅ[ÉÄí‚é~

        startPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameStarted && Input.GetButtonDown("Submit"))

        {

            StartGame();

        }


        if (Gamepad.current != null &&

Gamepad.current.buttonSouth.wasPressedThisFrame)

        {

            SceneManager.LoadScene("GameScene");

        }
    }
    void StartGame()

    {

        gameStarted = true;



        startPanel.SetActive(false);

        Time.timeScale = 1f; // ÉQÅ[ÉÄäJén

    }

}
