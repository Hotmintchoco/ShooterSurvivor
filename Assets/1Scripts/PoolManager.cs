using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject boxPrefab;
    List<GameObject>[] pools;
    List<GameObject> boxPool;

    void Awake()
    {
        pools = new List<GameObject>[EnemyPrefabs.Length];
        boxPool = new List<GameObject>();

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject selectEnemy = Instantiate(EnemyPrefabs[index], transform);
        pools[index].Add(selectEnemy);
        
        return selectEnemy;
    }

    public GameObject GetBox()
    {
        GameObject itemBox = Instantiate(boxPrefab, transform);
        boxPool.Add(itemBox);

        return itemBox;
    }
}
