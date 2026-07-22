///
///プレイヤーを制御するクラス
///
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //プレイヤーの移動速度指標
    [SerializeField]float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 move = Vector3.zero;
        //キー入力
        if (Keyboard.current.wKey.isPressed)
        {
            move.x = -1.0f;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move.x = 1.0f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move.z = 1.0f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.z = -1.0f;
        }
        
        transform.Translate(move * moveSpeed * Time.deltaTime);

        //左スティック
        Vector2 stick = Gamepad.current.leftStick.ReadValue();
        move.x += stick.x;
        move.z += stick.y;

        transform.Translate(move.normalized * moveSpeed * Time.deltaTime);
    }
}
