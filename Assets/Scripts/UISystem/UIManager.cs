using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace UIEnhance
{
    public class UIManager : MonoBehaviour
    {
        #region Vars

        public static UIManager instance;

        public AudioSource buttonSource;

        public UIPanel mainPanel;
        
        [Header("Panels settings")]
        public UIPanelAnimation defaultPanelAnimation;
        public Ease defaultPanelEase;

        [Header("Elements settings")] 
        public UIElementAnimation defaultElementAnimation;
        public Ease defaultElementEase;

        [Header("Buttons settings")] 
        public UIButtonClickAnimation defaultButtonClickAnimation;
        public Ease defaultButtonClickEase;

        public bool AnimationIsPlaying { get; private set; }

        private List<UIPanel> _panels = new List<UIPanel>();

        #endregion

        #region Initialization

        private void Awake()
        {
            instance = this;

            StartWork();
        }

        private void StartWork()
        {
            _panels = FindObjectsOfType<UIPanel>(true).ToList();

            _panels.ForEach(panel => { panel.gameObject.SetActive(false); });

            mainPanel.gameObject.SetActive(true);
            mainPanel.Open(null, UIPanelAnimation.None, Ease.Unset);

        }

        #endregion

        #region Logic

        public void OpenPanelOverlay(string panelName)
        {
            UIPanel panel = _panels.Find(p => p.panelName == panelName);
            if (panel != null)
            {
                panel.gameObject.SetActive(true);
                panel.Open(null, defaultPanelAnimation, defaultPanelEase);
            }
        }
        
        public void OpenPanel(string panelName)
        {
            if (AnimationIsPlaying)
            {
                return;
            }

            _panels.ForEach((uiPanel =>
            {
                if (uiPanel.panelName != panelName)
                {
                    uiPanel.Close(() => { uiPanel.gameObject.SetActive(false); }, defaultPanelAnimation, defaultPanelEase);
                }
            }));

            UIPanel panel = _panels.Find(p => p.panelName == panelName);
            if (panel != null)
            {
                AnimationIsPlaying = true;
                panel.gameObject.SetActive(true);
                panel.Open(() => { AnimationIsPlaying = false; }, defaultPanelAnimation, defaultPanelEase);
            }
        }

        public void ClosePanel(string panelName)
        {
            if (AnimationIsPlaying)
            {
                return;
            }

            UIPanel panel = _panels.Find(p => p.panelName == panelName);
            if (panel != null)
            {
                AnimationIsPlaying = true;
                panel.Close(() => { AnimationIsPlaying = false; }, defaultPanelAnimation, defaultPanelEase);
            }
        }

        public void OpenPanel(string panelName, Action callback)
        {
            if (AnimationIsPlaying)
            {
                return;
            }

            _panels.ForEach((uiPanel =>
            {
                if (uiPanel.panelName != panelName)
                {
                    uiPanel.Close(() => { uiPanel.gameObject.SetActive(false); }, defaultPanelAnimation, defaultPanelEase);
                }
            }));

            UIPanel panel = _panels.Find(p => p.panelName == panelName);
            if (panel != null)
            {
                AnimationIsPlaying = true;
                panel.gameObject.SetActive(true);
                panel.Open(() =>
                {
                    AnimationIsPlaying = false;
                    callback?.Invoke();
                }, defaultPanelAnimation, defaultPanelEase);
            }
        }

        public void ClosePanel(string panelName, Action callback)
        {
            if (AnimationIsPlaying)
            {
                return;
            }

            UIPanel panel = _panels.Find(p => p.panelName == panelName);
            if (panel != null)
            {
                AnimationIsPlaying = true;
                panel.Close(() =>
                {
                    AnimationIsPlaying = false;
                    callback?.Invoke();
                }, defaultPanelAnimation, defaultPanelEase);
            }
        }

        #endregion

        #region Structures
        public enum UIPanelAnimation
        {
            None,
            Fade,
            Swap,
            SwapAndFade,
            Slide,
            SlideAndFade,
            ScaleAndFade,
            RotateAtCenterAndFade,
            RotateAtTopLeftCornerAndFade,
            RotateAtTopRightCornerAndFade,
        }
        public enum UIElementAnimation
        {
            None,
            Fade,
            SlideFromLeft,
            SlideFromRight,
            ScaleAndFade,
        }
        
        public enum UIButtonClickAnimation
        {
            None,
            PunchPosition,
            PunchRotation,
            PunchScale,
            PunchRotationAndScale,
            
            ShakePosition,
            ShakeRotation,
            ShakeScale,
        }

        #endregion
        
        public void URL(string url)
        {
            Application.OpenURL(url);
        }
    }
}