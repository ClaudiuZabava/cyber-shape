using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmLine : MonoBehaviour
{
    private float _time=0;
    private float _distance=0;
    private float _startpos=0;

    public void SetTime(float time)
    {
        _time = time;
    }

    public void SetDistance(float distance)
    {
        _distance = distance;
    }

    public void SetStartPos(float pos)
    {
        _startpos = pos;
    }

    public float GetSpeed()
    {
        return _distance / _time;
    }

    private void CheckPosition()
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
        transform.position += new Vector3(_distance *Time.deltaTime/_time, 0,0);

        CheckPosition();
    }
}
