using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpoolManager : MonoBehaviour
{
    public static CarpoolManager Instance
    {
        get;
        private set;
    }

    private Queue<GameObject> _pool;
    [SerializeField] private GameObject[] _carModels;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] GameObject _parser;

    //This is called by a event on Parser (carsAmountParsed)
    public void GenerateCarPool(int poolSize)
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if(poolSize != 0)
        {
            _poolSize = poolSize;
        }
        Instance = this;
        _pool = new Queue<GameObject>();
        for(int i = 0; i < _poolSize; i++)
        {
            GameObject newCar = Instantiate<GameObject>(_carModels[Random.Range(0, _carModels.Length)]);
            _pool.Enqueue(newCar);
            newCar.SetActive(false);
        }
    }
    public GameObject ActivateObject(Vector3 pos)
    {
        if(_pool.Count == 0 || _pool == null)
        {
            return null;
        }
        GameObject obj = _pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        return obj;
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
