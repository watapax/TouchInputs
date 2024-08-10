using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Transform esquina1; // Primer Transform para la esquina del rectángulo
    public Transform esquina2; // Segundo Transform para la esquina opuesta del rectángulo
    public GameObject[] prefabs; // Prefab del GameObject a instanciar
    public int numberOfObjects = 20; // Número de objetos a instanciar
    public float objectSize = 1f; // Tamaño de los objetos (suponiendo que son cúbicos)
    public float randomOffset;

    void Start()
    {
        SpawnObjectsInGrid();
    }

    void SpawnObjectsInGrid()
    {
        // Calcular las dimensiones del área
        Vector3 min = new Vector3(
            Mathf.Min(esquina1.position.x, esquina2.position.x),
            Mathf.Min(esquina1.position.y, esquina2.position.y),
            Mathf.Min(esquina1.position.z, esquina2.position.z)
        );

        Vector3 max = new Vector3(
            Mathf.Max(esquina1.position.x, esquina2.position.x),
            Mathf.Max(esquina1.position.y, esquina2.position.y),
            Mathf.Max(esquina1.position.z, esquina2.position.z)
        );

        // Calcular el número de celdas por eje
        int cellsX = Mathf.FloorToInt((max.x - min.x) / objectSize);
        int cellsZ = Mathf.FloorToInt((max.z - min.z) / objectSize);

        // Crear una lista de celdas disponibles
        List<Vector2Int> availableCells = new List<Vector2Int>();
        for (int x = 0; x < cellsX; x++)
        {
            for (int z = 0; z < cellsZ; z++)
            {
                availableCells.Add(new Vector2Int(x, z));
            }
        }

        // Mezclar (shuffle) las celdas disponibles para asegurar posiciones aleatorias
        for (int i = 0; i < availableCells.Count; i++)
        {
            Vector2Int temp = availableCells[i];
            int randomIndex = Random.Range(i, availableCells.Count);
            availableCells[i] = availableCells[randomIndex];
            availableCells[randomIndex] = temp;
        }

        // Instanciar objetos en las celdas disponibles
        for (int i = 0; i < Mathf.Min(numberOfObjects, availableCells.Count); i++)
        {
            Vector2Int cell = availableCells[i];
            Vector3 spawnPosition = new Vector3(
                min.x + cell.x * objectSize + objectSize / 2,
                min.y,
                min.z + cell.y * objectSize + objectSize / 2
            );

            float rx = Random.Range(0, 100) > 50 ? randomOffset * 1 : randomOffset * -1;
            float rz = Random.Range(0, 100) > 50 ? randomOffset * 1 : randomOffset * -1;
            spawnPosition.x += rx;
            spawnPosition.z += rz;
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, Quaternion.identity);
        }
    }



}
