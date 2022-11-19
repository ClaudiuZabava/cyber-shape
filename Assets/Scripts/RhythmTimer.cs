using UI;
using UnityEngine;


public class RhythmTimer : MonoBehaviour
{
    [SerializeField] private float bpm = 135.0f; // default value, change in inspector for each track
    [SerializeField] private float offset = 0.045f;

    private float _interval; // time it takes for 1 beat
    private AudioSource _track; // the audio source it does the rhythm timing for
    private float _lastBeat = 0.0f;
    private float _dsptimesong = 0.0f;

    private void Awake()
    {
        _interval = 60.0f / bpm;
        _track = GetComponent<AudioSource>();
        _dsptimesong = (float)AudioSettings.dspTime;
        GetComponent<AudioSource>().Play();
    }

    public float GetInterval()
    {
        return _interval;
    }

    public float GetTime()
    {
        return _track.time;
    }
    
    public float GetOffset()
    {
        return offset;
    }

    private float TrackTime()
    {
        return (float)(AudioSettings.dspTime - _dsptimesong) - offset;
    }

    private void FixedUpdate()
    {
      if(TrackTime() >= _lastBeat + _interval)
        {
            _lastBeat = TrackTime();
        }
    }

    public bool CheckTime(float pityTime = 0) // checks if time since last beat is in the decided time interval for the next beat
    {
        return (TrackTime() >= (_lastBeat + _interval) - pityTime) || (TrackTime() <= _lastBeat + pityTime);
    }
}
