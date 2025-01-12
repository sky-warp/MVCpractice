using System.Collections;
using Model;
using View;
using Spawner;
using UnityEngine;

namespace Controller
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private CharacterSpawner _spawner;
        [SerializeField] private Transform _winnerScreenParent;
        [SerializeField] private GameObject _winnerScreenPrefab;

        private WinnerView _winnerView;
        private GameObject _restartGameInstance;

        private void Awake()
        {
            StartGame();
            _winnerView = _winnerScreenPrefab.GetComponent<WinnerView>();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void StartGame()
        {
            _spawner.DestroyCharacters();
            _spawner.CharacterSpawn();

            var leftCharacter = _spawner.Characters[0];
            var rightCharacter = _spawner.Characters[1];

            StartCoroutine(Battle(leftCharacter, rightCharacter, _spawner.RightCharacterView,
                _spawner.LeftCharacterView));
        }

        private IEnumerator Battle(Character leftCharacter, Character rightCharacter, BattleView rightCharacterView,
            BattleView leftCharacterView)
        {
            while (leftCharacter.IsAlive && rightCharacter.IsAlive)
            {
                leftCharacter.PerformAttack(rightCharacter, rightCharacterView);
                rightCharacter.PerformAttack(leftCharacter, leftCharacterView);

                yield return new WaitForSeconds(1f);

                if (!leftCharacter.IsAlive || !rightCharacter.IsAlive)
                {
                    var winner = (!leftCharacter.IsAlive) ? rightCharacter :
                        (!rightCharacter.IsAlive) ? leftCharacter : null;

                    if (winner == null)
                        Debug.Log("Winner is null");
                    else
                    {
                        _winnerView.Initialize(winner);
                        _restartGameInstance = _winnerView.ShowWinnerScreen(_winnerScreenPrefab, _winnerScreenParent);

                        var eventListner = _restartGameInstance.GetComponent<WinnerView>();
                        eventListner.RestartGame += StartGame;

                        yield return new WaitUntil(() => eventListner.IsButtonPressed);

                        eventListner.RestartGame -= StartGame;
                    }

                    yield break;
                }
            }
        }
    }
}