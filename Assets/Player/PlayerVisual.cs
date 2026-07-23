using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisual : MonoBehaviour
{
    public GameObject model01Prefab;
    public GameObject model02Prefab;

    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        if (input.playerIndex == 0)
        {
            Instantiate(model01Prefab, transform);
        }
        else
        {
            Instantiate(model02Prefab, transform);
        }
    }
}