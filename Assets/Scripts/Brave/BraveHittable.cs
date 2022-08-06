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
        if(collision.gameObject.tag == "Heal")
        {
            Heal(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Attack(collision);
        }
    }

    public void Attack(Collider2D collision)
    {
        _animator.SetTrigger("Attack1");

        var param = collision.gameObject.GetComponent<EnemyParamator>();
        var damageValue = param.DamageValue;
        var getManey = param.GetMoney;
        
        //当たった敵になにかしたいときは下のDestroyをコメントアウトしてください。
        Destroy(collision.gameObject);

        _braveHp!.Damage(damageValue);
        StageState.Instance.EarnedMoney(getManey);
    }

    private void Heal(Collision2D collision)
    {

        var value = (StageState.Instance.EnhancementLevel[EnhancementContent.Heal] + 1) * 3;
        Destroy(collision.gameObject);

        _braveHp!.Heal(value);
    }
}
