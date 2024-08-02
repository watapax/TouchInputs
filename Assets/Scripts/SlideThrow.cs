using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideThrow : MonoBehaviour
{
    public int municion;
    public LayerMask layerMask;
    public GameObject objetoPrefab, objetoEscena;
    public float fuerza;
    public float gravityScale;

    Touch touch;
    
    int largoHistorial = 10;

    Vector3 startPosBall;

    Vector3 prevPos, currentPos;
    Vector3 posicionPelota;
    Quaternion rotaciónPelota;

    HistorialPosiciones[] posiciones;

    private void Start()
    {
        startPosBall = objetoEscena.transform.position;
        Physics.gravity = new Vector3(0, Physics.gravity.y * gravityScale, 0);
        posiciones = new HistorialPosiciones[largoHistorial];
    }

    public struct HistorialPosiciones
    {
        public Vector3 posicion;
        public float tiempo;

        public HistorialPosiciones(Vector3 _pos , float _t)
        {
            posicion = _pos;
            tiempo = _t;
        }

    }

    void ActualizarHistorial(Vector3 _lastPos, float _lastTime)
    {
        for (int i = posiciones.Length - 1; i > 0 ; i--)
        {
            posiciones[i].posicion = posiciones[i - 1].posicion;
            posiciones[i].tiempo = posiciones[i - 1].tiempo;
        }

        posiciones[0].posicion = _lastPos;
        posiciones[0].tiempo = _lastTime;
    }

    


    private void Update()
    {


        if(Input.touchCount>0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                prevPos = PosicionEnPlano(touch.position);
                ResetearPosiciones(PosicionEnPlano(touch.position), Time.time);
                posicionPelota = PosicionEnPlano(touch.position);
            }

            if(touch.phase == TouchPhase.Moved)
            {
                ActualizarHistorial(objetoEscena.transform.position, Time.time);

                prevPos = posiciones[largoHistorial - 1].posicion;
                currentPos = posiciones[0].posicion;

                Vector3 direccion = (currentPos - prevPos).normalized;

                posicionPelota = PosicionEnPlano(touch.position);
                rotaciónPelota = DireccionObjeto(direccion);


            }

            if (touch.phase == TouchPhase.Ended)
            {

                ActualizarHistorial(objetoEscena.transform.position, Time.time);

                prevPos = posiciones[largoHistorial - 1].posicion;
                currentPos = posiciones[0].posicion;

                Vector3 direccion = (currentPos - prevPos).normalized;
                float magnitud = (currentPos - prevPos).magnitude;
           
                LanzarPelota(magnitud);

            }

            objetoEscena.transform.position = posicionPelota;
            objetoEscena.transform.rotation = rotaciónPelota;

        }
        else
        {
            objetoEscena.transform.position = startPosBall;
            objetoEscena.transform.localEulerAngles = Vector3.zero;
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


    Quaternion DireccionObjeto(Vector3 _direccion)
    {
        _direccion.z = Mathf.Abs(_direccion.y);
        return Quaternion.LookRotation(_direccion);       
    }



    void LanzarPelota(float _fuerza)
    {
        GameObject _ball = Instantiate(objetoPrefab);
        Rigidbody _rb = _ball.GetComponent<Rigidbody>();
        
        _ball.transform.position = objetoEscena.transform.position;
        _ball.transform.rotation = objetoEscena.transform.rotation;
       
        _rb.isKinematic = false;
        _rb.AddForce(_ball.transform.forward * _fuerza * fuerza, ForceMode.Impulse);

        municion--;

        objetoEscena.transform.localScale = Vector3.zero;
        
        print(startPosBall);

        RecargarPelota();
    }

    void RecargarPelota()
    {
        if (municion < 1) return;

        StartCoroutine(AnimacionRecarga());
    }



    void ResetearPosiciones(Vector3 _pos, float _t)
    {
        for (int i = 0; i < posiciones.Length; i++)
        {
            posiciones[i].posicion = _pos;
            posiciones[i].tiempo = _t;
        }
    }


    IEnumerator AnimacionRecarga()
    {
        float t = 0;
        float lerpTime = 0.5f;
        float waitTime = 1;

        yield return new WaitForSeconds(waitTime);


        while (t < lerpTime)
        {
            t += Time.deltaTime;
            float p = t / lerpTime;
            p = Mathf.Sin(p * Mathf.PI * 0.5f);

            objetoEscena.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);
            yield return null;
        }

        objetoEscena.transform.localScale = Vector3.one;




    }

}
