using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LevelManager : MonoBehaviour {

    public int playerIntegrity;
    public int playerttl;

    public TextMeshPro ttlText;
    public TextMeshPro integrityText;

    public int distance;

    public GameObject electricHitIndicator;
    public GameObject dataHitIndicator;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverCause;
    public GameObject winScreen;

    public static LevelManager instance;

    public GameObject startGameScreen;
    public Transform startObj;
    public Transform exitObj;

    public GameObject[] shields;
    public GameObject[] controllers;

    public bool gameStarted = false;
    public bool gameEnded = false;

    Vector3 startObjPos;
    Vector3 exitObjPos;

    private void Awake() {
        if (instance == null) {
            instance = this;
            foreach (GameObject shield in shields) {
                shield.SetActive(false);
            }
            gameStarted = false;
            gameEnded = false;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        startObjPos = startObj.position;
        exitObjPos = exitObj.position;
    }

    private void Update() {
        if (!gameStarted) {
            if ((startObj.position - startObjPos).sqrMagnitude > Mathf.Epsilon) {
                StartGame();
            } else if ((exitObj.position - exitObjPos).sqrMagnitude > Mathf.Epsilon) {
                Application.Quit();
            }
        }
    }

    public void RouterSelected(int newDistance) {
        playerttl--;
        ttlText.text = string.Format("TTL:\n{0}", playerttl);
        if (playerttl <= 0) {
            GameOver();
        }
        distance = newDistance;
    }

    public void PlayerDataHit(int damage) {
        playerIntegrity -= damage;
        if (playerIntegrity < 0) {
            playerIntegrity = 0;
        }
        integrityText.text = string.Format("Integrity:\n{0}%", playerIntegrity);

        if (playerIntegrity <= 0) {
            GameOver();
        } else {
            StartCoroutine(HitFlash(dataHitIndicator));
        }
    }

    public void PlayerElectricHit(int damage) {
        playerIntegrity -= damage;
        if (playerIntegrity < 0) {
            playerIntegrity = 0;
        }
        integrityText.text = string.Format("Integrity:\n{0}%", playerIntegrity);

        if (playerIntegrity <= 0) {
            GameOver();
        } else {
            StartCoroutine(HitFlash(electricHitIndicator));
        }
    }

    void StartGame() {
        startGameScreen.SetActive(false);
        startObj.gameObject.SetActive(false);
        foreach (GameObject shield in shields) {
            shield.SetActive(true);
        }
        foreach (GameObject controller in controllers) {
            controller.SetActive(false);
        }
        gameStarted = true;
    }

    public void WinGame() {
        foreach (GameObject shield in shields) {
            shield.SetActive(false);
        }
        foreach (GameObject controller in controllers) {
            controller.SetActive(true);
        }
        gameEnded = true;
        winScreen.SetActive(true);
        StartCoroutine(Restart(6f));
    }

    void GameOver() {
        if (gameEnded) {
            return;
        }
        gameOverScreen.SetActive(true);
        if (playerIntegrity <= 0) {
            gameOverCause.text = "MALFORMED";
        } else if (playerttl <= 0) {
            gameOverCause.text = "TIMED OUT";
        }
        gameEnded = true;
        StartCoroutine(Restart(4f));
    }

    IEnumerator HitFlash(GameObject go) {
        go.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        go.SetActive(false);
    }

    IEnumerator Restart(float delay) {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
