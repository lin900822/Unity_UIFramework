using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace UIFramework
{
    [Serializable]
    public class ViewAnimationPlayer
    {
        private GameObject _target;

        private Sequence _sequence;

        private RectTransform _rectTransform;
        private CanvasGroup   _canvasGroup;

        public ViewAnimationPlayer(GameObject target)
        {
            _target = target;

            _rectTransform = _target.GetComponent<RectTransform>();
            _canvasGroup   = _target.GetComponent<CanvasGroup>();
        }

        public async Task<Sequence> PlayClip(ViewAnimationClip clip)
        {
            if (_target == null) return null;
            if (clip == null) return null;

            if (_rectTransform == null) _rectTransform = _target.GetComponent<RectTransform>();
            if (_canvasGroup == null) _canvasGroup     = _target.GetComponent<CanvasGroup>();

            if (_sequence != null && _sequence.IsPlaying())
            {
                _sequence.Pause();
            }

            _sequence = DOTween.Sequence();

            AddViewTweenToSequence(clip.PositionAnimations);
            AddViewTweenToSequence(clip.RotationAnimations);
            AddViewTweenToSequence(clip.ScaleAnimations);
            AddViewTweenToSequence(clip.AlphaAnimations);

            await _sequence.AsyncWaitForCompletion();

            return _sequence;
        }

        public void Pause()
        {
            _sequence.Pause();
        }

        private void AddViewTweenToSequence<T>(List<T> viewAnimations) where T : ViewAnimation
        {
            if (viewAnimations == null) return;
            if (viewAnimations.Count <= 0) return;

            foreach (var animation in viewAnimations)
            {
                if (animation.EndTime - animation.StartTime <= 0) continue;

                var duration = animation.EndTime - animation.StartTime;

                switch (animation)
                {
                    case PositionAnimation positionAnimation:
                    {
                        var tween = DOTween.To(
                            () => positionAnimation.StartPosition,
                            x => _rectTransform.anchoredPosition = x,
                            positionAnimation.EndPosition,
                            duration);
                        SetEase(tween, animation);
                        _sequence.Insert(animation.StartTime, tween);
                        break;
                    }
                    case RotationAnimation rotationAnimation:
                    {
                        var tween = DOTween.To(
                            () => rotationAnimation.StartAngle,
                            x => _rectTransform.localRotation = Quaternion.Euler(0, 0, x),
                            rotationAnimation.EndAngle,
                            duration);

                        SetEase(tween, animation);
                        _sequence.Insert(animation.StartTime, tween);
                        break;
                    }
                    case ScaleAnimation scaleAnimation:
                    {
                        var tween = DOTween.To(
                            () => scaleAnimation.StartScale,
                            x => _rectTransform.localScale = x,
                            scaleAnimation.EndScale,
                            duration);

                        SetEase(tween, animation);
                        _sequence.Insert(animation.StartTime, tween);
                        break;
                    }
                    case AlphaAnimation alphaAnimation:
                    {
                        var tween = DOTween.To(
                            () => alphaAnimation.StartAlpha,
                            x => _canvasGroup.alpha = x,
                            alphaAnimation.EndAlpha,
                            duration);

                        SetEase(tween, animation);
                        _sequence.Insert(animation.StartTime, tween);
                        break;
                    }
                }
            }
        }

        private void SetEase<T1, T2, TPluginType>(TweenerCore<T1, T2, TPluginType> tween, ViewAnimation viewAnimation)
            where TPluginType : struct, IPlugOptions
        {
            var isUseEase = viewAnimation.Ease != Ease.Unset;

            if (isUseEase)
            {
                tween.SetEase(viewAnimation.Ease);
            }
            else
            {
                tween.SetEase(viewAnimation.AnimationCurve);
            }
        }
    }
}