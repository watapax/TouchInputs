using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class DañoPorParticula : MonoBehaviour
{
    public int hp;
    public TextMeshPro tmp;
    public UnityEvent onHit, onMuere;

    private void Start()
    {
        if(tmp)
        {
            tmp.text = hp.ToString();
        }
    }

    private void OnParticleCollision(GameObject other)
    {

        onHit.Invoke();
        hp--;
        if(tmp)
        {
            tmp.text = hp.ToString();
        }

        CheckHP();
    }

    void CheckHP()
    {
        if(hp<1)
        {
            onMuere.Invoke();
        }
    }
}
