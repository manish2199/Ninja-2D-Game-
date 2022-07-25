using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Object = System.Object;

public class MultiplayerNinja : MonoBehaviour
{
    #region Serialized_Fields
    [SerializeField] private MultiplayerNinjaScpObj multiplayerNinjaScpObj;
    
    [SerializeField] private PhotonView photonView;

    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI playerNameText; 
    #endregion

    #region Public_Fields

    [HideInInspector] public ITimer _playerTimerHandler;

    #endregion
    
    #region Getters

    public PhotonView PhotonView
    {
        get
        {
            return photonView;
        }
    }
    
    #endregion
    
    #region Private_Variables
    private IMovement _playerMovementHandler;

    private IJUMP _playerJumpHandler;

    private IShoot _playerShootHandler;

    private CameraMove _cameraMove;

    private bool _canMove;

    private bool _canJump;

    private bool _canShoot;

    private const byte OnGameOverEvent = 0 ;

    private IEnumerator _gameOverRoutine;
    
    #endregion
   
    #region Initializers
    private void InitializeMovemenVariables()
    {
        _canMove = true;        _canJump = true;
        _playerMovementHandler = GetComponent<IMovement>();
        _playerJumpHandler = GetComponent<IJUMP>();
        _playerMovementHandler.SetMovementVariables(animator,multiplayerNinjaScpObj.speed);  
        _playerJumpHandler.SetJumpVariables(animator,multiplayerNinjaScpObj.jumpForce);
    }

    private void InitializeShootVariables()
    {
        _canShoot = true;
        _playerShootHandler = GetComponent<IShoot>();
        _playerShootHandler.SetShootVariables(photonView,animator);
    }
    
     private void SetCamera()
    {
        if (photonView.IsMine)
        {
            _cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
            _cameraMove.target = this.transform;
        }
    }

    private void InitializeTimerVariables()
    { 
       _playerTimerHandler = GetComponent<ITimer>();
       _playerTimerHandler.InitializeTimer(GamePlayUIHandler.Instance.TimerTextMeshPro,photonView);
       _playerTimerHandler.StartTimer();
    }

    private void InitializeGameOverRoutine()
    {
        _gameOverRoutine = GameOverCoroutine();
    }

    #endregion

    #region Unity_Functions

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkinngClient_EventReceived;
    }
    
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkinngClient_EventReceived;
    }


    private void Awake()
    { 
         SetCamera();
 
         InitializeMovemenVariables();
         
         InitializeShootVariables();
         
         InitializeTimerVariables();
         
         InitializeGameOverRoutine();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }

    private void FixedUpdate()
    {
        
        if ( _cameraMove && photonView.IsMine)
        {
            _cameraMove.cameraMovement();
        }
        if (_canJump)
        {
            _playerJumpHandler.HandleJump();  
        }
    }
    
    private void Update()
    {
        if (_canMove)
        {
            _playerMovementHandler.HandleMultiplayerMovement();
        }

        if (_canShoot)
        {
            _playerShootHandler.HandlePlayerShoot(_playerMovementHandler.IsFacingLeft);
        }
         _playerTimerHandler.TimerFunction(); 
    }
    #endregion

    #region Private_Funtions

    private void FreezePlayer()
    {
        _playerTimerHandler.StopTimer();
        _canJump = false;
        _canMove = false;
        _canShoot = false;
    }


    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(4.5f);
        
        GamePlayUIHandler.Instance.DeactivateGameOver();
        GamePlayUIHandler.Instance.OnClickExitRoom();
    }
    
    private void NetworkinngClient_EventReceived(EventData obj)
    {
        if (obj.Code == OnGameOverEvent)
        {
            // freeze the player
            FreezePlayer();
            MultiplayerGameplayManager.Instance.ShowGameWinner((string)obj.CustomData);
        }
    }
    

    #endregion
    
    #region Trigger_Functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WinnerDetector"))
        {
            if (photonView.IsMine)
            {
                // raise the event and also to all player
                string winnerNameText = PhotonNetwork.NickName;
                object obj =  winnerNameText;
                PhotonNetwork.RaiseEvent(OnGameOverEvent, obj,RaiseEventOptions.Default, SendOptions.SendUnreliable);
                
                
                FreezePlayer();
                MultiplayerGameplayManager.Instance.ShowGameWinner(photonView.Owner.NickName);
                StartCoroutine(_gameOverRoutine);
            }
        }
    }

    #endregion

}

