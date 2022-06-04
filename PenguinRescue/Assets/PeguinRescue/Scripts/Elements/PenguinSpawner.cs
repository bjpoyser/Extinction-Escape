using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private BabyPenguin _prefab;

    private bool _isEnabled;
    private bool _isSeaLeaopardNearby;
    private BabyPenguin _babySpawned;

    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
    public BabyPenguin BabySpawned { get => _babySpawned; set => _babySpawned = value; }
    public bool IsSeaLeaopardNearby { get => _isSeaLeaopardNearby; set => _isSeaLeaopardNearby = value; }

    private void Awake()
    {
        _isEnabled = true;
        IsSeaLeaopardNearby = false;
    }

    public void SpawnBabyPenguin()
    {
        _babySpawned = Instantiate(_prefab, transform);
        _babySpawned.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.SeaLeopard.ToString()))
        {
            IsSeaLeaopardNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_babySpawned != null && other.gameObject == _babySpawned.gameObject)
        {
            _babySpawned = null;
        }

        if (other.CompareTag(GameTags.SeaLeopard.ToString()))
        {
            IsSeaLeaopardNearby = false;
        }
    }
}
