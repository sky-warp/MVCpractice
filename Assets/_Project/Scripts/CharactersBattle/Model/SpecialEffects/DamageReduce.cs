using UnityEngine;

namespace Model.SpecialEffects
{
    [CreateAssetMenu(fileName = "DamageReduce", menuName = "Create New Special Effect/Damage Reduce")]
    public class DamageReduce : SpecialEffect
    {
        [field: SerializeField, Range(1, 100)] public int DamageReduction { get; private set; }

        public override CharacterStats Apply(Character target)
        {
            var newStats = target.CurrentStats;
            int reduceAmount = newStats.CurrentDamage * DamageReduction / 100;
            newStats.CurrentDamage -= reduceAmount;

            Initialize();
            TimerInstance.StartTimer(Duration);
            Debug.Log($"Damage after debuff: {newStats.CurrentDamage}");

            return newStats;
        }

        public override CharacterStats Remove(Character target)
        {
            var newStats = target.CurrentStats;
            newStats.CurrentDamage = target.Damage;

            Dispose();
            return newStats;
        }
    }
}