using UnityEngine;
using UnityEngine.UI;
using Model;
using TMPro;

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
            _characterMaxHealth = data.Health;

            _characterData.CharacterAttacked += ShowFloatingText;
            _characterData.DamageTaken += UpdateHealth;
        }

        private void OnDisable()
        {
            _characterData.CharacterAttacked -= ShowFloatingText;
            _characterData.DamageTaken -= UpdateHealth;
        }

        public void UpdateHealth(int currentHealth)
        {
            _healthbar.fillAmount = (float)currentHealth / _characterMaxHealth;
        }

        public void ShowFloatingText()
        {
            Vector3 damageTextPosition = new Vector3(transform.position.x, transform.position.y - 80f, transform.position.z);
            
            var go = Instantiate(_floatingTextPrefab, damageTextPosition, Quaternion.identity, transform);
            go.GetComponent<TextMeshProUGUI>().text = _characterData.CurrentStats.CurrentDamage.ToString();
        }
    }
}