using UnityEngine;

namespace Model.SpecialEffects
{
    [CreateAssetMenu(fileName = "Stun", menuName = "Create New Special Effect/Stun")]
    public class Stun : SpecialEffect
    {
        public override CharacterStats Apply(Character target)
        {
            var newStats = target.CurrentStats;
            newStats.IsStunned = true;
            
            Initialize();
            TimerInstance.StartTimer(Duration);
            
            return newStats;   
        }

        public override CharacterStats Remove(Character target)
        {
            var newStats = target.CurrentStats;
            newStats.IsStunned = false;

            Dispose();
            
            return newStats;
        }
    }
    
}