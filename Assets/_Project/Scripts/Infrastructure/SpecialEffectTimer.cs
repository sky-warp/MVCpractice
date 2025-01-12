using System;
using System.Timers;
using Model.SpecialEffects;
using UnityEngine;

namespace ScriptableEventListner
{
    public class SpecialEffectTimer : MonoBehaviour
    {
        
        private float _timer;
        private float _duration;
        private bool _isEnabled;
        
        public event Action Completed;

        public void StartTimer(float duration)
        {
            _timer = 0f;
            _duration = duration;
            _isEnabled = true;
        }

        private void Update()
        {
            if(!_isEnabled)
                return;
            
            _timer += Time.deltaTime;

            if (_timer >= _duration)
            {
                _isEnabled = false;
                Completed?.Invoke();
            }
        }
    }
}