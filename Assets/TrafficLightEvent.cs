using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TrafficLightEvent : MonoBehaviour
{
    [SerializeField] GameObject[] _trafficLightsExecution;
    public void ReceiveSimulation(Semaphore[] semaphores)
    {
        for (int i = 0; i < semaphores.Length; i++)
        {
            _trafficLightsExecution[i]
                .GetComponent<TrafficLightHandler>()
                .On(semaphores[i].state);
        }
    }
}
