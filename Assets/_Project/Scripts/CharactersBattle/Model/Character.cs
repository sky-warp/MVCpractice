using System;
using System.Collections.Generic;
using UnityEngine;
using ConfigScript;
using Model.SpecialEffects;
using UnityEngine.Serialization;
using View;

namespace Model
{
    [Serializable]
    public class Character : IBuffable
    {
        [field: SerializeField] public string CharacterName { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        [field: SerializeField] public SpecialEffect SpecialEffect { get; private set; }

        private bool _onSpecialEffect = false;

        public CharacterStats BaseStats { get; }
        public CharacterStats CurrentStats { get; private set; }
        public bool IsStunned = false;
        public bool IsAlive => Health > 0;
        public event Action CharacterAttacked;

        public Character(CharacterContainer characterConfig)
        {
            CharacterName = characterConfig.Value.CharacterName;
            Health = characterConfig.Value.Health;
            Damage = characterConfig.Value.Damage;
            SpecialEffect = characterConfig.Value.SpecialEffect;

            BaseStats = new CharacterStats
            {
                CurrentDamage = Damage,
                CurrentHealth = Health,
                IsStunned = this.IsStunned,
            };

            CurrentStats = BaseStats;
        }

        public void PerformAttack(Character enemy, BattleView enemyView)
        {
            if (CurrentStats.IsStunned)
                return;

            if (SpecialEffect.IsDurationOver)
            {
                RemoveSpecialEffect(enemy);
                _onSpecialEffect = false;
            }

            if (!_onSpecialEffect)
            {
                if (UnityEngine.Random.value < SpecialEffect.EffectChance)
                {
                    ApplySpecialEffect(enemy);
                    _onSpecialEffect = true;
                }
            }

            enemy.TakeDamage(Damage);
            CharacterAttacked?.Invoke();
            enemyView.UpdateHealth(enemy);
        }

        public void TakeDamage(int damage)
        {
            if (Health - damage >= 0)
                Health -= damage;
            else
                Health = 0;
        }

        public void ApplySpecialEffect(Character target)
        {
            target.CurrentStats = SpecialEffect.Apply(target);
        }

        public void RemoveSpecialEffect(Character target)
        {
            target.CurrentStats = SpecialEffect.Remove(target);
        }
    }
}