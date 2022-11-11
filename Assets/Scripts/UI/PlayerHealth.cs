using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject healthPrefab;
    public int current_health, max_health;
    List<HealthChunk> health = new List<HealthChunk>();

    private void Start()
    {
        DrawHealth();
    }

    void Update()
    {
        for (int i = 0; i < max_health; i++)
        {
            health[i].SetHealth(i + 1 <= current_health);
        }
    }

    public void UpdateMaxHealth(int max)
    {
        max_health = max;
        DrawHealth();
    }

    private void DrawHealth()
    {
        ClearHealth();
        for (int i = 0; i < max_health; i++)
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
