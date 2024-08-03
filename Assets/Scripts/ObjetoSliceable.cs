using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjetoSliceable : MonoBehaviour
{

    Vector3 entrada, salida;
    bool entro;
    public float threshold = 0.99f;
    public GameObject prefabExplosion;
    public UnityEvent onSlice;


    private void OnMouseDown()
    {
        if (entro) return;
        entro = true;
        entrada = Input.GetTouch(0).position;
        //print("entro");
    }
    private void OnMouseEnter()
    {
        if (entro) return;
        entro = true;
        entrada = Input.GetTouch(0).position;
        //print("entro");
    }

    private void OnMouseExit()
    {
        entro = false;
        salida = Input.GetTouch(0).position;
        ChequearDireccion();
        //print("salio");
    }


    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended && entro)
            {
                //print("salio");
                entro = false;
                salida = Input.GetTouch(0).position;
                ChequearDireccion();
            }
        }
    }


    void ChequearDireccion()
    {
        Vector3 dir = (salida - entrada).normalized;
        Vector3 right = transform.right;

        float dotProduct = Vector3.Dot(right, dir);

        if( dotProduct >= threshold)
        {
            onSlice.Invoke();
        }
        else
        {
            print("lejos");
        }

    }


    public void SpawnearExplosion()
    {
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);
    }
}
