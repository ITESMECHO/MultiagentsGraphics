using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField] float _delay = 0f;
    [SerializeField] float _stepRate = 0.1f;
    [SerializeField] GameObject _parser;
    StepList _stepsData;
    [SerializeField] Step currentStep;
    Coroutine _corSimulation;
    
    void Start()
    {
        _corSimulation = StartCoroutine(Steps());
    }

    IEnumerator Steps()
    {
        yield return new WaitForSeconds(_delay);
        _stepsData = _parser.GetComponent<Parser>()._steps;
        if (_stepsData == null)
        {
            yield return null;
        }

        int count = 0;
        while (count < _stepsData.steps.Length)
        {
            Debug.Log(count);
            currentStep = _stepsData.steps[count];
            yield return new WaitForSeconds(_stepRate);
            count++;
        }
    }
}
