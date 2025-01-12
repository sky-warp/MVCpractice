using ScriptableEventListner;
using UnityEngine;

namespace Model.SpecialEffects
{
    public abstract class SpecialEffect : ScriptableObject
    {
        [field: SerializeField] public float EffectChance { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public SpecialEffectTimer TimerPrefab { get; private set; }
        public SpecialEffectTimer TimerInstance { get; private set; }
        public bool IsDurationOver { get; protected set; } = false;
        
        public abstract CharacterStats Apply(Character target);
        public abstract CharacterStats Remove(Character target);

        private void TimerListner()
        {
            IsDurationOver = true;
        }

        public void Initialize()
        {
            if (TimerInstance == null)
            {
                var timerObject = Instantiate(TimerPrefab, TimerPrefab.transform.position, Quaternion.identity);
                TimerInstance = timerObject.GetComponent<SpecialEffectTimer>();
            }

            TimerInstance.Completed += TimerListner;
        }

        public void Dispose()
        {
            if (TimerInstance != null)
            {
                TimerInstance.Completed -= TimerListner;
                Destroy(TimerInstance.gameObject);
                TimerInstance = null;
            }
        }
    }
}