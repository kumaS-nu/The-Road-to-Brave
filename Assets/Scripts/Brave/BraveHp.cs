using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveHp : MonoBehaviour, ICheere
{
    [Header("HPŠÖŒW")]
    [SerializeField]
    private int _initialHp = 100;
    [SerializeField]
    private Text _initialHptext;
    [SerializeField]
    private Text _currentHpText;
    [SerializeField]
    private Slider _hpSlider;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private int _respawnTime = 4;
    [Header("CheereŠÖŒW")]
    [SerializeField]
    int _damageDown = 1;
    [SerializeField]
    float _damageDownTime = 0.1f;
    [SerializeField]
    BraveController _braveController;
    [Header("ƒTƒEƒ“ƒh")]
    [SerializeField]
    AudioSource _deathSound;
    [SerializeField]
    AudioSource _damageSound;
    [SerializeField]
    AudioSource _attackSound;
    [SerializeField]
    AudioSource _healSound;

    private int _currentHp = 0;
    public float _currentDamageDown = 1.0f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentDamageDown = 1.0f;

        _currentHp = _initialHp;
        _initialHptext!.text = _initialHp.ToString();

        UISet(_currentHp);
    }

    public void Heal(int heal)
    {
        _healSound?.Play();
        var finalHeal = Mathf.Min(_initialHp, _initialHp + heal);
        _currentHp = finalHeal;
        UISet(_currentHp);
    }

    public void Damage(int damage)
    {
        _damageSound?.Play();
        _attackSound?.Play();

        var finalDamage = damage * Mathf.Clamp(_currentDamageDown,0.1f,1.0f);
        _currentHp -= (int)finalDamage;
        UISet(_currentHp);
        if(_currentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("God");
        StageState.Instance.BraveDeath();
        _deathSound?.Play();
        _animator.SetTrigger("Death");
        _braveController!.BraveMove.Death();
        StartCoroutine(Revive());
    }

    private void UISet(int hp)
    {
        _currentHpText!.text = hp.ToString();

        _hpSlider.value = (float)hp / (float)_initialHp;
    }

    public void HpUpdate()
    {
        _initialHp += 10;
        _initialHptext!.text = _initialHp.ToString();
        _currentHp += 10;
        _currentHpText!.text = _currentHp.ToString();
        _hpSlider.value = (float)_currentHp / (float)_initialHp;
    }

    public void OnCheere()
    {
        StartCoroutine(DamageDownCor());
    }

    IEnumerator DamageDownCor()
    {
        _currentDamageDown = _damageDown;
        yield return new WaitForSeconds(_damageDownTime);
        _currentDamageDown = 0;
    }

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(_respawnTime);
        Debug.Log("Revive");
        _currentHp = _initialHp;
        UISet(_initialHp);
        _braveController.BraveMove.Respawn();
        gameObject.layer = LayerMask.NameToLayer("Default");
        _animator.SetTrigger("Revive");
    }
}