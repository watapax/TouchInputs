using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Tapeable : MonoBehaviour
{
    public UnityEvent onHit, onDead;
    public TextMeshPro text;
    public int hp;

    private void Awake()
    {
        text.text = hp.ToString();
    }

    private void OnMouseDown()
    {
        if (hp == 0) return;

        hp--;
        if (hp > 0)
            onHit.Invoke();
        else
            onDead.Invoke();

        text.text = hp.ToString();
    }
}
