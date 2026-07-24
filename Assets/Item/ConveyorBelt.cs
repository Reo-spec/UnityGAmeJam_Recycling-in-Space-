///
///ベルトコンベア流すクラス
///
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    //コンベアの速度
    [SerializeField] float speed = 2f;
    //物体の固定する高さ
    [SerializeField] float height = 1.0f;
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
