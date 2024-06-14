using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx
{
    public class EventManager : MonoBehaviour
    {
        public delegate void OnFirstInputDelegate();
        public event OnFirstInputDelegate OnFirstInputEvent;

        public delegate void OnInitSpinDelegate();
        public event OnInitSpinDelegate OnInitSpinEvent;

        public delegate void OnPreSpinDelegate();
        public event OnPreSpinDelegate OnPreSpinEvent;

        public delegate void OnDuringSpinDelegate();
        public event OnDuringSpinDelegate OnDuringSpinEvent;

        public delegate void OnFinishSpinDelegate();
        public event OnFinishSpinDelegate OnFinishSpinEvent;

        public delegate void OnAfterSpinDelegate();
        public event OnAfterSpinDelegate OnAfterSpinEvent;

        public delegate void OnSpinIsSuccesfulDelegate(bool isYes);
        public event OnSpinIsSuccesfulDelegate OnSpinIsSuccesfulEvent;

        public delegate void OnNewSpinPrepareDelegate();
        public event OnNewSpinPrepareDelegate OnNewSpinPrepareEvent;

        public delegate void OnContinueButtonPressedDelegate();
        public event OnContinueButtonPressedDelegate OnContinueButtonPressedEvent;

        public delegate void OnGiveUpButtonPressedDelegate();
        public event OnGiveUpButtonPressedDelegate OnGiveUpButtonPressedEvent;

        public delegate void OnBombIsExplosedDelegate();
        public event OnBombIsExplosedDelegate OnBombIsExplosedEvent;

        

        public void OnFirstInputIsPressed()
        {
            OnFirstInputEvent?.Invoke();
        }

        public void OnInitSpin()
        {
            OnInitSpinEvent?.Invoke();
            Debug.Log("OnInitSpin");
        }

        public void OnPreSpin()
        {
            OnPreSpinEvent?.Invoke();
            Debug.Log("OnPreSpin");
        }

        public void OnDuringSpin()
        {
            OnDuringSpinEvent?.Invoke();
            Debug.Log("OnDuringSpin");
        }

        public void OnFinishSpin()
        {
            OnFinishSpinEvent?.Invoke();
            Debug.Log("OnFinishSpin");
        }

      
        public void OnSpinIsSuccesful(bool isYes)
        {
            OnSpinIsSuccesfulEvent?.Invoke(isYes);
            Debug.Log("OnSpinIsSuccesful = " + isYes);
        }

        public void OnAfterSpin()
        {
            OnAfterSpinEvent?.Invoke();
            Debug.Log("OnAfterSpin");
        }

        public void OnNewSpinPrepare()
        {
            OnNewSpinPrepareEvent?.Invoke();
            Debug.Log("OnNewSpinPrepare");
        }

        public void OnContinueButtonPressed()
        {
            OnContinueButtonPressedEvent?.Invoke();
        }

        public void OnGiveUpButtonPressed()
        {
            OnGiveUpButtonPressedEvent?.Invoke();
        }

        public void OnBombIsExplosed()
        {
            OnBombIsExplosedEvent?.Invoke();
        }


    }
}

