using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicarPlayers : MonoBehaviour
{
    public float anchoPlayer;
    public Transform playerContenedor;
    Vector3 originPos;
    public GameObject playerPrefab;

    List<GameObject> players = new List<GameObject>();
    bool duplicando;
    public int cantidadDePlayers;

    private void Start()
    {
        CrearUnClon();
        originPos = transform.localPosition;
    }


    public void CrearUnClon()
    {
        if (duplicando) return;
        duplicando = true;
        cantidadDePlayers++;
        GameObject temp = Instantiate(playerPrefab);
        temp.transform.parent = playerContenedor;
        Vector3 pos = Vector3.zero;

        if(cantidadDePlayers > 1)
        {
            pos.x = players[players.Count - 1].transform.localPosition.x + anchoPlayer;
        }

        temp.transform.localPosition = pos;
        players.Add(temp);

        Vector3 posClones = playerContenedor.localPosition;
        posClones.x -= anchoPlayer / 2;

        if (cantidadDePlayers > 1)
        {
            playerContenedor.localPosition = posClones;
        }


        Invoke("Desduplicar", 0.2f);
    }

    void Desduplicar()
    {
        duplicando = false;
    }
}
