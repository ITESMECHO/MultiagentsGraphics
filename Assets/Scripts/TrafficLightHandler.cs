using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightHandler : MonoBehaviour
{
    [SerializeField] int _status;
    /*
     * For all the arrays there is the following order:
     * 0: Green
     * 1: Yellow
     * 3: Red
    */
    [SerializeField] GameObject[] _lightsSuperior;
    [SerializeField] GameObject[] _lightsInferior;
    [SerializeField] Material[] _lightMaterialsON;
    [SerializeField] Material[] _lightMaterialsOFF;

    public void On(int lightNumber)
    {
        for(int i = 0; i < 3; i++)
        {
            Renderer rendererUp = _lightsSuperior[i].GetComponent<Renderer>();
            Renderer rendererDown = _lightsInferior[i].GetComponent<Renderer>();
            GameObject[] lightingUp = { 
                _lightsSuperior[i].transform.GetChild(0).gameObject,
                _lightsSuperior[i].transform.GetChild(1).gameObject,

            };
            GameObject[] lightingDown = { 
                _lightsInferior[i].transform.GetChild(0).gameObject,
                _lightsInferior[i].transform.GetChild(1).gameObject,
            };
            if (i == lightNumber)
            {
                rendererUp.material = _lightMaterialsON[i];
                rendererDown.material = _lightMaterialsON[i];
                EnableChildren(ref lightingUp, true);
                EnableChildren(ref lightingDown, true);

            } else
            {
                rendererUp.material = _lightMaterialsOFF[i];
                rendererDown.material = _lightMaterialsOFF[i];
                EnableChildren(ref lightingUp, false);
                EnableChildren(ref lightingDown, false);
            }
        }
    }

    void EnableChildren(ref GameObject[] children, bool isEnabled)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActive(isEnabled);
        }
    }
}