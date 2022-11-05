using UnityEngine;

namespace Evolution
{
    [CreateAssetMenu(fileName = "EvolutionStageDetails", menuName = "Evolution/Stage Details")]
    public class Stage : ScriptableObject
    {
        public Mesh mesh;
        public Stage nextStage;
    }
}