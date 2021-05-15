using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class UIManager : MonoBehaviour
    {
        public Slider distanceCheck;
        public TextMeshProUGUI multiplierText;

        public TextMeshProUGUI winPoint;
    
        public static UIManager Instance;
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
        

        public void Go()
        {
            GameManager.Instance.gs = GameManager.GameState.Start;
        }
    
        public void Win()
        {
            DOTween.Play("Win");
            GameManager.Instance.winPoints *= GameManager.Instance.winMultiplier;
            winPoint.text = GameManager.Instance.winPoints.ToString();
        }

        public static void Lose()
        {
            DOTween.Play("Lose");
        }
    }
}
