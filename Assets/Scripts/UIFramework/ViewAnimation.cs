using System;
using DG.Tweening;
using UnityEngine;

namespace UIFramework
{
    public abstract class ViewAnimation
    {
        public float StartTime;
        public float EndTime;

        public Ease           Ease;
        public AnimationCurve AnimationCurve;
    }

    [Serializable]
    public class PositionAnimation : ViewAnimation
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
    }

    [Serializable]
    public class RotationAnimation : ViewAnimation
    {
        public float StartAngle;
        public float EndAngle;
    }

    [Serializable]
    public class ScaleAnimation : ViewAnimation
    {
        public Vector2 StartScale;
        public Vector2 EndScale;
    }

    [Serializable]
    public class AlphaAnimation : ViewAnimation
    {
        public float StartAlpha;
        public float EndAlpha;
    }
}