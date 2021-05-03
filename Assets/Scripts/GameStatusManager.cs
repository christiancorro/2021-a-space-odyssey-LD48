using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour {

    public enum GameState {
        Init,
        Started,
        Paused,
        Gameover
    }

    public static GameState currentState;

    void OnEnable() {
        Debug.Log("Init");
        currentState = GameState.Init;
    }

    private void Update() {
        if (GameStatusManager.isInit() && Input.GetAxis("Vertical") > 0) {
            GameStatusManager.StartGame();
        }
    }

    public static bool isGameover() {
        return currentState.Equals(GameState.Gameover);
    }

    public static bool isPaused() {
        return currentState.Equals(GameState.Paused);
    }

    public static bool isStarted() {
        return currentState.Equals(GameState.Started);
    }

    public static bool isInit() {
        return currentState.Equals(GameState.Init);
    }

    public static void Gameover() {
        currentState = GameState.Gameover;
        Debug.Log("Gameover");

    }
    public static void StartGame() {
        currentState = GameState.Started;
        Debug.Log("Game started");
    }


}
