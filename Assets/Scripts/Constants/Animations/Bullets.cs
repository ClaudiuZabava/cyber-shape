using UnityEngine;

namespace Constants.Animations
{
    public static class Bullets
    {
        public static readonly int Hide = Animator.StringToHash("HideAnimation");
        public static readonly int Reveal = Animator.StringToHash("Reveal");
    
        public static class Triggers
        {
            public static readonly int Show = Animator.StringToHash("Show");
            public static readonly int Hide = Animator.StringToHash("Hide");
        }
    }
}