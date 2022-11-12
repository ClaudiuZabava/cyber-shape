using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject healthPrefab;
    List<HealthChunk> health = new List<HealthChunk>();
    public Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start()
    {
        DrawHealth();
    }

    void Update()
    {
        for (int i = 0; i < player.max_health; i++)
        {
            health[i].SetHealth(i + 1 <= player.current_health);
        }
    }

    public void DrawHealth()
    {
        ClearHealth();
        for (int i = 0; i < player.max_health; i++)
        {
            CreateHealthChunk();
        }

    }

    private void CreateHealthChunk()
    {
        GameObject newHealth = Instantiate(healthPrefab);
        newHealth.transform.SetParent(transform);

        HealthChunk hpComponent = newHealth.GetComponent<HealthChunk>();
        hpComponent.SetHealth(true);
        health.Add(hpComponent);
    }

    private void ClearHealth()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        health = new List<HealthChunk>();
    }
}
