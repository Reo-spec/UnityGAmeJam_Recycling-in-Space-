using UnityEngine;

public class amountofsunlight : MonoBehaviour
{
    [SerializeField] private Light sunLight;
    [SerializeField] private EventManager eventManager;

    void Update()
    {
        if (eventManager.IsMeteorEvent)
        {
            sunLight.intensity = 0.02f; // ˆÃ‚­‚·‚é
        }
        else
        {
            sunLight.intensity = 1.0f; // Œ³‚É–ß‚·
        }
    }
}
