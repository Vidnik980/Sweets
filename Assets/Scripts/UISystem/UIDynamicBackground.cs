using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIEnhance
{
    public class UIDynamicBackground : MonoBehaviour
    {
        [SerializeField] private float _x, _y;

        private RawImage _rawImage;

        private void Start()
        {
            _rawImage = GetComponent<RawImage>();
        }

        private void Update()
        {
            if (_rawImage)
            {
                _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime,
                    _rawImage.uvRect.size);
            }
        }
    }
}
