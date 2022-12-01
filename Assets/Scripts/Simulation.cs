using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnPositionChange : UnityEvent<Car[]> {}

[System.Serializable]
public class OnLightChange : UnityEvent<Semaphore[]> { }
public class Simulation : MonoBehaviour
{
    [SerializeField] float _delay = 0f;
    [SerializeField] float _stepRate = 0.1f;
    [SerializeField] GameObject _parser;
    [SerializeField] OnPositionChange onPositionChange;
    [SerializeField] OnLightChange onLightChange;
    [SerializeField] int _nSemaphores = 4;
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
            currentStep = _stepsData.steps[count];
            onPositionChange.Invoke(currentStep.cars);
            for (int i = 0; i < _nSemaphores; i++)
            {
                onLightChange.Invoke(currentStep.semaphores);
            }
            yield return new WaitForSeconds(_stepRate);
            count++;
        }
    }
}
