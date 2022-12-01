using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float _speed = 20f;
    CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Air");
        Vector3 move = transform.right * x + transform.up * y + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);
    }
}
