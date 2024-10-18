using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static event Action E_CameraSwitch;
    public static event Action E_SpawnPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            E_CameraSwitch?.Invoke();
			E_SpawnPlayer?.Invoke();
        }
    }
}
