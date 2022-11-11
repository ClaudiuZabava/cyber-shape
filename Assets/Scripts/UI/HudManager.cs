using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{

    public PlayerHealth hp;
    void Awake()
    {
        hp = GameObject.Find("PlayerHealth").GetComponent<PlayerHealth>();
    }

}
