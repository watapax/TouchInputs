using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ManagerSystem : MonoBehaviour
{
    public int hp;
    int count;
    public UnityEvent onComplete;
    bool complete;
    public float tiempoDeEsperaAntesDeTerminar;

    public void AgregarHit()
    {
        if (complete) return;

        count++;
        if(count >= hp)
        {
            complete = true;
            Invoke("Terminar", tiempoDeEsperaAntesDeTerminar);
        }
    }

    void Terminar()
    {
        onComplete.Invoke();
    }
}
