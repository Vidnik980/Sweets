using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIEnhance
{
    public interface IUIElement
    {
        public void Prepare();
        public void Animate();

        public float GetAnimationSequenceDuration();
    }
}
