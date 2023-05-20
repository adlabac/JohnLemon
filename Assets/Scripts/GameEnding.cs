using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitImageCanvasGroup;

    bool playerAtExit = false;
    float timer = 0;

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
            EndLevel();
        }
    }

    void EndLevel()
    {
        timer += Time.deltaTime;
        exitImageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
