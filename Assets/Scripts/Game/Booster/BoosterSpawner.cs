using Pong.ScriptableEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pong
{
    public class BoosterSpawner : MonoBehaviour, IPoolHandler<BaseBooster>
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private ScriptableEventsHub eventsHub;

        [Space(10)]
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
        [SerializeField] private Transform boostersPoolContainer;


        [Space(10)]
        [SerializeField] private List<BaseBooster> boosterPrefabs;

        private List<BaseBooster> boostersPool = new List<BaseBooster>();

        private List<BaseBooster> activeBoosters = new List<BaseBooster>();

        public void StartBoosterSpawnSequence()
        {
            StartCoroutine(BoosterSpawnSequence(gameSettings.BoosterSpawnTime));
        }

        public void ResetBoosters()
        {
            List<BaseBooster> stillActiveBoosters = new List<BaseBooster>(activeBoosters);

            foreach (var booster in stillActiveBoosters)
            {
                ReturnToPool(booster);
            }
        }

        public void ReturnToPool(BaseBooster booster)
        {
            booster.gameObject.SetActive(false);
            activeBoosters.Remove(booster);
            boostersPool.Add(booster);
        }

        public T GetFromPool<T>() where T : BaseBooster
        {
            if (boostersPool.Count == 0)
            {
                return null;
            }

            BaseBooster booster = boostersPool.FirstOrDefault(poolBooster => boostersPool is T);

            if (booster != null)
            {
                boostersPool.Remove(booster);
                activeBoosters.Add(booster);
                booster.gameObject.SetActive(true);
            }

            return booster as T;
        }

        public void SpawnRandomBooster()
        {
            List<Transform> freeSpawnPoints = spawnPoints.Where(spawnPointTransform =>
            {
                foreach (BaseBooster booster in activeBoosters)
                {
                    if ((booster.transform.position - spawnPointTransform.transform.position).sqrMagnitude < Mathf.Epsilon)
                    {
                        return false;
                    }
                }

                return true;
            }).ToList();

            if (freeSpawnPoints.Count == 0)
            {
                Debug.LogWarning("[BoosterSpawner] SpawnRandomBooster, can't spawn new booster, all spawn points are occupied");
                return;
            }

            Vector3 spawnPosition = freeSpawnPoints[UnityEngine.Random.Range(0, freeSpawnPoints.Count)].position;

            BaseBooster booster = null;

            booster = GetRandomBooster();

            if (booster != null)
            {
                booster.transform.position = spawnPosition;
            }
            else
            {
                Debug.LogError("[BoosterSpawner] SpawnRandomBooster, can't spawn new booster, could not get prefab instance neither from pool nor from prefabs instantiation");
            }
        }

        private BaseBooster GetRandomBooster()
        {
            float[] randomRanges = new float[gameSettings.BoosterTypeSpawnWieghts.Length];

            float sumRange = 0;

            for (int i = 0; i < randomRanges.Length; i++)
            {
                sumRange += gameSettings.BoosterTypeSpawnWieghts[i];
                randomRanges[i] = sumRange;
            }

            float random = UnityEngine.Random.Range(0, sumRange);

            if (randomRanges.Length >= 1 && random < randomRanges[0])
            {
                return GetBooster<CloneBallBooster>();
            }
            else if (randomRanges.Length >= 2 && random < randomRanges[1])
            {
                return GetBooster<DecreasePlatformSizeBooster>();
            }
            else if (randomRanges.Length >= 3 && random < randomRanges[2])
            {
                return GetBooster<IncreasePlatformSizeBooster>();
            }
            else if (randomRanges.Length >= 4 && random < randomRanges[3])
            {
                return GetBooster<ReverseControlBooster>();
            }

            return null;
        }

        private T GetBooster<T>() where T : BaseBooster
        {
            BaseBooster booster = GetFromPool<T>();

            if (booster == null)
            {
                booster = InstantiateBoosterPrefab<T>();
            }

            return booster as T;
        }

        private T InstantiateBoosterPrefab<T>() where T : BaseBooster
        {
            BaseBooster baseBoosterPrefab = boosterPrefabs.FirstOrDefault(s => s is T);

            if (baseBoosterPrefab == null)
            {
                return null;
            }

            BaseBooster booster = Instantiate(baseBoosterPrefab, boostersPoolContainer);
            booster.name = $"Booster {boostersPool.Count + activeBoosters.Count}";
            activeBoosters.Add(booster);
            booster.SetPoolHandler(this);

            return booster as T;
        }

        private IEnumerator BoosterSpawnSequence(float seconds)
        {
            while (true)
            {
                yield return new WaitForSeconds(seconds);

                SpawnRandomBooster();
            }
        }
    }
}
