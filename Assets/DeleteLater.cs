using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLater : MonoBehaviour
{
    [SerializeField] GameObject _mod;

    void Update()
    {
        float ratio = _mod.GetComponent<CarSpawner>()._ratio;
        transform.position = new Vector3(
            250 / ratio,
            transform.position.y,
            250 / ratio
        );
    }
}
