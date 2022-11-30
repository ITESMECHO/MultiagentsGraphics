using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private Car[] _cars; // nice to serialize for debugging
    [SerializeField] public float _ratio = 15;
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
            int yRotation = convertDirection(currentDirection[0], currentDirection[1]);
            string carName = getNameByDirection(i, currentDirection[0], currentDirection[1]);
            Vector3 positionAdjustment = convertPosition(currentDirection[0], currentDirection[1]);

            if (currentDirection[0] == 0)
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(
                        cars[i].x + positionAdjustment.x,
                        cars[i].y * positionAdjustment.y, 
                        cars[i].z * positionAdjustment.z)
                );
            } else
            {
                _carsExecution[i] = CarpoolManager.Instance.ActivateObject(
                    new Vector3(
                        cars[i].x * positionAdjustment.x,
                        cars[i].y * positionAdjustment.y,
                        cars[i].z + positionAdjustment.z)
                );
            }
            _carsExecution[i].transform.rotation = Quaternion.Euler(new Vector3(0, yRotation, 0));
            _carsExecution[i].name = carName;
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
            int yRotation = convertDirection(currentDirection[0], currentDirection[1]);
            Vector3 positionAdjustment = convertPosition(currentDirection[0], currentDirection[1]);
            _carsExecution[i].transform.rotation = Quaternion.Euler(new Vector3(0, yRotation, 0));
            if (currentDirection[0] == 0)
            {
                _carsExecution[i].transform.position = new Vector3(
                    cars[i].x / _ratio + positionAdjustment.x,
                    cars[i].y * positionAdjustment.y,
                    cars[i].z * positionAdjustment.z
                );
            }
            else
            {
                _carsExecution[i].transform.position = new Vector3(
                    cars[i].x * positionAdjustment.x,
                    cars[i].y * positionAdjustment.y,
                    cars[i].z / _ratio + positionAdjustment.z
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

    int convertDirection(int dirX, int dirZ)
    {
        /*
        * [0,  1] = 0
        * [0, -1] = 180
        * [1,  0] = 90
        * [-1, 0] = -90
        */
        if (dirX == 0 && dirZ == 1)
        {
            return 0;
        }
        if (dirX == 0 && dirZ == -1)
        {
            return 180;
        }
        if (dirX == 1 && dirZ == 0)
        {
            return 90;
        }
        if (dirX == -1 && dirZ == 0)
        {
            return -90;
        }
        return 0;
    }

    string getNameByDirection(int id, int dirX, int dirZ)
    {
        if (dirX == 0 && dirZ == 1)
        {
            return "S Car " + id;
        }
        if (dirX == 0 && dirZ == -1)
        {
            return "N Car " + id;
        }
        if (dirX == 1 && dirZ == 0)
        {
            return "W Car " + id;
        }
        if (dirX == -1 && dirZ == 0)
        {
            return "E Car " + id;
        }
        return "Car " + id;
    }

    Vector3 convertPosition(int dirX, int dirZ)
    {
        float x = 0, y = 1, z = 0;
        if (dirX == 0 && dirZ == 1)
        {
            x = -_offset + _offsetStreet;
            z = 1 / _ratio;
        }
        else if (dirX == 0 && dirZ == -1)
        {
            x = _offset - _offsetStreet;
            z = 1 / _ratio;
        }
        else if (dirX == 1 && dirZ == 0)
        {
            x = 1 / _ratio;
            z = _offset - _offsetStreet;
        }
        else if (dirX == -1 && dirZ == 0)
        {
            x = 1 / _ratio;
            z = -_offset + _offsetStreet;
        }

        return new Vector3(x, y, z);
    }
}
