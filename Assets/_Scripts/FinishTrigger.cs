using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class FinishTrigger : MonoBehaviour
    {
        public string multiplier;
        private GameObject _myParent;
        private Camera _camera;

        private void Start()
        {
            _myParent = gameObject.transform.parent.gameObject;
            _camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("MyBrick") && !other.gameObject.CompareTag("Player")) return;
            
            _camera.DOShakeRotation(0.2f, 0.7f);
            _myParent.GetComponent<DOTweenAnimation>().DOPlay();
            DOTween.Play("SliderClose");
            
            if (multiplier != "x20")
            {
                if (GameManager.Instance.myBricks.Count != 1)
                {
                    var gObj = other.gameObject;
                    gObj.transform.parent = null;
                    GameManager.Instance.myBricks.Remove(gObj);
                    GameManager.Instance.myBricks[GameManager.Instance.myBricks.Count - 1].GetComponent<BoxCollider>().enabled = true;
                    GameManager.Instance.yPos -= 1;
                }
            }

            switch (multiplier)
            {
                case "x1":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 1;
                    break;
                case "x2":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 2;
                    break;
                case "x3":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 3;
                    break;
                case "x4":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 4;
                    break;
                case "x5":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 5;
                    break;
                case "x6":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 6;
                    break;
                case "x7":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 7;
                    break;
                case "x8":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 8;
                    break;
                case "x9":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 9;
                    break;
                case "x10":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 10;
                    break;
                case "x20":
                    UIManager.Instance.multiplierText.text = multiplier;
                    DOTween.Restart("Text");
                    GameManager.Instance.winMultiplier = 20;
                    break;
            }
        }
    }
}
