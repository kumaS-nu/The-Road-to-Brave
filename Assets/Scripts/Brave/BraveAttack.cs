using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveAttack : MonoBehaviour
{
    [SerializeField]
    Animator _animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Attack();
        }
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack1");
    }
}
