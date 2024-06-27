using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using Update = UnityEngine.PlayerLoop.Update;

public class GameManager1 : NetworkBehaviour
{
    public static GameManager1 Instance { get; private set; }
    public bool isLocalPlayerReady = false;
    public event EventHandler OnStatePlayerReady;
    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public Dictionary<ulong, bool> playersReadyDictionary;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private NetworkVariable<State> state = new NetworkVariable<State>(State.WaitingToStart);
    private float waitingToStartTimer = 3f;
    private float countdownToStart = 3f;
    private float gamePlayingTimer = 20f;
    private float gamePlayingTimerMax = 200f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        playersReadyDictionary = new Dictionary<ulong, bool>();
        
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameManager_OnPlayerInteract;

    }

    private void GameManager_OnPlayerInteract(object sender, EventArgs e)
    {
        if (state.Value == State.WaitingToStart)
        {
            isLocalPlayerReady = true;
            SetPlayerReadyServerRpc();
            OnStatePlayerReady?.Invoke(this, EventArgs.Empty);

            bool allPlayersReady = true;
            
            foreach (ulong client in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if (playersReadyDictionary.ContainsKey(client))
                {
                    if (playersReadyDictionary.TryGetValue(client, out bool isClientReady))
                    {
                        if (isClientReady = false)
                        {
                            allPlayersReady = false;
                        }
                    }
                    else
                    {
                        allPlayersReady = false;
                    }
                }
                else
                {
                    allPlayersReady = false;
                }
            }

            if (allPlayersReady)
            {
                state.Value = State.CountdownToStart;
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        state.Value = State.WaitingToStart;
        base.OnNetworkSpawn();
        state.OnValueChanged += OnStateChanged;
    }

    private void OnStateChanged(State previousvalue, State newvalue)
    {
        Debug.Log("State was changed:" + state.Value);
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsLocalPlayerReady()
    {
        return isLocalPlayerReady;
    }
    
    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused){
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);

        }
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        
        switch (state.Value)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    
                }
                break;
            case State.CountdownToStart:
                countdownToStart -= Time.deltaTime;
                if (countdownToStart < 0)
                {
                    state.Value = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                   
                    
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state.Value = State.GameOver;

                }
                break;
            case State.GameOver:
                
                break;
            
                
        }
        Debug.Log("State: " + state);
    }

    public bool IsGamePlaying()
    {
        return state.Value == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state.Value == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state.Value == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playersReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;
    }
}
