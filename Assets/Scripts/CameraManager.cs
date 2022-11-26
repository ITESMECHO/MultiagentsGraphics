using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject[] _cams;
    [SerializeField] int _selection = 0;
    int _lastSelection;

    void Start()
    {
        UpdateCam();
        _lastSelection = _selection;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selection++;
        }
        if (Input.GetMouseButtonDown(1))
        {   
           _selection--;
        }

        if(_lastSelection != _selection)
        {
            UpdateCam();
            _lastSelection = _selection;
        }
    }

    void UpdateCam()
    {
        int current = Mathf.Abs(_selection) % _cams.Length;
        for (int i = 0; i < _cams.Length; i++)
        {
            if (i == current)
            {
                _cams[i].SetActive(true);
            }
            else
            {
                _cams[i].SetActive(false);
            }
        }
    }
}
