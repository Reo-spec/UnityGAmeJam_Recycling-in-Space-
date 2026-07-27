//
//カメラを制御するプログラム
//
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayerCamera3D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static MultiplayerCamera3D Instance;

    private List<Transform> players = new List<Transform>();

    [Header("アングル設定")]
    public Vector3 angleOffset = new Vector3(0, 1, -1); // カメラの向き

    [Header("距離設定")]
    public float minDistance = 8f;   // これ以上は寄らない(全員近くにいても最低限これだけ引く)
    public float padding = 1.2f;     // 余白の倍率(1.0=ギリギリ, 1.2=少し余裕を持たせる)

    [Header("追従の滑らかさ")]
    public float positionSmoothTime = 0.3f;
    public float distanceSmoothTime = 0.4f;

    Camera cam;
    Vector3 posVelocity;
    float distVelocity;
    float currentDistance;

    Quaternion fixedRotation; // 最初に設定されている角度をそのまま記憶
    Vector3 dir;              // ターゲットからカメラへの方向(記憶した角度から自動計算)

    void Start()
    {
        Instance = this;
        cam = GetComponent<Camera>();
        currentDistance = minDistance;

        // 今シーンビューで置いてある角度をそのまま記憶する
        fixedRotation = Quaternion.Euler(30f, 168f, 0f);
        dir = -(fixedRotation * Vector3.forward);
        dir.Normalize();
    }

    // ← プレイヤー側から呼ばれる登録用メソッド。新しく追加
    public void RegisterPlayer(Transform player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    // ← 新しく追加
    public void UnregisterPlayer(Transform player)
    {
        players.Remove(player);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (players == null || players.Count == 0) return;

        // 1. 全プレイヤーを含むバウンディングボックス
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (var p in players)
        {
            if (p != null) bounds.Encapsulate(p.position);
        }

        Vector3 dir = angleOffset.normalized;

        // 記憶した角度から、右・上方向を直接取得
        Vector3 camRight = fixedRotation * Vector3.right;
        Vector3 camUp = fixedRotation * Vector3.up;

        // 3. バウンディングボックスの8頂点を、カメラの右・上軸に投影して必要な半幅・半高を求める
        float maxRight = 0f, maxUp = 0f;
        Vector3 center = bounds.center;
        Vector3 ext = bounds.extents;

        for (int x = -1; x <= 1; x += 2)
            for (int y = -1; y <= 1; y += 2)
                for (int z = -1; z <= 1; z += 2)
                {
                    Vector3 corner = center + Vector3.Scale(ext, new Vector3(x, y, z));
                    Vector3 offset = corner - center;
                    maxRight = Mathf.Max(maxRight, Mathf.Abs(Vector3.Dot(offset, camRight)));
                    maxUp = Mathf.Max(maxUp, Mathf.Abs(Vector3.Dot(offset, camUp)));
                }

        // 4. FOVから必要な距離を逆算(縦・横それぞれ計算して大きい方を採用)
        float vFov = cam.fieldOfView * Mathf.Deg2Rad;
        float hFov = 2f * Mathf.Atan(Mathf.Tan(vFov / 2f) * cam.aspect);

        float distForHeight = maxUp / Mathf.Tan(vFov / 2f);
        float distForWidth = maxRight / Mathf.Tan(hFov / 2f);

        float targetDistance = Mathf.Max(distForHeight, distForWidth) * padding;
        targetDistance = Mathf.Max(targetDistance, minDistance); // 上限は設けない(全員映す優先)

        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distVelocity, distanceSmoothTime);

        // 5. カメラ配置
        Vector3 targetPos = bounds.center + dir * currentDistance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref posVelocity, positionSmoothTime);
        //transform.LookAt(bounds.center);
    }
}

