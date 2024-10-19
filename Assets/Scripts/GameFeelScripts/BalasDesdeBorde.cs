using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BalasDesdeBorde : MonoBehaviour
{
    public UnityEvent onAtaqueExitoso, onAtaqueFracaso;
    public HpSystem azulito;
    public GameObject advertencia;
    public GameObject balaPrefab;
    public Transform targetTransform;
    public BoxCollider2D areaCollider;

    Vector2 upRight, upLeft, downRight, downLeft;

    public float tiempoEntreSpawn;
    public int cantidadDeBalas;
    float t = 0;
    float lastSpawnTime;
    int count;
    Vector3 pos;
    bool detener;



    public void DetenerSpawn()
    {
        detener = true;
        advertencia.SetActive(false);

        if (azulito.hp < 1)
        {
            print("ataque exitoso");
            onAtaqueExitoso.Invoke();
        }
        else
        {
            Invoke("EsperarHastaQueSeRebienteLaBala", 2);

        }
    }

    void EsperarHastaQueSeRebienteLaBala()
    {
        if (azulito.hp < 1)
        {
            print("ataque exitoso");
            onAtaqueExitoso.Invoke();
        }
        else
        {
            print("ataque fracasó");
            onAtaqueFracaso.Invoke();
        }
    }

    private void Start()
    {
        Vector2 center = areaCollider.bounds.center;
        Vector2 size = areaCollider.bounds.size;

        upRight = center + new Vector2(size.x / 2, size.y / 2);
        upLeft = center + new Vector2(-size.x / 2, size.y / 2);
        downRight = center + new Vector2(size.x / 2, -size.y / 2);
        downLeft = center + new Vector2(-size.x / 2, -size.y / 2);

        advertencia.SetActive(false);
        SetearPosicion();
    }
    

    void SetearPosicion()
    {
        pos = (Vector3)PosicionSpawneo();

        advertencia.transform.right = targetTransform.position - pos;
        advertencia.transform.position = pos;

        advertencia.SetActive(true);
    }

    Vector2 PosicionSpawneo()
    {
        Vector2 pos = Vector2.zero;

        int rn = Random.Range(0, 100);

        if (rn < 25)
        {
            // arriba
            pos.y = upLeft.y;
            pos.x = Random.Range(upLeft.x, upRight.x);
        }
        else if (rn < 50)
        {
            pos.x = upRight.x;
            pos.y = Random.Range(upRight.y, downRight.y);
        }
        else if (rn < 75)
        {
            pos.y = downRight.y;
            pos.x = Random.Range(downRight.x, downLeft.x);
        }
        else
        {
            pos.x = downLeft.x;
            pos.y = Random.Range(downLeft.y, upLeft.y);
        }

        return pos;
    }


    private void Update()
    {
        if (detener) return;

        if(Time.time > lastSpawnTime + tiempoEntreSpawn  && count < cantidadDeBalas)
        {
            advertencia.SetActive(false);
            Vector3 direccionAtaque = (targetTransform.position - pos).normalized;

            GameObject bala = Instantiate(balaPrefab);
            bala.transform.position = pos;
            bala.GetComponent<MoverATarget>().SetDireccion(direccionAtaque, targetTransform);
            lastSpawnTime = Time.time;

            

            count++;
            if (count < cantidadDeBalas)
                SetearPosicion();
            else
            {
                DetenerSpawn();
            }
        }
    }


}
