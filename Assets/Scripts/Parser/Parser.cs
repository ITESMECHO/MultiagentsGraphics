using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CarsAmountParsed : UnityEvent<int> { }
public class Parser : MonoBehaviour
{
    public static Parser instance { get; private set; }
    [SerializeField] TextAsset _json;
    [SerializeField] public StepList _steps;
    [SerializeField] CarsAmountParsed carsAmountParsed;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More thant one Parser in scene");
        }
        _steps = JsonUtility.FromJson<StepList>(_json.text);
        carsAmountParsed.Invoke(_steps.carsAmount);
    }
}
