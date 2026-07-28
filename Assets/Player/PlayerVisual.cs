using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisual : MonoBehaviour
{
    [Header("モデル")]
    [SerializeField]public GameObject[] models;

    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        if (input.playerIndex < models.Length)
        {

            Instantiate(models[input.playerIndex], transform);
        }

        //自分のHoldPointに入っているものを削除
        Transform holdPoint = transform.Find("HoldPoint");

        if(holdPoint != null)
        {
            foreach(Transform child in holdPoint)
            {
                Destroy(child.gameObject);
            }
        }
    }
}