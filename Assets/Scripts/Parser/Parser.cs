using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public static Parser instance { get; private set; }
    [SerializeField] TextAsset _json;
    [SerializeField] public StepList _steps;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More thant one Parser in scene");
        }
        _steps = JsonUtility.FromJson<StepList>(_json.text);
    }
}
