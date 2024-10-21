using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventosDeEjecucion : MonoBehaviour
{
    public UnityEvent onStart, onDisable;


    private void Start()
    {
        onStart.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }
}
