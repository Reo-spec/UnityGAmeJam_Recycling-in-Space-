///
///物体を消去するクラス
///
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    //ItemSpawnerへの参照
    [SerializeField] ItemSpawner spawner;

    // Triggerに物体が入ったとき
    private void OnTriggerEnter(Collider other)
    {
        //コンベア上のものだけ削除
        if(other.CompareTag("Conveyor"))
        {
            // 物体を削除
            Destroy(other.gameObject);

            //現在数を減らす
            spawner.currentItems--;
        }
    }
}
