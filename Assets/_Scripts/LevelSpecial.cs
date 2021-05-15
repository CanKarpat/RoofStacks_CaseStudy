using UnityEngine;

namespace _Scripts
{
    public class LevelSpecial : MonoBehaviour
    {
        public GameObject finishLine;

        private void Start()
        {
            GameManager.Instance.finishLine = finishLine;
        }
    }
}
