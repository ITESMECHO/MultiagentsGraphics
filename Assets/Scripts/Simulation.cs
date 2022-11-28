using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class OnPositionChange : UnityEvent<Car[]> {}

[System.Serializable]
public class OnLightChange : UnityEvent<int> { }
public class Simulation : MonoBehaviour
{
    [SerializeField] float _delay = 0f;
    [SerializeField] float _stepRate = 0.1f;
    [SerializeField] GameObject _parser;
    [SerializeField] OnPositionChange onPositionChange;
    [SerializeField] OnLightChange onLightChange;
    StepList _stepsData;
    Step currentStep;
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
            // Debug.Log(count);
            currentStep = _stepsData.steps[count];
            onPositionChange.Invoke(currentStep.cars);
            onLightChange.Invoke(currentStep.semaphores[0].state);
            yield return new WaitForSeconds(_stepRate);
            count++;
        }
    }
}
