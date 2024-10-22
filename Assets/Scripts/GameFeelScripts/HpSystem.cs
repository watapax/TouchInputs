using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HpSystem : MonoBehaviour, IDamagable
{
    public int hp;
    public Image hpBarImage;
    public UnityEvent onHit, onDead;
    float maxHp;

    private void Start()
    {
        maxHp = hp;
    }
    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        if (hp == 0) return;
        hp--;

        Check();
    }

    public void TakeDamage(int _cantidad)
    {
        if (hp == 0) return;
        hp -= _cantidad;

        Check();
    }

    void Check()
    {
        if (hpBarImage != null)
        {
            float _currentHp = (float)hp;
            float fillImage = _currentHp / maxHp;
            hpBarImage.fillAmount = fillImage;
        }


        if (hp < 1)
            onDead.Invoke();
        else
            onHit.Invoke();
    }
}
