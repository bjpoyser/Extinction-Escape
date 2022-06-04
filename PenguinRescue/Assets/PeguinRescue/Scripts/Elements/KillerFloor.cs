using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class KillerFloor : MonoBehaviour
{
    [SerializeField] private Orca _orcaPrefab;
    [SerializeField] private GameTags[] _targetTags;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < _targetTags.Length; i++)
        {
            if (other.CompareTag(_targetTags[i].ToString()))
            {

                switch (_targetTags[i])
                {
                    case GameTags.Penguin:
                        {
                            PenguinController playerPenguin = other.GetComponent<PenguinController>();
                            if (playerPenguin != null && !playerPenguin.WasCaptured)
                            {
                                playerPenguin.CanMove = false;
                                playerPenguin.WasCaptured = true;
                                SpawnOrca(other.transform);
                            }

                            break;
                        }

                    case GameTags.BabyPenguin:
                        {
                            BabyPenguin babyPenguin = other.GetComponent<BabyPenguin>();
                            if (babyPenguin != null && !babyPenguin.WasCaptured)
                            {
                                babyPenguin.CanMove = false;
                                babyPenguin.WasCaptured = true;
                                SpawnOrca(other.transform);
                            }

                            break;
                        }

                    case GameTags.PenguinSpawner:
                        {
                            PenguinSpawner spawner = other.GetComponent<PenguinSpawner>();
                            if (spawner != null)
                            {
                                Debug.Log("Deactivated");
                                spawner.IsEnabled = false;
                            }

                            break;
                        }
                }
            }
        }
    }

    private void SpawnOrca(Transform pTarget)
    {
        if (_orcaPrefab != null)
        {
            Orca orca = Instantiate(_orcaPrefab);

            Vector3 initialPosition = pTarget.position;
            Vector3 targetPosition = pTarget.position;
            initialPosition.y = initialPosition.y - 15;
            targetPosition.y = initialPosition.y + 15;

            orca.transform.position = initialPosition;
            orca.transform.rotation = Quaternion.Euler(0, -90, 90);
            orca.SetTarget(targetPosition, initialPosition);
        }
    }
}
