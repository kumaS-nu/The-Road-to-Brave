using Cysharp.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveHp : MonoBehaviour
{
    [Header("HP関係")]
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
    [Header("Cheere関係")]
    [SerializeField]
    private CheerManager _cheerManager;
    [Header("動きへの参照")]
    [SerializeField]
    private BraveMove _braveMove;
    [Header("サウンド")]
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

    public bool IsLive { get; private set; } =  true;

    private void Start()
    {
        Init();
        _ = GetGuardLoop();
    }

    private void Init()
    {
        _currentDamageDown = 1.0f;

        _currentHp = _initialHp;
        _initialHptext.text = _initialHp.ToString();

        UISet(_currentHp);
    }

    public void Heal(int heal)
    {
        _healSound.Play();
        var finalHeal = Mathf.Min(_initialHp, _currentHp + heal);
        _currentHp = finalHeal;
        UISet(_currentHp);
    }

    public void Damage(int damage)
    {
        _damageSound.Play();
        _attackSound.Play();

        var finalDamage = damage * _currentDamageDown;
        _currentHp -= (int)finalDamage;
        UISet(_currentHp);
        if(_currentHp <= 0)
        {
            _ = Death();
        }
    }

    private async UniTask Death()
    {
        gameObject.layer = LayerMask.NameToLayer("God");
        StageState.Instance.BraveDeath();
        _deathSound.Play();
        _braveMove.Death();
        IsLive = false;
        _animator.SetTrigger("Death");
        await UniTask.Delay(_respawnTime * 1000);
        Debug.Log("Revive");
        _currentHp = _initialHp;
        UISet(_initialHp);
        IsLive = true;
        _braveMove.Respawn();
        _ = GetGuardLoop();
        gameObject.layer = LayerMask.NameToLayer("Default");
        _animator.SetTrigger("Revive");
    }

    private void UISet(int hp)
    {
        _currentHpText!.text = hp.ToString();

        _hpSlider.value = (float)hp / _initialHp;
    }

    public void HpUpdate()
    {
        var temp = _initialHp;
        _initialHp = StageState.Instance.HPTable[StageState.Instance.EnhancementLevel[EnhancementContent.Armor]];
        _initialHptext!.text = _initialHp.ToString();
        _currentHp += _initialHp - temp;
        _currentHpText!.text = _currentHp.ToString();
        _hpSlider.value = (float)_currentHp / _initialHp;
    }

    private async UniTask GetGuardLoop()
    {
        while (IsLive)
        {
            //Max -0.5 to -0.5 * 0.6, -0.5 * 0.6 * 0.6 ...
            _currentDamageDown = 1 - _cheerManager.GetCheerPower() * (1 - Mathf.Exp(-0.7f - 0.5f * StageState.Instance.EnhancementLevel[EnhancementContent.Cheer]));
            await UniTask.Yield();
        }
    }

    // 相互参照したくないけど...UniRx使えば無くせるだろうけどそもそもそれぞれ密にやりとりするこの設計が悪い．
}