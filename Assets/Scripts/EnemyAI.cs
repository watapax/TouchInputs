using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public ElNodo startNode;      // Nodo de inicio (donde está Blinky)
    public Transform pacman;      // Referencia al Pacman (o player)
    public float speed = 5f;      // Velocidad de movimiento de Blinky

    private List<Vector3> path = new List<Vector3>(); // Camino a seguir

    void Update()
    {
        // Si no hay camino calculado, encontrarlo
        if (path.Count == 0)
        {
            ElNodo targetNode = GetClosestNode(pacman.position);
            StartCoroutine(AStarPathfinding(startNode, targetNode));
        }
        else
        {
            MoveAlongPath(); // Moverse a lo largo del camino calculado
        }
    }

    // Algoritmo A* usando los nodos predefinidos y sus conexiones
    IEnumerator AStarPathfinding(ElNodo start, ElNodo goal)
    {
        List<ElNodo> openList = new List<ElNodo>();
        HashSet<ElNodo> closedList = new HashSet<ElNodo>();

        Dictionary<ElNodo, ElNodo> cameFrom = new Dictionary<ElNodo, ElNodo>();
        Dictionary<ElNodo, float> gCost = new Dictionary<ElNodo, float>();
        Dictionary<ElNodo, float> fCost = new Dictionary<ElNodo, float>();

        openList.Add(start);
        gCost[start] = 0;
        fCost[start] = Vector3.Distance(start.transform.position, goal.transform.position);

        while (openList.Count > 0)
        {
            // Obtener el nodo con el menor fCost
            ElNodo currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (fCost[openList[i]] < fCost[currentNode])
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // Si llegamos al nodo objetivo
            if (currentNode == goal)
            {
                path = RetracePath(cameFrom, currentNode);
                yield break;
            }

            // Explorar los vecinos
            foreach (ElNodo neighbor in currentNode.vecinos)
            {
                if (closedList.Contains(neighbor)) continue;

                float tentativeGCost = gCost[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGCost >= gCost[neighbor])
                {
                    continue;
                }

                // Actualizar el mejor camino encontrado
                cameFrom[neighbor] = currentNode;
                gCost[neighbor] = tentativeGCost;
                fCost[neighbor] = gCost[neighbor] + Vector3.Distance(neighbor.transform.position, goal.transform.position);
            }

            yield return null; // Pausar para evitar congelar Unity
        }

        path = new List<Vector3>(); // Si no hay camino
    }

    // Retroceder el camino desde el objetivo hasta el inicio
    List<Vector3> RetracePath(Dictionary<ElNodo, ElNodo> cameFrom, ElNodo currentNode)
    {
        List<Vector3> path = new List<Vector3>();
        while (cameFrom.ContainsKey(currentNode))
        {
            path.Add(currentNode.transform.position);
            currentNode = cameFrom[currentNode];
        }
        path.Reverse();
        return path;
    }

    // Mover a Blinky a lo largo del camino calculado
    void MoveAlongPath()
    {
        if (path.Count > 0)
        {
            Vector3 targetPosition = path[0];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                path.RemoveAt(0);
            }
        }
    }

    // Encontrar el nodo más cercano a una posición
    ElNodo GetClosestNode(Vector3 position)
    {
        ElNodo closestNode = null;
        float minDistance = Mathf.Infinity;

        foreach (ElNodo node in FindObjectsOfType<ElNodo>())
        {
            float distance = Vector3.Distance(position, node.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }
}
