using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public MainRoad MainRoadPrefab;
    [Header("--------------------Cars Configs--------------------")]
    [Space(10)]
    public float CarsSpeed;
    public List<GameObject> CarsPrefabs;
    public List<float> CarsPositions;
    public float MinCarSpawnTime;
    public float MaxCarSpawnTime;
    [Header("--------------------Decors Configs--------------------")]
    public List<GameObject> DecorsPrefabs;
    public List<float> DecorsPositions;
    public float MinDecorSpawnTime;
    public float MaxDecorSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnDecors());
    }
    

    private IEnumerator SpawnCars()
    {
        foreach (var car in CarsPrefabs)
        {
            yield return new WaitForSeconds(Random.Range(MinCarSpawnTime, MaxCarSpawnTime));

            var spawnPositionsY = car.transform.position.y;

            float spawnPositionsX = CarsPositions[Random.Range(0, CarsPositions.Count)];

            Instantiate(car, new Vector3(spawnPositionsX, spawnPositionsY, transform.position.z),
                Quaternion.Euler(0, spawnPositionsX > 0 ? 0 : 180, 0)).GetComponent<ObjectMover>().
                SetSpeed(spawnPositionsX > 0 ? CarsSpeed - 5 : CarsSpeed + 15);
        }
        StartCoroutine(SpawnCars());
    }
    private IEnumerator SpawnDecors()
    {
        MainRoad road = Instantiate(MainRoadPrefab, transform.position, Quaternion.identity);
        Transform roadTransform = road.transform;
        Vector3 startRoadPosition = roadTransform.position;
        foreach (var col in road.DecorColliders)
        {
            for (int i = 0; i < DecorsPrefabs.Count; i++)
            {
                var spawnPosition = GetRandomPointInCollider(col);
                Instantiate(DecorsPrefabs[Random.Range(0, DecorsPrefabs.Count)], spawnPosition, Quaternion.identity).GetComponent<ObjectMover>().SetSpeed(25);
                //yield return new WaitForSeconds(Random.Range(MinDecorSpawnTime, MaxDecorSpawnTime));
            }
            
        }
        yield return new WaitUntil(() => Mathf.Abs(startRoadPosition.z - roadTransform.position.z) >= road.RoadCollider.bounds.size.z);
        StartCoroutine(SpawnDecors());
    }
    private Vector3 GetRandomPointInCollider(Collider col)
    {
        Vector3 point = new Vector3
            (
            Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z)
            );
        if (col.bounds.Contains(point))
        {
            return point;
        }
        else
        {
            return GetRandomPointInCollider(col);
        }
    }
}
