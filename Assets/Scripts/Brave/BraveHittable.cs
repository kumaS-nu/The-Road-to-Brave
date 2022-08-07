using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveHittable : MonoBehaviour
{
    [SerializeField]
    BraveHp _braveHp;
    [SerializeField]
    Animator _animator;
    [SerializeField]
    private EnemyManager _enemyManager;


    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject EarnedMoneyUI;

    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

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

        var level = StageState.Instance.EnhancementLevel[EnhancementContent.EnemyStrength];

        var damageValue = StageState.Instance.damageTable[level];//param.DamageValue;
        var getManey = StageState.Instance.earnedMoneyTable[level];

        var obj = Instantiate(EarnedMoneyUI, Vector3.zero, Quaternion.identity);
        Vector3 screenPos = cam.WorldToScreenPoint(collision.gameObject.transform.position);
        obj.transform.SetParent(canvas.transform, false);
        obj.transform.position = screenPos;
        
        //当たった敵になにかしたいときは下のDestroyをコメントアウトしてください。
        _enemyManager.KillEnemy(collision.gameObject);

        _braveHp!.Damage(damageValue);
        StageState.Instance.EarnedMoney(getManey);

    }

    private void Heal(Collision2D collision)
    {

        
        var value = StageState.Instance.healTable[StageState.Instance.EnhancementLevel[EnhancementContent.Heal]];
        
        Destroy(collision.gameObject);

        _braveHp!.Heal(value);
    }
}
