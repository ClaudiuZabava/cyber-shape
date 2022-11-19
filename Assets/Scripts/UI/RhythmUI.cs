using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmUI : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject pityPrefab;
    [SerializeField] private int numOfBeats = 5;

    private float _distance;
    private RhythmTimer _time;
    private List<RhythmLine> _lines = new();
    // Start is called before the first frame update
    void Awake()
    {
        _distance = GetComponent<RectTransform>().sizeDelta.x/2; //distanta de la un capat la centru, start position pt linii e ori distance ori -distance pt x, si acelasi y ca bara
    }

    private void Start()
    {
        _time = GameObject.Find("Game Manager").GetComponent<RhythmTimer>();
        InitialDraw();
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
    }

    private void CreateLine(int i)
    {
        var j = -i;
        var newLine = Instantiate(linePrefab, transform, true);
        var newLine2 = Instantiate(linePrefab, transform, true);

        newLine.GetComponent<RectTransform>().localPosition = new Vector2(i * _distance / numOfBeats, 0);
        newLine2.GetComponent<RectTransform>().localPosition = new Vector2(j * _distance / numOfBeats, 0);

        var lineComponent = newLine.GetComponent<RhythmLine>();
        var lineComponent2 = newLine2.GetComponent<RhythmLine>();

        lineComponent.SetTime(_time.GetInterval() - _time.GetTime());
        lineComponent2.SetTime(_time.GetInterval() - _time.GetTime());

        lineComponent.SetDistance(-Mathf.Sign(i) * _distance / numOfBeats);
        lineComponent.SetStartPos(Mathf.Sign(i) * _distance);
        lineComponent2.SetDistance(-Mathf.Sign(j) * _distance / numOfBeats);
        lineComponent2.SetStartPos(Mathf.Sign(j) * _distance);

        var dist_offset = lineComponent.GetSpeed() * _time.GetOffset();

        newLine.GetComponent<RectTransform>().localPosition = new Vector2(i * _distance / numOfBeats - Mathf.Sign(i) * dist_offset, 0);
        newLine2.GetComponent<RectTransform>().localPosition = new Vector2(j * _distance / numOfBeats - Mathf.Sign(j) * dist_offset, 0);

        _lines.Add(lineComponent);
        _lines.Add(lineComponent2);
    }

}
