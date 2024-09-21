using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIEnhance
{
    public class UIButton : UIContainer, IPointerDownHandler
    {
        [Space(20), Header("Button settings")] public float animationButtonDuration = .2f;

        public Vector3 targetValue = Vector3.one / 10f;
        public int vibrato = 10;
        public float elasticityOrRandomness = 1;

        [Space] public UIManager.UIButtonClickAnimation overrideButtonAnimation;
        public Ease overrideButtonEase;

        private Tween _tween;
        private RectTransform _rect;
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _defaultPosition = _rect.anchoredPosition;
            _defaultRotation = transform.rotation;
            _defaultScale = transform.localScale;
        }

        private void OnEnable()
        {
            _tween.Kill();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (UIManager.instance.buttonSource)
                UIManager.instance.buttonSource.Play();
            UIManager.UIButtonClickAnimation animation = UIManager.instance.defaultButtonClickAnimation;
            Ease ease = UIManager.instance.defaultButtonClickEase;
            _tween.Kill(true);

            if (overrideButtonAnimation != UIManager.UIButtonClickAnimation.None)
            {
                animation = overrideButtonAnimation;
            }

            if (overrideButtonEase != Ease.Unset)
            {
                ease = overrideButtonEase;
            }

            switch (animation)
            {
                case UIManager.UIButtonClickAnimation.PunchPosition:
                    _tween = _rect.DOPunchAnchorPos(targetValue, animationButtonDuration, vibrato,
                        elasticityOrRandomness);
                    break;
                case UIManager.UIButtonClickAnimation.PunchRotation:
                    _tween = _rect.DOPunchRotation(targetValue, animationButtonDuration, vibrato,
                        elasticityOrRandomness);
                    break;
                case UIManager.UIButtonClickAnimation.PunchScale:
                    _tween = _rect.DOPunchScale(targetValue, animationButtonDuration, vibrato, elasticityOrRandomness);
                    break;
                case UIManager.UIButtonClickAnimation.PunchRotationAndScale:
                    _tween = _rect.DOPunchRotation(targetValue, animationButtonDuration, vibrato,
                        elasticityOrRandomness);
                    _tween = _rect.DOPunchScale(targetValue, animationButtonDuration, vibrato, elasticityOrRandomness);
                    break;

                case UIManager.UIButtonClickAnimation.ShakePosition:
                    _tween = _rect.DOShakeAnchorPos(animationButtonDuration, targetValue, vibrato,
                        elasticityOrRandomness);
                    break;
                case UIManager.UIButtonClickAnimation.ShakeRotation:
                    _tween = _rect.DOShakeRotation(animationButtonDuration, targetValue, vibrato,
                        elasticityOrRandomness);
                    break;
                case UIManager.UIButtonClickAnimation.ShakeScale:
                    _tween = _rect.DOShakeScale(animationButtonDuration, targetValue, vibrato, elasticityOrRandomness);
                    break;
            }

            _tween.OnComplete(() => { ResetTransform(ease); }).SetEase(ease);
        }

        private void ResetTransform(Ease ease)
        {
            _rect.DOAnchorPos(_defaultPosition, animationButtonDuration / 2f).SetEase(ease);
            transform.DORotateQuaternion(_defaultRotation, animationButtonDuration / 2f).SetEase(ease);
            transform.DOScale(_defaultScale, animationButtonDuration / 2f).SetEase(ease);
        }
    }
}