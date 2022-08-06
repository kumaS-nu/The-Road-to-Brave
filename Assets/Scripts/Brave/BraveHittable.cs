using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveHittable : MonoBehaviour
{
    [SerializeField]
    BraveHp _braveHp;
    [SerializeField]
    Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Attack();
        }

        if(collision.gameObject.tag == "Heal")
        {
            Heal(collision);
        }
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack1");
    }

    private void Heal(Collision2D collision)
    {
        var value = collision.gameObject.GetComponent<HealItem>().HealValue;

        _braveHp!.Heal(value);
    }
}
