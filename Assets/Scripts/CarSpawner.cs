using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private Car[] _cars; // nice to serialize for debugging
    [SerializeField] float _ratio = 15;
    [SerializeField] float _offset = 10;
    [SerializeField] float _offsetStreet = 0.15f;
    private GameObject[] _carsExecution;
    bool spawned = false;
    public void SpawnCars(ref Car[] cars)
    {
        _carsExecution = new GameObject[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            Car currentCar = cars[i];
            if (i < 10)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(cars[i].x - _offset + _offsetStreet, cars[i].y, cars[i].z / _ratio)
                );
                _carsExecution[i].name = "S Car " + i;
            }
            else
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(cars[i].x + _offset - _offsetStreet, cars[i].y, cars[i].z / _ratio)
                );
                _carsExecution[i].transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _carsExecution[i].name = "N Car " + i;
            }
        }
        spawned = true;
    }

    public void PositionCars(Car[] cars)
    {
        _cars = cars;
        if (!spawned)
        {
            SpawnCars(ref cars);
            return; 
        }
        for (int i = 0; i < _carsExecution.Length; i++)
        {
            _carsExecution[i].transform.position = new Vector3(
                _carsExecution[i].transform.position.x,
                cars[i].y,
                cars[i].z / _ratio
            );

            // A proposal for ease motion
            /*_carsExecution[i].transform.position = Vector3.Lerp(
                _carsExecution[i].transform.position,
                new Vector3(
                    _carsExecution[i].transform.position.x,
                    cars[i].y,
                    cars[i].z / _ratio
                ),
                _interpolation
            );*/
        }
    }
}
