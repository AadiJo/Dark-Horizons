using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{

    [HideInInspector] public bool triggered = false;

    public enum EventTypes { Activate, Deactivate };
    public EventTypes eventType = EventTypes.Activate;
    public GameObject triggerObject;
    private void Update()
    {

        if (triggered)
        {

            if (eventType == EventTypes.Activate)
            {

                triggerObject.SetActive(true);

            }
            else if (eventType == EventTypes.Deactivate)
            {

                triggerObject.SetActive(false);

            }

        }

    }

}
