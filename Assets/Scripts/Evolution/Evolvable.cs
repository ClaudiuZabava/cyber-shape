using UnityEngine;

namespace Evolution
{
    public class Evolvable : MonoBehaviour
    {
        [field: SerializeField] public Stage Stage { get; private set; }

        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private Animator _animator;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            _animator = GetComponent<Animator>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        private void Start()
        {
            ApplyStage();
        }

        public void Evolve()
        {
            if (Stage.NextStage is null)
            {
                return;
            }
            
            _particleSystem.Play();

            Stage = Stage.NextStage;
            _animator.SetTrigger(Constants.Animations.Evolution.Triggers.Evolution);
            var animatorState = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("Base Layer"));
            Invoke(nameof(ApplyStage), animatorState.length / 4);
        }

        private void ApplyStage()
        {     
            _meshFilter.mesh = Stage.Mesh;
            _meshCollider.sharedMesh = Stage.Mesh;
        }
    }
}
