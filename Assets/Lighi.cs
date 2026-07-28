using UnityEngine;




public class LightColorManager : MonoBehaviour

{

    public Light targetLight;

    public EventManager eventManager;



    void Update()

    {

        if (eventManager.CurrentState == EventManager.EventState.MeteorImpact)

        {

            targetLight.color = Color.red;

        }

        else

        {

            targetLight.color = Color.white;

        }

    }

}