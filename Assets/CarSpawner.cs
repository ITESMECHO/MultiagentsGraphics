using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Car[] _cars;
    private GameObject[] _carsExecution;

    void Start()
    {
        _carsExecution = new GameObject[_cars.Length];
        SpawnCars();
    }

    private void SpawnCars()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                new Vector3(_cars[i].x, _cars[i].y, _cars[i].z)
            );
        }
    }
}
