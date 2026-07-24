///
///燃料メーターの加算を制御するクラス
///
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
    //ゴミのカウント(例:ゴミが10個貯まったら1カウント)
    [SerializeField] float OneCount;
    //メーターの上限値(例:6カウント貯まったらクリア)
    [SerializeField] float CountMax = 6;
    //ミスのカウント(例:ゴミを10回間違えた箱に入れたら-1カウント)
    [SerializeField] float Mistake;

    //表示を切り換える
    [SerializeField] Image meterImage;

    //Countの数だけ画像を用意して、順番にセットする
    [SerializeField] Sprite[] meterSprites;

    //メーターのカウント
    float Count = 0;
    //ミスをカウント
    float MistakeCount=0;
    //ゴミの数
    float Trash = 0;

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
    }

    [ContextMenu("Add Trash")]
    public void ADDmeter()
    {
        //ゴミの正誤判定

        //ゴミが一定値貯まったらカウントを1進める
        if (OneCount == Trash && OneCount > 0)
        {
            Count += 1;
            //初期化
            Trash = 0;
        }
        
        //ミスが一定値貯まったらカウントを-1する
        if (Mistake == MistakeCount && Mistake > 0)
        {
            Count -= 1;
            //初期化
            MistakeCount = 0;
        }

        UpdateMeterUI();
        
        //メーターが0から-1になった場合(ゲームオーバー)
        if(Count < 0)
        {

        }
        //ゲームクリア条件
        else if(Count>=CountMax)
        {

        }
    }

    void UpdateMeterUI()
    {
        if (meterImage == null || meterSprites.Length == 0) return;

        //Countの配列の範囲(0〜配列の最大数)に収める
        int index=Mathf.Clamp((int)Count,0,meterSprites.Length - 1);
        Debug.Log($"Count={Count}, index={index}, sprite={meterSprites[index].name}");
        //画像を差し替える
        meterImage.sprite = meterSprites[index];

    }

}
