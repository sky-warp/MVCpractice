using UnityEngine;

namespace FloatingText
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float DestroyTime;

        private void Start()
        {
            Destroy(gameObject, DestroyTime);
        }
    }
}
