using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class ConsumablesSpawner : MonoBehaviour
{
    [SerializeField] private ConsumablesKeeper consumablesKepper;
    [SerializeField] Transform leftExtremePositionTransform;
    [SerializeField] Transform rightExtremePositionTransform;
    [SerializeField] private GameObject consumablesReturner;

    [Range(0.3f, 1.3f)]
    [SerializeField] private float timeBetweenSpawn;
    private Vector2 spawnPosition;
    private Consumables spawningConsmables;
    private bool needToSpawn;
    private bool spawnIsPaused;

    private void Start()
    {
        consumablesReturner.gameObject.SetActive(false);
    }

    public void StartSpawn()
    {
        consumablesReturner.gameObject.SetActive(false);
        needToSpawn = true;
        spawnIsPaused = false;
        SpawnFruit();
    }

    public void StopSpawn()
    {
        needToSpawn = false;
        consumablesReturner.gameObject.SetActive(true);
    }

    private async UniTask SpawnFruit()
    {
        spawnPosition.y = Camera.main.ScreenToWorldPoint(leftExtremePositionTransform.position).y;
        Spawn();
        while (needToSpawn)
        {
            await UniTask.Delay((int)(timeBetweenSpawn * 1000));
            if (spawnIsPaused)
            {
                continue;
            }
            Spawn();
        }
    }

    private void Spawn()
    {
        spawnPosition.x = Random.Range(Camera.main.ScreenToWorldPoint(leftExtremePositionTransform.position).x, Camera.main.ScreenToWorldPoint(rightExtremePositionTransform.position).x);

        spawningConsmables = consumablesKepper.GetConsumablesFromPool();
        spawningConsmables.transform.position = spawnPosition;
        spawningConsmables.gameObject.SetActive(true);
    }
}
