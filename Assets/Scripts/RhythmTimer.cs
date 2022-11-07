using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimer : MonoBehaviour
{

    private float currentTime; //the time between updates
    private float lastTime; //previous time of audio source
    public float Timer; //current time since last beat
    public float interval; //time it takes for 1 beat

    public int bpm = 120; //default value, change in inspector for each track
    public AudioSource track; //the audio source it does the rhythm timing for


    private void Awake()
    {
        currentTime = 0f;
        lastTime = 0f;
        Timer = 0f;
        interval = 60f / bpm;
        track = GetComponent<AudioSource>();
    }

    public bool CheckTime(float pity_Time) //checks if time since last beat is in the decided time interval for the next beat
    {
        if (Timer >= interval - pity_Time || Timer <= pity_Time) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (lastTime > GetComponent<AudioSource>().time)
        {
            lastTime = 0f;
        }
        currentTime = track.time - lastTime;
        Timer += currentTime;

        if (Timer >= interval)
        {
            Timer = Timer - interval;
        }
        lastTime = GetComponent<AudioSource>().time;
    }
}
