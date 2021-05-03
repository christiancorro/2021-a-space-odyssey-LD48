using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GUIManager : MonoBehaviour {

    public Canvas pause, gameover, hud;
    public TMPro.TextMeshProUGUI score;
    public Starship starship;

    public AudioMixerSnapshot defaultSnapshot, pauseSnapshot, gameoverSnapshot;

    void Start() {
        Debug.Log(GameStatusManager.currentState);
    }

    void Update() {

        if (Input.GetButtonDown("Pause")) {
            if (Time.timeScale == 1) {
                if (!GameStatusManager.isGameover()) {
                    PauseGame();
                }
            } else {
                ResumeGame();
            }
        }


        if (GameStatusManager.isPaused() && Input.GetButtonDown("Exit")) {
            Application.Quit();
        }

        if (GameStatusManager.isPaused() && Input.GetButtonDown("Replay")) {
            ReplayGame();
        }

        if (GameStatusManager.isGameover()) {
            score.text = String.Format("{0:0.00}", Starship.distance / 1000) + " light years";
            gameover.gameObject.SetActive(true);
            CanvasGroup panel = gameover.GetComponentInChildren<CanvasGroup>();
            StartCoroutine(DoFade(panel, panel.alpha, 1, 1.3f));
            gameoverSnapshot.TransitionTo(2f);
            hud.gameObject.SetActive(false);
            if (Input.GetButtonDown("Exit")) {
                Application.Quit();
            } else if (Input.GetButtonDown("Replay")) {
                ReplayGame();
            }
        }

    }


    private void PauseGame() {
        pauseSnapshot.TransitionTo(1f);
        GameStatusManager.currentState = GameStatusManager.GameState.Paused;
        ShowPauseUI();
        Time.timeScale = 0;
    }

    private void ShowPauseUI() {
        pause.gameObject.SetActive(true);
        CanvasGroup panel = pause.GetComponentInChildren<CanvasGroup>();
        StartCoroutine(DoFade(panel, panel.alpha, 1, 0.2f));
        hud.gameObject.SetActive(false);
    }

    private void ResumeGame() {
        ClosePauseUI();
        GameStatusManager.currentState = GameStatusManager.GameState.Started;
        defaultSnapshot.TransitionTo(1f);
        Time.timeScale = 1;
    }

    private void ClosePauseUI() {
        CanvasGroup panel = pause.GetComponentInChildren<CanvasGroup>();
        StartCoroutine(DoFade(panel, panel.alpha, 0, 0.2f));
        //pause.gameObject.SetActive(false);
        hud.gameObject.SetActive(true);
    }

    private void ReplayGame() {
        StartCoroutine(CloseGameoverUI());
    }

    IEnumerator CloseGameoverUI() {
        CanvasGroup panel = gameover.GetComponentInChildren<CanvasGroup>();
        yield return DoFade(panel, panel.alpha, 0, 0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restart");
        ResumeGame();
        Time.timeScale = 1;
    }

    public IEnumerator DoFade(CanvasGroup panel, float start, float end, float duration) {
        float counter = 0;
        while (counter < duration) {
            counter += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }
}
