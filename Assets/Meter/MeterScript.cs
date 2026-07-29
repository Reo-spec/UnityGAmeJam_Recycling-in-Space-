///
///燃料メーターの加算を制御するクラス
///
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MeterScript : MonoBehaviour
{
    //ゴミのカウント(例:ゴミが10個貯まったら1カウント)
    [Header("1カウントの必要数")]
    [SerializeField] int OneCount;
    //メーターの上限値(例:6カウント貯まったらクリア)
    [Header("カウント上限")]
    [SerializeField] int CountMax = 6;
    //ミスのカウント(例:ゴミを10回間違えた箱に入れたら-1カウント)
    [Header("ミスの1カウント必要数")]
    [SerializeField] int Mistake;

    //表示を切り換える
    [SerializeField] Image meterImage;

    //Countの数だけ画像を用意して、順番にセットする
    [SerializeField] Sprite[] meterSprites;

    [SerializeField] private string GameOverName = "GameOver"; // 次のシーンができたら、ここにシーン名を入れる
    [SerializeField] private string GameClearName = "GameClear"; 

    //メーターのカウント
    int Count = 0;
    //ミスをカウント
    public int MistakeCount=0;
    //ゴミの数
    public int Trash = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲーム開始時にメーターの見た目を初期状態にする
        UpdateMeterUI();
    }

    // Update is called once per frame
    void Update()
    {
        ADDmeter();
        UpdateMeterUI();
        Debug.Log(
            "ゴミ:" + Trash +
            "ミス:" + MistakeCount +
            "カウント:" + Count
        );
    }

    [ContextMenu("Add Trash")]
    public void ADDmeter()
    {
        //ゴミの正誤判定

        //ゴミが一定値貯まったらカウントを1進める
        if (OneCount <= Trash && OneCount > 0)
        {
            Count += 1;
            //初期化
            Trash -= OneCount;
        }
        
        //ミスが一定値貯まったらカウントを-1する
        if (Mistake <= MistakeCount && Mistake > 0)
        {
            Count -= 1;
            //初期化
            MistakeCount -= Mistake;
        }

        //メーターが0から-1になった場合(ゲームオーバー)
        if(Count < 0)
        {
            SceneManager.LoadScene("GameOver");//シーンを変更するプログラム
        }
        //ゲームクリア条件
        else if(Count>=CountMax)
        {
            SceneManager.LoadScene("GameClear");//シーンを変更するプログラム
        }
    }

    void UpdateMeterUI()
    {
        Debug.Log("UpdateMeterUI");
        if (meterImage == null || meterSprites.Length == 0) return;

        //Countの配列の範囲(0〜配列の最大数)に収める
        int index=Mathf.Clamp((int)Count,0,meterSprites.Length - 1);
        Debug.Log($"Count={Count}, index={index}, sprite={meterSprites[index].name}");
        //画像を差し替える
        meterImage.sprite = meterSprites[index];

    }

}
