using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitImageCanvasGroup;
    public CanvasGroup caughtImageCanvasGroup;

    bool playerAtExit = false;
    bool playerCaught = false;
    float timer = 0;

    public void CaughtPlayer()
    {
        playerCaught = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }

    void Update()
    {
        if (playerAtExit)
        {
            EndLevel(exitImageCanvasGroup, false);
        }
        else if (playerCaught)
        {
            EndLevel(caughtImageCanvasGroup, true);
        }
    }

    void EndLevel(CanvasGroup canvasGroup, bool restart)
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if (restart)
            {
                SceneManager.LoadScene("Level1");
            }
            else
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}
