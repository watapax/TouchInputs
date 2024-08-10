using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicadorBala : MonoBehaviour
{
    public ParticleSystem[] psBalas;
    List<ParticleCollisionEvent> collisionsEvents = new List<ParticleCollisionEvent>();
    public Transform spawnPointTransform;

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();

        int numCollisionEvents = particleSystem.GetCollisionEvents(gameObject, collisionsEvents);

        Vector3 tempPos = Vector3.zero;

        for (int i = 0; i < numCollisionEvents; i++)
        {
            tempPos += collisionsEvents[i].intersection;
        }

        Vector3 posFinal = tempPos / numCollisionEvents;

        spawnPointTransform.position = posFinal;

        for (int i = 0; i < psBalas.Length; i++)
        {
            psBalas[i].Emit(1);
        }

    }
}
