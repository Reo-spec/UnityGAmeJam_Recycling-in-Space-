///
///プレイヤーを制御するクラス
///
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //プレイヤーの移動速度指標
    [Header("移動速度")]
    [SerializeField]float moveSpeed;
    //PlayerInputへの参照
    PlayerInput playerInput;
    Rigidbody rb;
    // アイテムを持つ位置（HoldPoint）
    [Header("アイテムを持つ位置")]
    [SerializeField] Transform holdPoint;

    GameObject heldItem;// 現在持っているアイテム
    GameObject nearbyItem;// プレイヤーの近くにあるアイテム

    //参照
    [SerializeField] MeterScript meterScript;
    [SerializeField] ItemSpawner itemSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>(); // ← 追加
        MultiplayerCamera3D.Instance.RegisterPlayer(transform);
    }

    void OnDestroy()
    {
        MultiplayerCamera3D.Instance.UnregisterPlayer(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Grab();// 掴む処理
    }

    void FixedUpdate()
    {
        Move();// 移動処理
    }

    public void Move()
    {
        //Vector3 move = Vector3.zero;
        ////キー入力
        //if (Keyboard.current.wKey.isPressed)
        //{
        //    move.x = -1.0f;
        //}
        //if (Keyboard.current.sKey.isPressed)
        //{
        //    move.x = 1.0f;
        //}
        //if (Keyboard.current.dKey.isPressed)
        //{
        //    move.z = 1.0f;
        //}
        //if (Keyboard.current.aKey.isPressed)
        //{
        //    move.z = -1.0f;
        //}

        //transform.Translate(move * moveSpeed * Time.deltaTime);

        ////左スティック
        //Vector2 stick = Gamepad.current.leftStick.ReadValue();
        //move.x += stick.x;
        //move.z += stick.y;

        //Input Actionsの"Move"アクションから入力値Vector2を読み取る
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 move = Vector3.zero;
        move.x = -input.x;//元のコードのｗキーに合わせている
        move.z = -input.y;//元のコードを参照

        //transform.Translate(move.normalized * moveSpeed * Time.deltaTime);

        Vector3 targetPos = rb.position + move.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(targetPos);
    }
    //プレイヤーが物体を掴む動き
    void Grab()
    {
        //RBボタンの判定
        //// コントローラーが接続されていなければ終了
        //if (Gamepad.current == null) return;

        //// RBボタンが押された瞬間
        //if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        //{
        //    // 何も持っていなければ拾う
        //    if (heldItem == null)
        //        PickUp();
        //    // 持っていれば離す
        //    else
        //        Drop();
        //}
        if (playerInput.actions["Grab"].WasPressedThisFrame())
        {
            if (heldItem == null)
                PickUp();
            else
                Drop();
        }
    }
    //アイテムを持つ処理
    void PickUp()
    {
        // 近くにアイテムが無ければ何もしない
        if (nearbyItem == null) return;

        BombItem bomb = nearbyItem.GetComponent<BombItem>();
        //爆弾判定
        if(bomb != null)
        {
            //ミス(例：+5)
            meterScript.MistakeCount += bomb.penalty;
            //アイテム数を減らす
            itemSpawner.RemoveItemCount();

        }
        // 持つアイテムとして保存
        heldItem = nearbyItem;

        heldItem.tag = "HeldItem";

        // Rigidbodyを取得
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();

        // 物理演算を停止
        if (rb != null)
            rb.isKinematic = true;

        //持っている間コリダーを無効化
        Collider col=heldItem.GetComponent<Collider>();
        if(col != null)
           col.enabled = false;

        // HoldPointの子にする
        heldItem.transform.SetParent(holdPoint);
        // HoldPointと同じ位置に移動
        heldItem.transform.localPosition = Vector3.zero;
        // 回転をリセット
        heldItem.transform.localRotation= Quaternion.identity;
        if(bomb != null)
        {
            Destroy(heldItem);

            heldItem = null;
            nearbyItem = null;

            return;
        }
    }
    //アイテムを離す処理
    void Drop()
    {
        heldItem.tag = "Pickup";

        // HoldPointから外す
        heldItem.transform.SetParent(null);

        // Rigidbody取得
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();

        // ← 追加：離すときにColliderを再度有効化
        Collider col = heldItem.GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        if (rb != null)
        {
            //物理演算を再開
            rb.isKinematic = false;
            //重力を無効化
            rb.useGravity = false;
            //速度をリセット
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //スリープ解除
            rb.WakeUp();

        }
        //親から外す
        heldItem.transform.SetParent(null);

        if (heldItem != null)
        {
            heldItem.tag = "Pickup";
            // 持ち物を空にする
            heldItem = null;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")||
            other.CompareTag("Conveyor"))
        {
            nearbyItem = other.gameObject;
        }
    }
    //アイテムから離れた処理
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup") ||
           other.CompareTag("Conveyor"))
        {
            nearbyItem = null;
        }
    }
}
