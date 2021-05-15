using UnityEngine;

namespace _Scripts
{
    public class TimeManager : MonoBehaviour
    {
        private static TimeManager Instance;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }
}
