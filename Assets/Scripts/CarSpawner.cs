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
            int[] currentDirection = cars[i].direction;
            int yRotation = 0;
            string carName = "";


            /*
             * [0,  1] = 0
             * [0, -1] = 180
             * [1,  0] = 90
             * [-1, 0] = -90
             */
            if (currentDirection[0] == 0 && currentDirection[1] == 1)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                        new Vector3(cars[i].x - _offset + _offsetStreet, cars[i].y, cars[i].z / _ratio)
                );
                yRotation = 0;
                carName = "S Car ";
            }
            else if (currentDirection[0] == 0 && currentDirection[1] == -1)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                        new Vector3(cars[i].x + _offset - _offsetStreet, cars[i].y, cars[i].z / _ratio)
                );
                yRotation = 180;
                carName = "N Car ";
            }
            else if (currentDirection[0] == 1 && currentDirection[1] == 0)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                        new Vector3(cars[i].x / _ratio, cars[i].y, cars[i].z)
                );
                yRotation = -90;
                carName = "W Car ";
            }
            else if (currentDirection[0] == -1 && currentDirection[1] == 0)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                        new Vector3(cars[i].x / _ratio, cars[i].y, cars[i].z)
                );
                yRotation = 90;
                carName = "E Car ";
            }

            _carsExecution[i].name = carName + i;
            _carsExecution[i].transform.rotation = Quaternion.Euler(new Vector3(0, yRotation, 0));

            /*if (i < 10)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(cars[i].x - _offset + _offsetStreet, cars[i].y, cars[i].z / _ratio)
                );
                _carsExecution[i].name = "S Car " + i;
            }
            else
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(
                        cars[i].x + _offset - _offsetStreet,
                        cars[i].y, 
                        cars[i].z / _ratio
                    )
                );
                _carsExecution[i].transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _carsExecution[i].name = "N Car " + i;
            }*/
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
            int[] currentDirection = cars[i].direction;

            if(currentDirection[0] == 0)
            {
                _carsExecution[i].transform.position = new Vector3(
                    _carsExecution[i].transform.position.x,
                    cars[i].y,
                    cars[i].z / _ratio
                );
            }
            else
            {
                _carsExecution[i].transform.position = new Vector3(
                    cars[i].x / _ratio,
                    cars[i].y,
                    _carsExecution[i].transform.position.z
                );
            }

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
