using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmUI : MonoBehaviour
{
    private float _distance;
    private RhythmTimer _time;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject pityPrefab;
    [SerializeField] private int numOfBeats = 5;

    private List<RhythmLine> _lines = new();
    // Start is called before the first frame update
    void Awake()
    {
        _distance = GetComponent<RectTransform>().sizeDelta.x/2; //distanta de la un capat la centru, start position pt linii e ori distance ori -distance pt x, si acelasi y ca bara
    }

    private void Start()
    {
        //_time = GameObject.Find("Game Manager").GetComponent<RhythmTimer>().getInterval(); //cat timp ii ia unei linii ce reprezinta cel mai apropiat beat sa ajunga la centru
        _time = GameObject.Find("Game Manager").GetComponent<RhythmTimer>();
        InitialDraw();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_distance);
    }

    private void ClearLines()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        _lines = new List<RhythmLine>();
    }

    private void InitialDraw()
    {
        ClearLines();
        for(int i=1; i<=numOfBeats;i++)
        {
            CreateLine(i);
        }
        for (int i = -1; i >= -numOfBeats; i--)
        {
            CreateLine(i);
        }
    }

    private void CreateLine(int i)
    {
        var newLine = Instantiate(linePrefab, transform, true);

        newLine.GetComponent<RectTransform>().localPosition = new Vector2(i * _distance / numOfBeats, 0);

        var lineComponent = newLine.GetComponent<RhythmLine>();

        lineComponent.SetTime(_time.getInterval() - _time.getTime());
        
        lineComponent.SetDistance(-Mathf.Sign(i) * _distance / numOfBeats);
        lineComponent.SetStartPos(Mathf.Sign(i) * _distance);

        _lines.Add(lineComponent);
    }


    private void FixedUpdate()
    {
        
    }
}
