using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Model;

namespace View
{
    public class WinnerView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _winnerTextObject;

        private Character _characterData;
        private string _characterName;

        public bool IsButtonPressed { get; private set; }

        public event Action RestartGame;

        private void Awake()
        {
            _button.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Restart);
        }

        public void Initialize(Character data)
        {
            _characterName = data.CharacterName;
        }

        public GameObject ShowWinnerScreen(GameObject winScreen, Transform winScreenTransform)
        {
            gameObject.SetActive(true);
            _winnerTextObject.text = _characterName + " wins!";
            var restartGameInstance = Instantiate(winScreen, winScreenTransform);

            return restartGameInstance;
        }

        public void Restart()
        {
            IsButtonPressed = true;
            RestartGame?.Invoke();
            Destroy(gameObject);
        }
    }
}