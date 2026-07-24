using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTime = 180f;

    [Header("時間切れ後のシーン遷移")]
    [SerializeField] private string nextSceneName; // 次のシーンができたら、ここにシーン名を入れる

    private float elapsedTime = 0f;
    private bool isRunning = true;
    
    void Start()
    {
        elapsedTime = startTime;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            elapsedTime -= Time.deltaTime;
            if(elapsedTime <= 0f)
            {
                elapsedTime = 0f;
                isRunning = false;
                OnTimerEnd();
            }

            UpdateTimerDisplay();

        }
    }

    //表示方法
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        //例
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 時間切れになったときの処理
    void OnTimerEnd()
    {
        Debug.Log("タイムアップ！");

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("Next Scene Nameが未設定です。シーンができたらInspectorで設定してください。");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    //ゲームが実際に始まったタイミングで出す
    public void StartTimer()
    {
        isRunning = true;
    }

    //ゲームクリア時にタイマーを止める
    public void StopTimer()
    {
        isRunning = false;
    }

    //タイマーをリスタートする
    public void ResetTimer()
    {
        elapsedTime = startTime;
        isRunning = true;
    }

}
