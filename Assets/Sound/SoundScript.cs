using UnityEngine;

public class SoundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioSource seSource;

    // Inspectorで直接AudioClipを割り当てて、ボタンなどから呼び出す
    public void Play(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }

    // 第二引数で音量を指定できるようにする(省略時は1.0=通常音量)
    public void Play(AudioClip clip, float volume = 1.0f)
    {
        seSource.PlayOneShot(clip, volume);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
