using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BraveMove : MonoBehaviour, ICheere
{
    [Header("�����֌W")]
    [SerializeField]
    private float _speedUpTime = 0.1f;
    [SerializeField]
    private float _speedUpScale = 1.2f;
    [Header("�ړ��֌W")]
    [SerializeField]
    private float _initialRoundTime = 15f;
    [SerializeField]
    Transform[] _points;
    [SerializeField]
    Transform _mostLeftPositon;
    [SerializeField]
    Transform _mostRightPositon;
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    private float _roundTime = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _roundTime = _initialRoundTime;

        List<Vector3> points = new List<Vector3>();
        foreach (var point in _points)
        {
            points.Add(point.position);
        }

        Move(points.ToArray());
    }

    private Tween _tween;
    private void Move(Vector3[] pos)
    {
        _tween = transform.DOLocalPath(pos, _roundTime, PathType.CatmullRom)
                    .SetEase(Ease.Linear)
                    .SetOptions(false, AxisConstraint.Z)
                    .OnComplete(() => Move(pos));
    }

    public void SetRoundTime(float roundTime)
    {
        _roundTime = roundTime;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _mostLeftPositon.position) <= 0.4)
        {

        }
    }

    public void OnCheere()
    {
        StartCoroutine(SpeedUpCor());
    }

    IEnumerator SpeedUpCor()
    {
        _tween.timeScale = _speedUpScale;
        yield return new WaitForSeconds(_speedUpTime);
        _tween.timeScale = default;
    }
}