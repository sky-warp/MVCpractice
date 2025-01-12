using System.Collections.Generic;
using UnityEngine;
using ScriptableEventListner;

namespace ScriptableEvent
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent/Create GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListner> _listners = new List<GameEventListner>();

        public void Raise()
        {
            for (int i = _listners.Count - 1; i >= 0; i--)
            {
                _listners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListner listener)
        {
            _listners.Add(listener);
        }

        public void UnregisterListener(GameEventListner listener)
        {
            _listners.Remove(listener);
        }
    }
}