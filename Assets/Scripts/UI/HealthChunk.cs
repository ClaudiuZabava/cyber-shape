using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChunk : MonoBehaviour
{
    public Sprite FullChunk;
    public Sprite BlankChunk;
    Image healthImage;

    private void Awake()
    {
        healthImage = GetComponent<Image>();
    }

    public void SetHealth(bool Full)
    {
        if (Full)
        {
            healthImage.sprite = FullChunk;
        }
        else
        {
            healthImage.sprite = BlankChunk;
        }
    }
}
