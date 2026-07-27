///
///イベントを管理するクラス
///
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //イベント管理
    public enum EventState
    {
        Normal,      //通常
        MeteorImpact //隕石衝突
    }

    public EventState CurrentState=EventState.Normal;

    [Header("参照")]
    [SerializeField] private ConveyorBelt conveyorBelt;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TimerScript timer;
    [Header("イベント確率(%)")]
    [SerializeField] private float eventChance = 0.2f;
    [Header("イベント時間(上:最小,下:最大)")]
    [SerializeField] private float minTime = 10f;
    [SerializeField] private float maxTime = 30f;
    [Header("イベント継続時間")]
    [SerializeField] private float eventDuration = 20f;
    [Header("イベントコンベア速度(倍率)")]
    [SerializeField] private float eventConveyorSpeed=1.2f;
    private float eventtimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != EventState.Normal) return;

        eventtimer-= Time.deltaTime;

        if(eventtimer<=0 )
        {
            CheckEvent();
            ResetTimer();
        }
    }
    //タイマーリセット
    void ResetTimer()
    {
        eventtimer = Random.Range(minTime, maxTime);
    }
    //イベント判定
    void CheckEvent()
    {
        if (timer.elapsedTime <= 30f) return;
        if (CurrentState != EventState.Normal)return;

        if(Random.value<=eventChance)
        {
            StartMeteorEvent();
        }
        else
        {
            Debug.Log("何も起こらなかった");
        }
    }
    //隕石イベント開始
    void StartMeteorEvent()
    {
        CurrentState = EventState.MeteorImpact;

        Debug.Log("隕石衝突イベント発生!");

        itemSpawner.SetMeteorMode(true);
        conveyorBelt.SetSpeedMultiplier(eventConveyorSpeed);

        Invoke(nameof(EndMeteorEvent), eventDuration);
    }
    //隕石イベント終了
    void EndMeteorEvent()
    {
        CurrentState = EventState.Normal;

        itemSpawner.SetMeteorMode(false);
        conveyorBelt.SetSpeedMultiplier(1f);

        ResetTimer();

        Debug.Log("隕石イベント終了");
    }
}
