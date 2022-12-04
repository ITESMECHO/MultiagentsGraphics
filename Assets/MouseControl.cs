using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] float _mouseSens = 100f;
    [SerializeField] GameObject _cameraManager;
    float _xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        _xRot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSens * Time.deltaTime;
        
        _xRot -= mouseY;

        if (_cameraManager.GetComponent<CameraManager>()._tilted)
        {
            _xRot = Mathf.Clamp(_xRot, -180f, 0f);
            transform.localRotation = Quaternion.Euler(_xRot + 90, 0f, 0f);
        } else
        {
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);
            transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);
        }
        transform.parent.transform.Rotate(Vector3.up * mouseX);
    }
}
