using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Stop,
            Start,
            Pause,
            Win,
            Lose
        }
        public GameState gs;
    
        [HideInInspector]public GameObject player;
        [HideInInspector]public Transform playerT;
        [HideInInspector]public Rigidbody playerRb;
    
        private Vector3 _pos1, _pos2;
    
        [HideInInspector]public bool holding,upLerp;

        [HideInInspector]public GameObject newBrick;

        [HideInInspector]public float yPos;
    
        public List<GameObject> myBricks = new List<GameObject>();

        [Space(10)] public float moveSpeed;
        public float moveSens;

        [Space(10)] public float leftClamp;
        public float rightClamp;

        [HideInInspector] public GameObject finishLine;

        [HideInInspector]public int winPoints;
        [HideInInspector]public int winMultiplier;

        [HideInInspector]public bool onAir;

        public Transform trailObject;
        public TextMeshPro addPointText;

        public ParticleSystem smokeFX;
        
        private void Start()
        {
            playerRb = player.GetComponent<Rigidbody>();
            playerT = player.GetComponent<Transform>();

            yPos = playerT.position.y;
        }

    
        private void FixedUpdate()
        {
            switch (gs)
            {
                case GameState.Start:
                    playerT.position += playerT.forward * (moveSpeed * Time.deltaTime);
                    break;
                
                case GameState.Win:
                {
                    playerT.position += playerT.forward * (moveSpeed * Time.deltaTime);
                    moveSpeed -= 2f * Time.deltaTime;

                    if (moveSpeed <= 0)
                    {
                        moveSpeed = 0;
                        player.GetComponent<Animator>().SetTrigger(Win1);
                    }

                    break;
                }
                case GameState.Stop:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            var position = playerT.position;
        
            position = new Vector3(Mathf.Clamp(position.x, leftClamp, rightClamp), position.y, position.z);
            playerT.position = position;

            if (gs == GameState.Start)
            {
                if (yPos <= 1) yPos = 1;

                if (Input.GetMouseButtonDown(0))
                {
                    _pos1 = GetMousePosition();
                    holding = true;
                }

                if (Input.GetMouseButton(0) && holding)
                {
                    _pos2 = GetMousePosition();
                    var delta = _pos1 - _pos2;
                    _pos1 = _pos2;

                    Vector3 velocity;
                    velocity = new Vector3(Mathf.Lerp(playerRb.velocity.x, -delta.x * moveSens, 100f * Time.deltaTime), (velocity = playerRb.velocity).y, velocity.z);
                    playerRb.velocity = velocity;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    holding = false;
                }
            }
        }

        public void Win()
        {
            gs = GameState.Win;
            winPoints = myBricks.Count;
            trailObject.gameObject.GetComponent<TrailRenderer>().DOTime(1, 2).SetEase(Ease.OutExpo);
        }

        public void Lose()
        {
            gs = GameState.Lose;
            moveSpeed = 0;
            player.GetComponent<Animator>().SetTrigger(Lose1);
            trailObject.gameObject.GetComponent<TrailRenderer>().DOTime(1, 2).SetEase(Ease.OutExpo);
            smokeFX.Play();
        }

        public void NextLevelButton()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    
        public void PositionUpdate()
        {
            var position = playerT.position;
        
            position = new Vector3(position.x, yPos, position.z);
            playerT.position = position;
        }
        #region MyRegion
    
        public static GameManager Instance;
        private static readonly int Lose1 = Animator.StringToHash("Lose");
        private static readonly int Win1 = Animator.StringToHash("Win");

        private static Vector2 GetMousePosition()
        {
            var pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            return pos;
        }
    
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
    
        #endregion
    }
}
