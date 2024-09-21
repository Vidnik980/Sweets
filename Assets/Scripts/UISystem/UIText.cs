using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using Febucci.UI.Core;
using TMPro;
using UnityEngine;

namespace UIEnhance
{
    [RequireComponent(typeof(TextAnimator_TMP), typeof(TypewriterByCharacter))]
    public class UIText : MonoBehaviour, IUIElement
    {
        public float animationCharDelay = 0.035f;

        public bool waitSequenceForTextFinish;

        public float GetAnimationSequenceDuration() => waitSequenceForTextFinish
            ? _typewriter.TextAnimator.textFull.Length * animationCharDelay
            : 0f;

        private TextAnimator_TMP _animator;
        private TypewriterByCharacter _typewriter;

        private void OnValidate()
        {
            GetReferences();

            _typewriter.startTypewriterMode = TypewriterCore.StartTypewriterMode.FromScriptOnly;
            _typewriter.waitForNormalChars = animationCharDelay;
            _typewriter.waitLong = animationCharDelay;
            _typewriter.waitMiddle = animationCharDelay;
        }

        private void GetReferences()
        {
            if (!_animator)
                _animator = GetComponent<TextAnimator_TMP>();
            if (!_typewriter)
                _typewriter = GetComponent<TypewriterByCharacter>();
        }

        private void Start()
        {
            GetReferences();
        }

        private void OnEnable()
        {
            Prepare();
        }

        public void Prepare()
        {
            _animator.SetVisibilityEntireText(false, false);
        }

        public void Animate()
        {
            _typewriter.StartShowingText(true);
        }
    }

}