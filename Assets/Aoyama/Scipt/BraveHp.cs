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
    private float _curreentDamageDown = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _curreentDamageDown = 0;

        _currentHp = _initialHp;
        _initialHptext!.text = _initialHp.ToString();

        UISet(_currentHp);
    }

    public void Damage(int damage)
    {
        _currentHp -= damage - _damageDown;
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
        _curreentDamageDown = _damageDown;
        yield return new WaitForSeconds(_curreentDamageDown);
        _curreentDamageDown = 0;
    }
}