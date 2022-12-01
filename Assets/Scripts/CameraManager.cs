using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    [SerializeField] GameObject[] _cams;
    [SerializeField] GameObject _freeCamera;
    [SerializeField] int _selection = 0;
    [HideInInspector] public GameObject _currentCamera;
    bool _cameras = true;
    bool _onFreeCamera = false;
    public bool _tilted = false;
    int _lastSelection;

    void Start()
    {
        UpdateCam();
        _lastSelection = _selection;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _freeCamera.SetActive(false);
            _cameras = !_cameras;
        }

        if (_cameras)
        {
            ResetFreeCam();
            _onFreeCamera = false;
            if (_currentCamera)
            {
                _currentCamera.SetActive(true);
            }
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
        else
        {
            if (!_onFreeCamera)
            {
                _currentCamera.SetActive(false);
                _freeCamera.SetActive(true);
                ResetFreeCam();
                _onFreeCamera = true;
            }
        }

    }

    void UpdateCam()
    {
        int current = Mathf.Abs(_selection) % _cams.Length;
        _currentCamera = _cams[current];
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

    void ResetFreeCam()
    {
        if(_currentCamera.transform.rotation.eulerAngles.x == 90)
        {
            _tilted = true;
            _freeCamera.transform.position = _currentCamera.transform.position;
            _freeCamera.transform.rotation = Quaternion.Euler(
                _currentCamera.transform.rotation.eulerAngles.x - 90,
                _currentCamera.transform.rotation.eulerAngles.y,
                _currentCamera.transform.rotation.eulerAngles.z
            );
            _freeCamera.transform.GetChild(0).transform.rotation =
                _currentCamera.transform.rotation;
        } else
        {
            _tilted = false;
            _freeCamera.transform.position = _currentCamera.transform.position;
            _freeCamera.transform.rotation = _currentCamera.transform.rotation;
            _freeCamera.transform.GetChild(0).transform.rotation =
                _currentCamera.transform.rotation;
        }
    }
}
