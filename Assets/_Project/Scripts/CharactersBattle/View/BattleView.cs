using UnityEngine;
using UnityEngine.UI;
using Model;

namespace View
{
    public class BattleView : MonoBehaviour
    {
        [SerializeField] private GameObject _floatingTextPrefab;

        private Image _healthbar;
        private Character _characterData;
        private int _characterMaxHealth;

        public void Initialize(Character data)
        {
            _healthbar = gameObject.GetComponent<Image>();
            _characterData = data;
            _healthbar.fillAmount = _characterData.Health;
            _characterMaxHealth = data.Health;

            data.CharacterAttacked += ShowFloatingText;
        }

        private void OnDisable()
        {
            _characterData.CharacterAttacked -= ShowFloatingText;
        }

        public void UpdateHealth(Character data)
        {
            int currentHealth = data.Health;
            _healthbar.fillAmount = (float)currentHealth / _characterMaxHealth;
        }

        public void ShowFloatingText()
        {
            var go = Instantiate(_floatingTextPrefab, transform.localPosition, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = _characterData.CurrentStats.CurrentDamage.ToString();
        }
    }
}