using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] int _status;
    [SerializeField] GameObject[] _lightsSuperior;
    [SerializeField] GameObject[] _lightsInferior;
    [SerializeField] Material[] _lightMaterialsON;
    [SerializeField] Material[] _lightMaterialsOFF;


    void Update()
    {
        On(_status);
    }

    void On(int lightNumber)
    {
        for(int i = 0; i < 3; i++)
        {
            if(i == lightNumber)
            {
                _lightsSuperior[i].GetComponent<Renderer>().material = _lightMaterialsON[i];
                _lightsInferior[i].GetComponent<Renderer>().material = _lightMaterialsON[i];
            } else
            {
                _lightsSuperior[i].GetComponent<Renderer>().material = _lightMaterialsOFF[i];
                _lightsInferior[i].GetComponent<Renderer>().material = _lightMaterialsOFF[i];
            }
        }
    }
}