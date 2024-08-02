using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Colision3D : MonoBehaviour
{
    public string tag;
    public UnityEvent onColisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(tag))
        {
            onColisionEnter.Invoke();
        }
    }
}
