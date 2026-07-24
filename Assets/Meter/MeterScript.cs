///
///燃料メーターの加算を制御するクラス
///
using UnityEngine;

public class MeterScript : MonoBehaviour
{
    //ゴミのカウント(例:ゴミが10個貯まったら1カウント)
    [SerializeField] float OneCount;
    //メーターの上限値(例:10カウント貯まったらクリア)
    [SerializeField] float CountMax;
    //ミスのカウント(例:ゴミを10回間違えた箱に入れたら-1カウント)
    [SerializeField] float Mistake;
    //メーターのカウント
    float Count = 0.0f;
    //ミスをカウント
    public float MistakeCount=0.0f;
    //ゴミの数
    public float Trash = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ADDmeter();
    }

    public void ADDmeter()
    {
        //ゴミの正誤判定

        //ゴミが一定値貯まったらカウントを1進める
        if (OneCount == Trash)
        {
            Count += 1;
            //初期化
            Trash = 0;
        }
        //ミスが一定値貯まったらカウントを-1する
        if (Mistake == MistakeCount)
        {
            Count -= 1;
            //初期化
            MistakeCount = 0;
        }
        //メーターが0から-1になった場合(ゲームオーバー)
        if(Count < 0)
        {

        }
        //ゲームクリア条件
        else if(Count>=CountMax)
        {

        }
    }
}
