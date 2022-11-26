using UnityEngine;

namespace UI
{
    public class RhythmUI : MonoBehaviour
    {
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private int numOfBeats = 5;

        private float _distance;
        private RhythmTimer _time;

        private void Awake()
        {
            _distance = GetComponent<RectTransform>().sizeDelta.x / 2;
            _time = GameObject.Find("Game Manager").GetComponent<RhythmTimer>();
        }

        private void Start()
        {
            InitialDraw();
        }

        private void ClearLines()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        private void InitialDraw()
        {
            ClearLines();
            for (var i = 1; i <= numOfBeats; i++)
            {
                CreateLine(i);
            }
        }

        private void CreateLine(int i)
        {
            var j = -i;
            var newLine = Instantiate(linePrefab, transform, true);
            var newLine2 = Instantiate(linePrefab, transform, true);

            var lineComponent = newLine.GetComponent<RhythmLine>();
            var lineComponent2 = newLine2.GetComponent<RhythmLine>();

            lineComponent.Time = _time.Interval - _time.Time;
            lineComponent2.Time = _time.Interval - _time.Time;

            lineComponent.Distance = -Mathf.Sign(i) * _distance / numOfBeats;
            lineComponent.StartPos = Mathf.Sign(i) * _distance;
            lineComponent2.Distance = -Mathf.Sign(j) * _distance / numOfBeats;
            lineComponent2.StartPos = Mathf.Sign(j) * _distance;

            var distOffset = lineComponent.Speed() * _time.Offset;

            newLine.GetComponent<RectTransform>().localPosition = new Vector2(i * _distance / numOfBeats - Mathf.Sign(i) * distOffset, 0);
            newLine2.GetComponent<RectTransform>().localPosition = new Vector2(j * _distance / numOfBeats - Mathf.Sign(j) * distOffset, 0);
        }
    }
}
