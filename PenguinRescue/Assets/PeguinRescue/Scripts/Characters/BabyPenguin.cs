using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class BabyPenguin : ChaseBehavior
{
    private bool _wasCaptured;

    public bool WasCaptured { get => _wasCaptured; set => _wasCaptured = value; }

    private void Awake()
    {
        _canMove = true;
    }

    public void Save()
    {
        _wasSaved = true;
        _player = null;

        PenguinGameManager.Instance.PenguinSaved();
    }

    public void Trapped()
    {
        _wasSaved = false;
        _player = null;
        PenguinGameManager.Instance.PenguinTrapped();
        Destroy(gameObject);
    }
}
