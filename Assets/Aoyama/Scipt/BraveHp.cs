using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveHp : MonoBehaviour
{
    [SerializeField]
    private int _initialHp = 100;
    [SerializeField]
    private Text _initialHptext;
    [SerializeField]
    private Text _currentHpText;
    [SerializeField]
    private Slider _hpSlider;

    private int _currentHp = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _currentHp = _initialHp;
        _initialHptext!.text = _initialHp.ToString();

        UISet(_currentHp);
    }

    public void Damage(int damage)
    {
        _currentHp -= damage;
        UISet(_currentHp);
    }

    private void UISet(int hp)
    {
        _currentHpText!.text = hp.ToString();

        _hpSlider.value = hp / _initialHp;
    }
}
