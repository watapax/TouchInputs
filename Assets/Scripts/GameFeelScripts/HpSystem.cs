using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HpSystem : MonoBehaviour, IDamagable
{
    public int hp;
    public UnityEvent onHit, onDead;

    public void TakeDamage()
    {
        if (hp == 0) return;
        hp--;
        if (hp < 1)
            onDead.Invoke();
        else
            onHit.Invoke();
    }
}
