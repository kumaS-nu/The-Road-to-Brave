using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveController : MonoBehaviour
{
    [SerializeField]
    BraveHp _braveHp;
    [SerializeField]
    public BraveMove BraveMove;

    public void Cheere()
    {
        _braveHp.OnCheere();
        BraveMove.OnCheere();
    }
}
