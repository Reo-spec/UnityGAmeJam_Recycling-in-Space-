using TMPro;
using UnityEngine;



public class EventTextManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private GameObject eventTextObject;
    [SerializeField] private TMP_Text eventText;

    void Update()
    {
        eventTextObject.SetActive(eventManager.IsMeteorEvent);
    }
}