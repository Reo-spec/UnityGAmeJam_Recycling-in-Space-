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

        Debug.Log($"name={gameObject.name} index={input.playerIndex}");
        Transform visualRoot = transform.Find("VisualRoot");

        foreach(Transform child in visualRoot)
        {
            Destroy(child.gameObject);
        }
        GameObject model=
            Instantiate(
            models[input.playerIndex],
            visualRoot
        );
        model.transform.localPosition = new Vector3(0, 0,0);

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