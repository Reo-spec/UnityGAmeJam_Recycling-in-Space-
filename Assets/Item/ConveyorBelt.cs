///
///ベルトコンベア流すクラス
///
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    //コンベア
    //コンベアの速度
    [SerializeField] float startspeed = 2f;
    private float speed;
    //物体の固定する高さ
    [SerializeField] float height = 1.0f;

    //時間経過ごとの加速度
    //TimeScriptを参照する
    [SerializeField] TimerScript timer;
    //コンベア速度
    [SerializeField] float Time120; //2分
    [SerializeField] float Time60;  //1分
    [SerializeField] float Time30;  //30秒
    //条件判定
    private bool speedUp120 = false;
    private bool speedUp60 = false;
    private bool speedUp30 = false;

    //時間経過ごとの物体生成数
    //ItemSpawnerを参照する
    [SerializeField] ItemSpawner spawner;
    //物体頻度
    [SerializeField] float ItemTime120;
    [SerializeField] float ItemTime60;
    [SerializeField] float ItemTime30;

    //初期化
    void Start()
    {
        speed = startspeed;
        speedUp120 = false;
        speedUp60 = false;
        speedUp30 = false;
    }
    //更新処理
    void Update()
    {
        TimeSpeedUp();
    }
    void TimeSpeedUp()
    {
        //1分経過
        if (!speedUp120&&timer.elapsedTime <= 120)
        {
            speed += Time120;
            spawner.SetSpawnInterval(ItemTime120);
            speedUp120 = true;
        }
        //2分経過
        if (!speedUp60 && timer.elapsedTime <= 60)
        {
            speed += Time60;
            spawner.SetSpawnInterval(ItemTime60);
            speedUp60 = true;
        }
        //2分30秒経過
        if(!speedUp30 && timer.elapsedTime <= 30)
        {
            speed += Time30;
            spawner.SetSpawnInterval(ItemTime30);
            speedUp30 = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Conveyor")) return;

        Vector3 pos = other.transform.position;

        //右方向へ流す
        other.transform.position +=
            Vector3.right * speed * Time.deltaTime;

        //高さ固定
        pos.y = height;
    }
    //アイテムを見つける処理
    private void OnTriggerEnter(Collider other)
    {
        //コンベアに乗ったらタグ変更
        if (other.CompareTag("Pickup"))
        {
            other.tag = "Conveyor";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //コンベアから出たら元に戻す
        if(other.CompareTag("Conveyor"))
        {
            other.tag = "Pickup";
        }
    }
}
