using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveController : MonoBehaviour
{
    [SerializeField]
    BraveHp _braveHp;
    [SerializeField]
    BraveMove _braveMove;

    public void Cheere()
    {
        _braveHp.OnCheere();
        _braveMove.OnCheere();
    }
}
