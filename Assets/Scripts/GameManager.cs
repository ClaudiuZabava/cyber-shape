using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairImg;
    
    [Header("Enemy settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int constEnemies = 7;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int maxDistance = 5;
    [SerializeField] private float secondsTillSpawn = 3f;

    private void Start()
    {
        var hotSpot = new Vector2(crosshairImg.width / 2f, crosshairImg.height / 2f);
        Cursor.SetCursor(crosshairImg, hotSpot, CursorMode.Auto);
        
        StartCoroutine(SpawnEnemies());
    }
    
    private Vector3 GetRandomPosition()
    {
        while (true)
        {
            var randomPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 0.95f, Random.Range(-maxWidth, maxWidth));
            var distance = Vector3.Distance(transform.position, randomPosition);
            if (distance < maxDistance)
            {
                continue;
            }

            return randomPosition;
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(secondsTillSpawn);
        for (var i = 0; i < constEnemies; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        }
    }
}
