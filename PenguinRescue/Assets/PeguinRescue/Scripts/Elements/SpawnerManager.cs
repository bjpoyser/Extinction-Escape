using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] PenguinSpawner[] _spawns;
    #endregion

    #region Private Variables
    private static SpawnerManager _instance;
    #endregion

    #region Properties
    public static SpawnerManager Instance { get => _instance; set => _instance = value; }
    #endregion

    #region Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _spawns = transform.GetComponentsInChildren<PenguinSpawner>();
    }

    public void SpawnNextWave(int pSpawnersNeeded)
    {
        for (int i = 0; i < pSpawnersNeeded; i++)
        {
            PenguinSpawner spawner = GetSpawner();
            if (spawner != null && spawner.IsEnabled)
            {
                spawner.SpawnBabyPenguin();
                PenguinGameManager.Instance.PenguinsToSave++;
                PenguinGameManager.Instance.TotalPenguinsSpawned++;
            }
        }
    }

    private PenguinSpawner GetSpawner()
    {
        List<PenguinSpawner> availableSpawners = new List<PenguinSpawner>();
        for (int i = 0; i < _spawns.Length; i++)
        {
            if (_spawns[i].IsEnabled && !_spawns[i].IsSeaLeaopardNearby && _spawns[i].BabySpawned == null)
            {
                availableSpawners.Add(_spawns[i]);
            }
        }

        if (availableSpawners.Count > 0)
        {
            int index = Random.Range(0, availableSpawners.Count);
            return availableSpawners[index];
        }

        return null;
    }
    #endregion
}
