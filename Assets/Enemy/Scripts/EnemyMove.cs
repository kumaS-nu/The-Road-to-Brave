using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float _moveDist = 0.1f;            // �ړ�����
    private Vector3 _moveDirection;             // �ړ�����

    private Vector3 _referencePosition;         // ��ƂȂ���W
    private Vector3 _targetPosition;            // �ڕW�ƂȂ���W
    private float _movementLimitTimer = 0.0f;   // �ړ��������s���^�C�}�[
    private bool _moveFlag = false;             // �ړ��t���O
    private int _lastDirection;
    private bool _CompleteFirstMoveFlag;        

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _moveDirection = Vector3.zero;

        _referencePosition = transform.position;

        _targetPosition = Vector3.zero;

        _lastDirection = -1;

        _CompleteFirstMoveFlag = false;

    }

    private void Update()
    {
        if (_moveFlag == false)
        {
            CountTimer();
        }
        else
        {
            ChangeTarget();

            transform.position += _moveDirection * _moveDist;
        }
    }


    private void ChangeTarget()
    {
        if (transform.position == _targetPosition)
        {
            _moveFlag = false;
            _movementLimitTimer = 0.0f;
            _moveDirection = -_moveDirection;
        }

        if (transform.position == _referencePosition)
        {
            int randDirection;
            do
            {
                randDirection = (int)Random.Range(0.0f, 4.0f);
            } while (randDirection == _lastDirection);

            switch (randDirection)
            {
                case 0:
                    _moveDirection = Vector3.up;
                    break;
                case 1:
                    _moveDirection = Vector3.down;
                    break;
                case 2:
                    _moveDirection = Vector3.right;
                    break;
                case 3:
                    _moveDirection = Vector3.left;
                    break;
            }

            // �ڕW���W�̕ύX
            _targetPosition = _referencePosition + _moveDirection * 1.0f;


            if (_CompleteFirstMoveFlag == false)
            {
                _CompleteFirstMoveFlag = true;
            }
            else
            {
                _moveFlag = false;
                _movementLimitTimer = 0.0f;
                return;
            }

            _lastDirection = randDirection;
        }

    }
    private void CountTimer()
    {
        _movementLimitTimer += Time.deltaTime;

        if (_movementLimitTimer >= 5.0f)
        {
            _moveFlag = true;
        }
    }
}
