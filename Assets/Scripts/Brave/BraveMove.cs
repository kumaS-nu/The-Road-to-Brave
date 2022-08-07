using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BraveMove : MonoBehaviour
{
    [Header("‰ž‰‡ŠÖŒW")]
    [SerializeField]
    private float _speedUpTime = 0.1f;
    [SerializeField]
    private float _speedUpScale = 1.2f;
    [SerializeField]
    private BraveHp _braveHp;
    [SerializeField]
    private CheerManager _cheerManager;
    [Header("ˆÚ“®ŠÖŒW")]
    [SerializeField]
    private float _initialRoundTime = 15f;
    [SerializeField]
    Transform[] _movePoints;
    [SerializeField]
    Transform _mostLeftPositon;
    [SerializeField]
    Transform _mostRightPositon;
    [SerializeField]
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    SpawnPortion _spawnPortion;

    private float _roundTime = 0;
    private List<Vector3> _points = new List<Vector3>();
    private Vector3 _initPosition;
    private bool _beforeAllived = true;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _initPosition = transform.position;
        _roundTime = _initialRoundTime;

        foreach (var point in _movePoints)
        {
            _points.Add(point.position);
        }

        //Move(_points.ToArray());
        MoveTween = transform.DOLocalPath(_points.ToArray(), _roundTime, PathType.CatmullRom)
                    .OnPlay(() => _spawnPortion.Spawn())
                    .SetEase(Ease.Linear)
                    .SetOptions(false, AxisConstraint.Z)
                    .SetLoops(-1)
                    .OnStepComplete(() => _spawnPortion.Spawn());
    }

    public void Death()
    {
        MoveTween.Pause();
        Debug.Log("Dead");
    }

    public void Respawn()
    {
        Debug.Log("Resporn");
        transform.position = _initPosition;
        _spriteRenderer.flipX = true;
        Move(_points.ToArray());
    }

    public Tween MoveTween;
    private void Move(Vector3[] pos)
    {
        MoveTween.Restart();
        /*
        MoveTween = transform.DOLocalPath(pos, _roundTime, PathType.CatmullRom)
                    .OnPlay(() => _spawnPortion.Spawn())
                    .SetEase(Ease.Linear)
                    .SetOptions(false, AxisConstraint.Z)
                    .SetLoops(-1)
                    .OnStepComplete(() => _spawnPortion.Spawn());*/
    }

    public void SetRoundTime(float roundTime)
    {
        _roundTime = roundTime;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _mostLeftPositon.position) <= 0.4)
        {
            _spriteRenderer.flipX = false;
        }

        if (Vector3.Distance(transform.position, _mostRightPositon.position) <= 0.4)
        {
            _spriteRenderer.flipX = true;
        }

        if (_braveHp.IsLive)
        {
            MoveTween.timeScale = 1 + (0.2f + 0.1f * StageState.Instance.EnhancementLevel[EnhancementContent.Cheer]) * _cheerManager.GetCheerPower();
        }
    }
}
