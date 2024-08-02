using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarObjeto : MonoBehaviour
{
    public GameObject objeto;
    public float fuerzaLanzamiento;
    public LayerMask layerMask;
    Touch touch;
    Vector3 startPos, endPos;
    float startTime;
    Vector3 lastPos, currentPos;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                objeto.GetComponent<Rigidbody>().isKinematic = true;
                objeto.transform.position = PosicionEnPlano(touch.position);
                startPos = objeto.transform.position;
                startTime = Time.time;
                lastPos = objeto.transform.position;
            }

            if(touch.phase == TouchPhase.Moved)
            {   
                objeto.transform.position = PosicionEnPlano(touch.position);
                Vector3 direccion = lastPos - objeto.transform.position;

                lastPos = objeto.transform.position;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                endPos = objeto.transform.position;
                Lanzar(startPos, endPos, Time.time - startTime);
            }
        }
    }


    Vector3 PosicionEnPlano(Vector2 _touchPos)
    {
        Vector3 p = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(_touchPos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
        {
            p = hitInfo.point;
        }

        return p;
    }

    void Lanzar(Vector3 _startPos, Vector3 _endPos, float _duracion)
    {
        Vector3 direccion = (_endPos - _startPos).normalized;
        float distancia = (_endPos - startPos).magnitude;
        float fuerza = fuerzaLanzamiento * (distancia / Display.main.systemHeight) / _duracion;
        Vector3 direccionLanzamiento = transform.forward * fuerza;
        Rigidbody rb = objeto.GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.AddForce(direccionLanzamiento, ForceMode.Impulse);
        
    }

    void Lanzar2(Vector3 _lastPos, Vector3 _currentPos)
    {

    }


}
