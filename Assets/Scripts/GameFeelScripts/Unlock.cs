using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Unlock : MonoBehaviour
{
 public Transform targetTransform; 

    public UnityEvent AlLlegar;
    
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 posInicial;
    float maxDistance;
    bool llego;
    bool drageando;
    
    private void Start()
    {
        posInicial = transform.position;
        maxDistance = Vector3.Distance(transform.position, targetTransform.position);
    }

    void OnMouseDown()
    {
        if (llego) return;
        StopAllCoroutines();
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        drageando = true;
    }



    private void FixedUpdate()
    {
        if (llego) return;

        if (drageando)
        {
            Vector3 posActual = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) + offset;
            Vector3 direccion = (targetTransform.position - transform.position).normalized;
            Vector3 movimientoProyectado = Vector3.Project((posActual - transform.position), direccion);
            Vector3 posNueva = transform.position + movimientoProyectado;

            float distanciaActual = Vector3.Distance(posNueva, targetTransform.position);

            if (!SiNoSePaso(posNueva))
            {
                if (distanciaActual <= maxDistance)
                    transform.position = posNueva;
            }
            else
            {
                llego = true;
                drageando = true;
                transform.position = targetTransform.position;
                AlLlegar.Invoke();
            }
        }


    }


    private void OnMouseUp()
    {
        if (llego) return;
        drageando = false;
        StartCoroutine(Volver());
    }

    IEnumerator Volver()
    {
        while(transform.position != posInicial)
        {
            transform.position = Vector3.MoveTowards(transform.position, posInicial, 15 * Time.deltaTime);
            yield return null;
        }
    }

    bool SiNoSePaso(Vector3 proxPos)
    {
        Vector3 direccionObjeto = transform.right.normalized;
        Vector3 direccionComparada = (targetTransform.position - proxPos).normalized;

        float productoEscalar = Vector3.Dot(direccionObjeto, direccionComparada);

        if (Mathf.Approximately(productoEscalar, 1f))
        {
            return false;
        }
        else
        {
            return true;
        }
    }




}
