using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class DistanceChecker : MonoBehaviour
    {
        private void Update()
        {
            transform.DOLookAt(GameManager.Instance.finishLine.transform.position, 0.001f);

            if (Physics.Raycast(transform.position,transform.forward, out var hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Finish"))
                {
                    if (UIManager.Instance.distanceCheck.maxValue == 1)
                        UIManager.Instance.distanceCheck.maxValue = hit.distance;
                
                    UIManager.Instance.distanceCheck.value = hit.distance;
                }
            }

            var position = GameManager.Instance.finishLine.transform.position;
            position = new Vector3(position.x, transform.position.y, position.z);
            GameManager.Instance.finishLine.transform.position = position;
        }
    }
}
