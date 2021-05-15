using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public bool turnLeftLerp,turnRightLerp;

        public float yRot;

        public Transform ground;

        public bool extraGravity;
        private Camera _camera;
        public Transform trailObject;
        private Animator _myAnimator;
        
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Start()
        {
            _camera = Camera.main;
            _myAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (turnLeftLerp)
            {
                turnLeftLerp = false;
                DOTween.To(() => yRot, x => yRot = x, yRot + 90f, 0.3f).SetEase(Ease.OutQuad)
                    .OnUpdate(RotationCheck)
                    .OnComplete(() =>
                    {
                        ground = null;
                        yRot = 0f;
                    });
            }
        
            if (turnRightLerp)
            {
                turnRightLerp = false;
                DOTween.To(() => yRot, x => yRot = x, yRot - 90f, 0.3f).SetEase(Ease.OutQuad)
                    .OnUpdate(RotationCheck)
                    .OnComplete(() => 
                    { 
                        ground = null;
                        yRot = 0f;
                    });
            }

            switch (extraGravity)
            {
                case true:
                {
                    var mass = GameManager.Instance.playerRb.mass;
                    GameManager.Instance.playerRb.AddForce(Physics.gravity * (mass * mass));
                    break;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Brick"))
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                foreach (var mr in other.gameObject.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }
                StartCoroutine(StartAdding(other.gameObject));
            }

            if (other.gameObject.CompareTag("CurveLeftGround"))
            {
                ground = other.gameObject.transform;
                yRot = ground.localEulerAngles.y;
                turnLeftLerp = true;
            }
        
            if (other.gameObject.CompareTag("CurveRightGround"))
            {
                ground = other.gameObject.transform;
                yRot = ground.localEulerAngles.y;
                turnRightLerp = true;
            }

            if (!other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("CurveRightGround") && !other.gameObject.CompareTag("CurveLeftGround")) return;
            extraGravity = false;
            GameManager.Instance.onAir = false;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Finish")) return;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            GameManager.Instance.Win();
        }

        private void RotationCheck()
        {
            var localEulerAngles = ground.localEulerAngles;
            localEulerAngles = new Vector3(localEulerAngles.x, yRot, localEulerAngles.z);
            ground.localEulerAngles = localEulerAngles;
        }

        public void OnAir()
        {
            GameManager.Instance.playerRb.useGravity = true;
            GameManager.Instance.onAir = true;
            extraGravity = true;
        }

        private void AddBrick()
        {
            OnAir();
            
            _camera.DOShakeRotation(0.2f, 0.7f);
            
            _myAnimator.SetTrigger(Jump);
            
            //List 
            var newOne = Instantiate(GameManager.Instance.newBrick, GameManager.Instance.myBricks[GameManager.Instance.myBricks.Count -1].transform.GetChild(0).position, Quaternion.identity, gameObject.transform);
            GameManager.Instance.myBricks.Add(newOne);
            
            //Trail object position change
            var position = GameManager.Instance.trailObject.position;
            position = new Vector3(position.x, position.y - 1, position.z);
            GameManager.Instance.trailObject.position = position;

            //Smooth position change bool
            GameManager.Instance.yPos += 1;
            GameManager.Instance.PositionUpdate();

            //For collider overlap.
            GameManager.Instance.myBricks[GameManager.Instance.myBricks.Count - 2].GetComponent<BoxCollider>().enabled = false;

            //Camera Zoom Out
            _camera.DOFieldOfView(_camera.fieldOfView + 1.5f, 1f);
            
            var gObj = _camera.gameObject;
            var localPosition = gObj.transform.localPosition;
            
            localPosition = new Vector3(localPosition.x, localPosition.y - 0.5f, localPosition.z);
            gObj.transform.localPosition = localPosition;
        }

        
        private IEnumerator StartAdding(GameObject other)
        {
            var myBricksScript = other.GetComponent<MyBricksScript>();
            GameManager.Instance.addPointText.text = "+"+myBricksScript.howMuchBricksIHave.ToString();
            DOTween.PlayForward("AddPoint");
            for (var i = 0; i < myBricksScript.howMuchBricksIHave; i++)
            {
                AddBrick();
                
                yield return new WaitForSeconds(0.31f);
            }
        }
    }
}
