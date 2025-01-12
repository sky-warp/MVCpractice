using System;
using System.Collections.Generic;
using UnityEngine;
using ConfigScript;
using Model;
using UnityEngine.Serialization;
using View;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private CharacterContainer[] _characterConfigs;
        [SerializeField] private Transform _viewLeftParent;
        [SerializeField] private Transform _viewRightParent;
        [SerializeField] private GameObject _healthbarRightPrefab;
        [SerializeField] private GameObject _healthbarLeftPrefab;
        [SerializeField] private Transform _leftCharacterSpawn;
        [SerializeField] private Transform _rightCharacterSpawn;
        [SerializeField] private string resourcesFolder;

        private List<Character> _spawnedCharacters = new();
        private CharacterContainer[] _tempContainer;
        private GameObject _leftCharacterPrefab;
        private GameObject _rightCharacterPrefab;

        public List<Character> Characters { get; private set; }
        public BattleView LeftCharacterView { get; private set; }
        public BattleView RightCharacterView { get; private set; }

        public void CharacterSpawn()
        {
            int indexLeftCharacter = Random.Range(0, _characterConfigs.Length);
            int indexRightCharacter;
            do
            {
                indexRightCharacter = Random.Range(0, _characterConfigs.Length);
            } while (indexRightCharacter == indexLeftCharacter);

            Character leftCharacter = new Character(_characterConfigs[indexLeftCharacter]);
            Character rightCharacter = new Character(_characterConfigs[indexRightCharacter]);

            _spawnedCharacters.Add(leftCharacter);
            _spawnedCharacters.Add(rightCharacter);

            var viewItemLeft = Instantiate(_healthbarLeftPrefab, _viewLeftParent);
            viewItemLeft.GetComponent<BattleView>().Initialize(leftCharacter);
            var viewItemRight = Instantiate(_healthbarRightPrefab, _viewRightParent);
            viewItemRight.GetComponent<BattleView>().Initialize(rightCharacter);

            GameObject[] prefabs = Resources.LoadAll<GameObject>(resourcesFolder);

            _leftCharacterPrefab = prefabs[Random.Range(0, prefabs.Length - 1)];
            _rightCharacterPrefab = prefabs[Random.Range(0, prefabs.Length - 1)];

            Characters = _spawnedCharacters;

            Instantiate(_leftCharacterPrefab, _leftCharacterSpawn);
            Instantiate(_rightCharacterPrefab, _rightCharacterSpawn);
        }

        public void DestroyCharacters()
        {
            if (Characters != null)
            {
                Characters.Clear();
            }

            foreach (Transform child in _leftCharacterSpawn)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _rightCharacterSpawn)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _viewLeftParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in _viewRightParent)
            {
                Destroy(child.gameObject);
            }

            Characters = null;
            _leftCharacterPrefab = null;
            _rightCharacterPrefab = null;
        }
    }
}