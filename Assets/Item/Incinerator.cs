///
///焼却炉を管理するクラス
///
using Unity.VisualScripting;
using UnityEngine;
//燃料ゲージが通常のごみよりも増える

public class Incinerator : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] MeterScript meter;
    [Header("成功時のポイント")]
    [SerializeField] int Success;
    [Header("失敗時のポイント")]
    [SerializeField] int Failure;
    private void OnTriggerEnter(Collider other)
    {
        //持っているものは回収しない
        if (!other.CompareTag("Pickup")) return;

        MineralItem mineral = other.GetComponent<MineralItem>();
        //MineralItemがついていなかったらここで処理を終了する
        if (mineral == null) return;

        //鉱石の場合
        if (mineral != null)
        {
            meter.Trash += Success;
        }
        else
        {
            meter.MistakeCount+= Failure;
        }
        Destroy(other.gameObject);
    }
}
