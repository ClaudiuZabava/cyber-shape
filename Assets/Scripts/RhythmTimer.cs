using UnityEngine;

public class RhythmTimer : MonoBehaviour
{
    [SerializeField] private int bpm = 130; // default value, change in inspector for each track
    [SerializeField] private float timer; // current time since last beat
    
    private float _currentTime; // the time between updates
    private float _lastTime; // previous time of audio source
    private float _interval; // time it takes for 1 beat
    private AudioSource _track; // the audio source it does the rhythm timing for

    private void Awake()
    {
        _currentTime = 0f;
        _lastTime = 0f;
        timer = 0f;
        _interval = 60f / bpm;
        _track = GetComponent<AudioSource>();
    }
    
    private void FixedUpdate()
    {
        if (_lastTime > _track.time)
        {
            _lastTime = 0f;
        }
        _currentTime = _track.time - _lastTime;
        timer += _currentTime;

        if (timer >= _interval)
        {
            timer -= _interval;
        }
        _lastTime = _track.time;
    }

    public bool CheckTime(float pityTime) // checks if time since last beat is in the decided time interval for the next beat
    {
        return timer >= _interval - pityTime || timer <= pityTime;
    }
}
