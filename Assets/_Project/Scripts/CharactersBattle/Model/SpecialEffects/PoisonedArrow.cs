using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.SpecialEffects
{
    [CreateAssetMenu(fileName = "PoisonedArrow", menuName = "Create New Special Effect/Posioned Arrow")]
    public class PoisonedArrow : SpecialEffect
    {
        [field: SerializeField] public int PoisonDamage { get; private set; }
        [field: SerializeField] public float TickDuration { get; private set; }
        [field: SerializeField] public GameObject PoisonCoroutineHost { get; private set; }

        private GameObject _poisongGO;
        private Coroutine _poisonCoroutine;

        public override CharacterStats Apply(Character target)
        {
            if (_poisonCoroutine == null)
            {
                _poisongGO = Instantiate(PoisonCoroutineHost, PoisonCoroutineHost.transform.position,
                    Quaternion.identity);
                _poisonCoroutine = _poisongGO.GetComponent<MonoBehaviour>().StartCoroutine(DealPoisonDamage(target));
            }
            
            Initialize();
            TimerInstance.StartTimer(Duration);
            
            return target.CurrentStats;
        }

        public override CharacterStats Remove(Character target)
        {
            if (_poisonCoroutine != null)
            {
                _poisongGO.GetComponent<MonoBehaviour>().StopAllCoroutines();
                Destroy(_poisongGO, 0f);
                _poisonCoroutine = null;
            }

            Dispose();
            return target.CurrentStats;
        }

        public IEnumerator DealPoisonDamage(Character target)
        {
            float tickTimer = 0f;
            float durationTimer = 0f;

            var newStats = target.CurrentStats;

            while (durationTimer < Duration)
            {
                tickTimer += Time.deltaTime;
                durationTimer += Time.deltaTime;

                if (tickTimer >= TickDuration)
                {
                    target.CurrentStats.CurrentHealth -= PoisonDamage;
                    tickTimer = 0f;
                }

                yield return null;
            }
        }
    }
}