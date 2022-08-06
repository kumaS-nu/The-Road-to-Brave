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
            Attack(collision);
        }

        if(collision.gameObject.tag == "Heal")
        {
            Heal(collision);
        }
    }

    public void Attack(Collision2D collision)
    {
        _animator.SetTrigger("Attack1");

        var value = collision.gameObject.GetComponent<EnemyParamator>().DamageValue;
        
        //当たった敵になにかしたいときは下のDestroyをコメントアウトしてください。
        Destroy(collision.gameObject);

        _braveHp!.Damage(value);
    }

    private void Heal(Collision2D collision)
    {

        var value = (StageState.Instance.EnhancementLevel[EnhancementContent.Heal] + 1) * 3;
        Destroy(collision.gameObject);

        _braveHp!.Heal(value);
    }
}
