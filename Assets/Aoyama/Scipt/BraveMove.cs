using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BraveMove : MonoBehaviour
{
    [SerializeField] 
    Transform[] _points;

    private Vector3 _nextPosition;
    private int _index = 0;

    private void Start()
    {
        List<Vector3> points = new List<Vector3>();
        foreach (var point in _points)
        {
            points.Add(point.position);
        }

        Move(points.ToArray());
    }

    //private void Update()
    //{
    //    float distance = Vector3.Distance(transform.position, _nextPosition);
    //    if(distance <= 0.5)
    //    {
    //        PointUpdate();
    //    }
    //}

    private void PointUpdate()
    {
        _index++;
        _nextPosition = _points[_index].position;
    }

    private void Move(Vector3[] pos)
    {
        transform.DOLocalPath(pos, 10.0f, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .SetOptions(false, AxisConstraint.Z)
                .onComplete();
    }
}
