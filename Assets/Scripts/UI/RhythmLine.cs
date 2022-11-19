using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmLine : MonoBehaviour
{
    private float _time;
    private float _distance;
    private float _startpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTime(float time)
    {
        _time = time;
        _time = _time / Time.deltaTime;
    }

    public void SetDistance(float distance)
    {
        _distance = distance;
    }

    public void SetStartPos(float pos)
    {
        _startpos = pos;
    }

    private void checkPosition()
    {
        if(_distance < 0)
        {
            if (GetComponent<RectTransform>().localPosition.x <= 0.0f)
            {

                GetComponent<RectTransform>().localPosition = new Vector3(_startpos, 0, 0);
            }
        }
        if(_distance > 0)
        {
            if (GetComponent<RectTransform>().localPosition.x >= 0.0f)
            {
                GetComponent<RectTransform>().localPosition = new Vector3(_startpos, 0, 0);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += new Vector3(_distance/_time, 0,0);
        checkPosition();
    }
}
