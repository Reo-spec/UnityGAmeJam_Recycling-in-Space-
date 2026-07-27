///
///物体を生成するクラス
///
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [Header("通常ごみ")]
    [SerializeField] GameObject[] trashPrefabs;
    [Header("爆弾")]
    [SerializeField] GameObject[] bombPrefab;
    [Header("鉱石")]
    [SerializeField] GameObject[] mineralPrefab;
    //通常ごみ
    // 生成するPrefab（箱など）
    [Header("基本の見た目")]
    [SerializeField] GameObject itemPrefab;
    [Header("基本生成頻度")]
    [SerializeField] float startspawnInterval = 3f;
    public float spawnInterval;

    //現在シーン内に存在するアイテム数
    public int currentItems = 0;

    [Header("アイテム生成の最大数")]
    [SerializeField] int maxItems;

    //爆弾の出現確率(例:5%)
    [Header("爆弾出現確率")]
    [SerializeField] private float bombRate = 0.05f;
    [Header("鉱石出現率")]
    [SerializeField] private float mineralRate = 0.7f;
    //隕石衝突イベント
    private bool meteorMode = false;

    void Start()
    {
        spawnInterval = startspawnInterval;
        // 1秒後から開始して、
        // spawnInterval秒ごとにSpawnItemを実行
        InvokeRepeating(nameof(SpawnItem),1f,spawnInterval);
        meteorMode = false;
    }
    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
        CancelInvoke(nameof(SpawnItem));
        InvokeRepeating(nameof(SpawnItem),1f,spawnInterval);
    }
    void Update()
    {

    }
    void SpawnItem()
    {
        //最大数に達したら生成しない
        if (currentItems >= maxItems) return;

        GameObject item;

        if (meteorMode)
        {
            if (Random.value < mineralRate)
            {

                int mineralId = Random.Range(0, mineralPrefab.Length);

                item = Instantiate(
                    mineralPrefab[mineralId],
                    transform.position,
                    Quaternion.identity
                );
                MineralItem mineral = item.GetComponent<MineralItem>();
                if (mineral != null)
                {
                    mineral.mineralID = mineralId;
                }
                currentItems++;
                return;
            }
        }
        if(Random.value<bombRate)
        {
            int bombId = Random.Range(0, bombPrefab.Length);

            //爆弾生成
            item = Instantiate(
                bombPrefab[bombId],
                transform.position,
                Quaternion.identity
            );
            BombItem bomb = item.GetComponent<BombItem>();
            if ( bomb != null )
            {
                bomb.bombID = bombId;
            }
        }
        else
        {
            //通常ごみ生成
            int id = Random.Range(0, trashPrefabs.Length);

            item = Instantiate(
                trashPrefabs[id],   // 生成するPrefab
                transform.position, // Spawnerの位置
                Quaternion.identity // 回転なし
             );
            TrashItem trash = item.GetComponent<TrashItem>();
            if (trash != null)
            {
                //ランダムID
                trash.trashID = id;
            }
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
    public void SetMeteorMode(bool active)
    {
        meteorMode = active;
    }
}
