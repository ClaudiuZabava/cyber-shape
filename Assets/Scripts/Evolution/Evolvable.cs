using UnityEngine;

namespace Evolution
{
    public class Evolvable : MonoBehaviour
    {
        [SerializeField]
        private Stage currentStage;

        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private Animator _animator;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            ApplyStage();
        }

        private void Update()
        {
            // TODO: remove this; it's here only for demonstrating how this works.
            if (Input.GetMouseButtonDown(0))
            {
                Evolve();
            }
        }

        private void Evolve()
        {
            if (currentStage.nextStage is null)
            {
                return;
            }
            
            currentStage = currentStage.nextStage;
            _animator.SetTrigger(Constants.Animations.Evolution.Triggers.Evolution);
            var animatorState = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("Base Layer"));
            Invoke(nameof(ApplyStage), animatorState.length / 4);
        }

        private void ApplyStage()
        {            
            _meshFilter.mesh = currentStage.mesh;
            _meshCollider.sharedMesh = currentStage.mesh;
        }
    }
}
