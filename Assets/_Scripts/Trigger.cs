using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class Trigger : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag("Obstacle"))
            {
                if (other.gameObject.CompareTag("MyBrick"))
                {
                    var gObj = other.gameObject;
                    gObj.transform.parent = null;
                    GameManager.Instance.myBricks.Remove(gObj);
                
                    GameManager.Instance.player.GetComponent<PlayerController>().OnAir();
                
                    GameManager.Instance.myBricks[GameManager.Instance.myBricks.Count - 1].GetComponent<BoxCollider>().enabled = true;
                
                    GameManager.Instance.yPos -= 1;
                
                    _camera.DOFieldOfView(_camera.fieldOfView - 1.5f, 1f);

                    var gObj2 = _camera.gameObject;
                    var localPosition = gObj2.transform.localPosition;
                    
                    localPosition = new Vector3(localPosition.x, localPosition.y + 0.5f, localPosition.z);
                    gObj2.transform.localPosition = localPosition;

                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    
                    //Trail object position change
                    var position = GameManager.Instance.trailObject.position;
                    position = new Vector3(position.x, position.y + 1, position.z);
                    GameManager.Instance.trailObject.position = position;
                }
            }

            if (gameObject.CompareTag("Water"))
            {
                if (other.gameObject.CompareTag("MyBrick"))
                {
                    GameManager.Instance.myBricks.Remove(other.gameObject);
                    Destroy(other.gameObject);
                
                    GameManager.Instance.player.GetComponent<PlayerController>().OnAir();

                    GameManager.Instance.myBricks[GameManager.Instance.myBricks.Count - 1].GetComponent<BoxCollider>().enabled = true;
                    GameManager.Instance.yPos -= 1;
                
                    _camera.DOFieldOfView(_camera.fieldOfView - 1, 1f);
                    
                    //Trail object position change
                    var position = GameManager.Instance.trailObject.position;
                    position = new Vector3(position.x, position.y + 1, position.z);
                    GameManager.Instance.trailObject.position = position;
                }
            }

            if (!other.gameObject.CompareTag("Player")) return;
            GameManager.Instance.Lose();
            UIManager.Lose();
        }
    }
}
