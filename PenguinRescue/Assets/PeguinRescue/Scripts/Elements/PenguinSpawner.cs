using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private BabyPenguin _prefab;

    private bool _isEnabled;
    private BabyPenguin _babySpawned;

    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
    public BabyPenguin BabySpawned { get => _babySpawned; set => _babySpawned = value; }

    private void Awake()
    {
        _isEnabled = true;
    }

    public void SpawnBabyPenguin()
    {
        _babySpawned = Instantiate(_prefab, transform);
        _babySpawned.transform.SetParent(null);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_babySpawned != null && other.gameObject == _babySpawned.gameObject)
        {
            _babySpawned = null;
        }
    }
}
