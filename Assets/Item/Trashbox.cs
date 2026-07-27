///
///ゴミ箱を管理するクラス
///
using UnityEngine;

public class Trashbox : MonoBehaviour
{
    [SerializeField] int correctID;
    [SerializeField] MeterScript meter;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //持っているものは回収しない
        if (!other.CompareTag("Pickup")) return;

        TrashItem trash=other.GetComponent<TrashItem>();

        if (trash == null) return;

        //ゴミを入れたら加算させる
        if (trash.trashID == correctID)
        {

            Destroy(other.gameObject);
            meter.Trash++;
        }
        else
        {

            Destroy(other.gameObject);
            meter.MistakeCount++;
        }
    }
    void Update()
    {
        
    }
}
