using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public CanvasGroup exitImage;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    bool playerAtExit = false;
    float timer = 0f;

    void Update()
    {
        if (playerAtExit)
        {
            EndLevel();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }

    void EndLevel()
    {
        timer += Time.deltaTime;
        exitImage.alpha = timer / fadeDuration;

        if (timer >= fadeDuration + displayImageDuration)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
