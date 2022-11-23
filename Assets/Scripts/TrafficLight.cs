using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] int _status;
    /*For all the arrays there is the following order:
     * 0: Green
     * 1: Yellow
     * 3: Red
    */
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
            Renderer rendererUp = _lightsSuperior[i].GetComponent<Renderer>();
            Renderer rendererDown = _lightsInferior[i].GetComponent<Renderer>();
            GameObject lightingUp = _lightsSuperior[i].transform.GetChild(0).gameObject;
            GameObject lightingDown = _lightsInferior[i].transform.GetChild(0).gameObject;
            if (i == lightNumber)
            {
                rendererUp.material = _lightMaterialsON[i];
                rendererDown.material = _lightMaterialsON[i];
                lightingUp.SetActive(true);
                lightingDown.SetActive(true);

            } else
            {
                rendererUp.material = _lightMaterialsOFF[i];
                rendererDown.material = _lightMaterialsOFF[i];
                lightingUp.SetActive(false);
                lightingDown.SetActive(false);
            }
        }
    }
}