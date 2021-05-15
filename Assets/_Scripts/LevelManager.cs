using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public List<GameObject> level = new List<GameObject>();

        public void Awake()
        {
            Instantiate(level[Random.Range(0, level.Count)]);
        }
    }
}
