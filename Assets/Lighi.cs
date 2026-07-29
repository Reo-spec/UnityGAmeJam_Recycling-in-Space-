using UnityEngine;

public class LightColorManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Light targetLight;
   
    void Update()
    {

        void Update()
        {
            if (eventManager == null)
            {
                Debug.Log("eventManager‚ª–¢İ’è");
                return;
            }

            if (targetLight == null)
            {
                Debug.Log("targetLight‚ª–¢İ’è");
                return;
            }

            if (eventManager.IsMeteorEvent)
            {
                targetLight.color = Color.red;
                targetLight.intensity = 100f; // –¾‚é‚­‚·‚é
            }
            else
            {
                targetLight.color = Color.white;
                targetLight.intensity = 1f; // ’Êí
            }
        }


        if (eventManager.IsMeteorEvent)
        {
            targetLight.color = Color.red;
        }
        else
        {
            targetLight.color = Color.white;
        }
    }
}