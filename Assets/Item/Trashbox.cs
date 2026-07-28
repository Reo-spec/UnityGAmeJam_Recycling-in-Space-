///
///ゴミ箱を管理するクラス
///
using UnityEngine;

public class Trashbox : MonoBehaviour
{
    [Header("どのIDのごみを処理するか")]
    [SerializeField] int correctID;
    [Header("参照")]
    [SerializeField] MeterScript meter;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //持っているものは回収しない
        if (!other.CompareTag("Pickup")) return;

        TrashItem trash=other.GetComponent<TrashItem>();

        //TrashItemがついていなかったらここで処理を終了する
        if (trash == null) return;

        //ゴミを入れたら加算させる
        if (trash.trashID == correctID)
        {
            meter.Trash++;
        }
        else
        {
            meter.MistakeCount++;
        }
        Destroy(other.gameObject);
    }
}
