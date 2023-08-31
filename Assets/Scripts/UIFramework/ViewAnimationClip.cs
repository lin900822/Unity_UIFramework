using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    [CreateAssetMenu(fileName = "ViewAnimationClip", menuName = "UIFramework/ViewAnimationClip")]
    public class ViewAnimationClip : ScriptableObject
    {
        public List<PositionAnimation> PositionAnimations;
        public List<RotationAnimation> RotationAnimations;
        public List<ScaleAnimation>    ScaleAnimations;
        public List<AlphaAnimation>    AlphaAnimations;
    }
}