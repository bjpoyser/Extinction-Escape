using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class BabyPenguin : ChaseBehavior
{
    [SerializeField] private GameObject _helpBubble;

    private bool _wasCaptured;

    public bool WasCaptured { get => _wasCaptured; set => _wasCaptured = value; }

    private void Awake()
    {
        _canMove = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _helpBubble.SetActive(true);
    }

    private void Update()
    {
        Move();
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

    protected override void OnFollowingPlayer()
    {
        _helpBubble.SetActive(false);
    }
}
