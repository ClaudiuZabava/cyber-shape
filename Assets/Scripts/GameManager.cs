using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairImg;
    
    [Header("Enemy settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int constEnemies = 7;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int maxDistance = 5;
    [SerializeField] private float secondsTillSpawn = 3f;
    [SerializeField] private float percentToNextWave = .75f;

    private bool _spawning = false;
    private void Start()
    {
        var hotSpot = new Vector2(crosshairImg.width / 2f, crosshairImg.height / 2f);
        Cursor.SetCursor(crosshairImg, hotSpot, CursorMode.Auto);

        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (!_spawning)
        {
            CheckNoEnemies();
        }
    }

    private void CheckNoEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < percentToNextWave * constEnemies)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        while (true)
        {
            var randomPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 0.5f, Random.Range(-maxWidth, maxWidth));
            var distance = Vector3.Distance(transform.position, randomPosition);
            if (distance < maxDistance)
            {
                continue;
            }

            return randomPosition;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        _spawning = true;
        yield return new WaitForSeconds(secondsTillSpawn);
        for (var i = 0; i < constEnemies; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        }

        _spawning = false;
    }
}
