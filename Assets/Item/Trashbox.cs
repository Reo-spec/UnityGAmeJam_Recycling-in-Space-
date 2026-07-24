///
///ゴミ箱を管理するクラス
///
using UnityEngine;

public class Trashbox : MonoBehaviour
{
    [SerializeField] int correctID;
    [SerializeField] MeterScript meter;

    private void OnTriggerEnter(Collider other)
    {
        //持っているものは回収しない
        if (!other.CompareTag("Pickup")) return;

        TrashItem trash=other.GetComponent<TrashItem>();

        if (trash == null) return;

        //ゴミを入れたら加算させる
        if (trash.trashID == correctID)
        {
            meter.Trash++;
            Destroy(other.gameObject);
        }
        else
        {
            meter.MistakeCount++;
            Destroy(other.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
