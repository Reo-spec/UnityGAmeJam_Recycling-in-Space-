///
///物体を生成するクラス
///
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // 生成するPrefab（箱など）
    [SerializeField] GameObject itemPrefab;
    // 何秒ごとに生成するか
    [SerializeField] float spawnInterval = 3f;

    //現在シーン内に存在するアイテム数
    public int currentItems = 0;

    //最大アイテム数
    [SerializeField] int maxItems = 10;

    [SerializeField] GameObject[] trashPrefabs;

    void Start()
    {
        // 1秒後から開始して、
        // spawnInterval秒ごとにSpawnItemを実行
        InvokeRepeating(nameof(SpawnItem),1f,spawnInterval);
    }
    void SpawnItem()
    {
        //最大数に達したら生成しない
        if (currentItems >= maxItems) return;

        int id = Random.Range(0, trashPrefabs.Length);

        // Prefabを生成する
        GameObject item=Instantiate(
            trashPrefabs[id],   // 生成するPrefab
            transform.position, // Spawnerの位置
            Quaternion.identity // 回転なし
         );

        TrashItem trash = item.GetComponent<TrashItem>();
        if(trash != null)
        {
            //ランダムID
            trash.trashID = id;
        }



        //生成したので数を増やす
        currentItems++;
    }
    public void ADDItemCount()
    {
        currentItems++;
    }
    public void RemoveItemCount()
    {
        currentItems--;
    }
    
}
