using UnityEngine;

public class RhythmTimer : MonoBehaviour
{
    [field: SerializeField] public float Offset { get; private set; } = 0.045f;
    [SerializeField] private float bpm = 135.0f; // default value, change in inspector for each track

    public float Interval { get; private set; }
    public float LastBeat { get; private set; }
    public float Time => _track.time;

    private float _dspTimeSong;
    private AudioSource _track; // the audio source it does the rhythm timing for

    private void Awake()
    {
        Interval = 60.0f / bpm;
        _track = GetComponent<AudioSource>();
       // LastBeat = Offset - Interval;
    }

    private void Start()
    {
        _dspTimeSong = (float)AudioSettings.dspTime;
        LastBeat = Offset - _dspTimeSong;
        _track.Play();
    }

    private void Update()
    {
        if (TrackTime() >= LastBeat + Interval)
        {
            LastBeat += Interval;
        }
    }

    public float TrackTime()
    {
        // return (float)(AudioSettings.dspTime - _dspTimeSong) + Offset;
        return (float)(AudioSettings.dspTime - _dspTimeSong - Offset);
    }

    public bool CheckTime(float pityTime = 0) // checks if time since last beat is in the decided time interval for the next beat
    {
        return TrackTime() >= LastBeat + Interval - pityTime || TrackTime() <= LastBeat + pityTime;
    }
}
