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
    [Header("CheereŠÖŒW")]
    [SerializeField]
    int _damageDown = 1;
    [SerializeField]
    float _damageDownTime = 0.1f;

    private int _currentHp = 0;
    public float _currentDamageDown = 1.0f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentDamageDown = 0;

        _currentHp = _initialHp;
        _initialHptext!.text = _initialHp.ToString();

        UISet(_currentHp);
    }

    public void Damage(int damage)
    {
        var finalDamage = damage * _currentDamageDown;
        _currentHp -= (int)finalDamage;
        UISet(_currentHp);
    }

    private void UISet(int hp)
    {
        _currentHpText!.text = hp.ToString();

        _hpSlider.value = (float)hp / (float)_initialHp;
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
}