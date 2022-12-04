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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _stepRate = 0.015f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _stepRate = 0.02f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _stepRate = 0.05f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _stepRate = 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _stepRate = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _stepRate = 0.01f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _stepRate = 0.005f;
        }
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
