using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LaColision
{
    public string elTag;
    public UnityEvent onTriggerEnter;
    public bool destruirElOtro;

    public void EjectarEvento()
    {
        onTriggerEnter.Invoke();

    }
}

public class DetectarTrigger2D : MonoBehaviour
{
    public LaColision[] colisiones;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < colisiones.Length; i++)
        {
            if(collision.CompareTag(colisiones[i].elTag))
            {
                colisiones[i].EjectarEvento();

                if (colisiones[i].destruirElOtro)
                    Destroy(collision.gameObject);

                break;
                
            }
        }
    }

}
