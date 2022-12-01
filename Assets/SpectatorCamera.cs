using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    [SerializeField] float _horizontalSpeed = 10;
    [SerializeField] float _verticalSpeed = 10;
    [SerializeField] float _mouseSens = 5;

    float _yaw = 0;
    float _pitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * _horizontalSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical")   * _verticalSpeed * Time.deltaTime;

        _yaw +=   Input.GetAxis("Mouse X") * _mouseSens * Time.deltaTime;
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSens * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0);

        transform.position +=
            transform.TransformDirection(Vector3.forward) * v +
            transform.TransformDirection(Vector3.right) * h;
    }
}
