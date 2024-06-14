using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx
{
    public class EventManager : MonoBehaviour
    {
        public enum SpinStates
        {
            NONE, INIT, PRESPIN, DURING, FINISH, AFTER, ONSPINSUCCESFULL, NEWSPINPREPARE
        }

        private SpinStates spinstate;

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

        public delegate void OnSkipLevelDelegate();
        public event OnSkipLevelDelegate OnSkipLevelEvent;

        public SpinStates GetState()
        {
            return spinstate;
        }

        public void SetStates(SpinStates state)
        {
            spinstate = state;
        }

        public void OnFirstInputIsPressed()
        {
            OnFirstInputEvent?.Invoke();
        }

        public void OnInitSpin()
        {
            if (GetState() != SpinStates.INIT)
            {
                OnInitSpinEvent?.Invoke();
                Debug.Log("OnInitSpin");
                SetStates(SpinStates.INIT);
            }

        }

        public void OnPreSpin()
        {
            if (GetState() != SpinStates.PRESPIN)
            {

                OnPreSpinEvent?.Invoke();
                Debug.Log("OnPreSpin");
                SetStates(SpinStates.PRESPIN);
            }
        }

        public void OnDuringSpin()
        {
            if (GetState() != SpinStates.DURING)
            {
                OnDuringSpinEvent?.Invoke();
                Debug.Log("OnDuringSpin");

                SetStates(SpinStates.DURING);
            }

        }

        public void OnFinishSpin()
        {
            if (GetState() != SpinStates.FINISH)
            {
                OnFinishSpinEvent?.Invoke();
                Debug.Log("OnFinishSpin");
                SetStates(SpinStates.FINISH);
            }

        }


        public void OnSpinIsSuccesful(bool isYes)
        {
            if (GetState() != SpinStates.ONSPINSUCCESFULL)
            {
                OnSpinIsSuccesfulEvent?.Invoke(isYes);
                Debug.Log("OnSpinIsSuccesful = " + isYes);
                SetStates(SpinStates.ONSPINSUCCESFULL);
            }

        }

        public void OnAfterSpin()
        {
            if (GetState() != SpinStates.AFTER)
            {
                OnAfterSpinEvent?.Invoke();
                Debug.Log("OnAfterSpin");
                SetStates(SpinStates.AFTER);
            }

        }

        public void OnNewSpinPrepare()
        {
            if (GetState() != SpinStates.NEWSPINPREPARE)
            {
                OnNewSpinPrepareEvent?.Invoke();
                Debug.Log("OnNewSpinPrepare");
                SetStates(SpinStates.NEWSPINPREPARE);
            }

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

        public void OnSkipLevel()
        {
            OnSkipLevelEvent?.Invoke();
        }

    }
}

