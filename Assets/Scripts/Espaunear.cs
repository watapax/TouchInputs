using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espaunear : MonoBehaviour
{
    public GameObject prefab;
    public Transform target;

    public void Spawn()
    {
        Vector3 pos = target.position;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
