using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEnding : MonoBehaviour
{
    public GameObject player;
    public CanvasGroup exitImage;
    public CanvasGroup caughtImage;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    bool playerAtExit = false;
    bool playerCaught = false;

    float timer = 0f;

    public void CaughtPlayer()
    {
        playerCaught = true;
    }

    void Update()
    {
        if (playerAtExit)
        {
            EndLevel(exitImage, false);
        }
        else if (playerCaught)
        {
            EndLevel(caughtImage, true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }

    void EndLevel(CanvasGroup image, bool restart)
    {
        timer += Time.deltaTime;
        image.alpha = timer / fadeDuration;

        if (timer >= fadeDuration + displayImageDuration)
        {
            if (restart)
            {
                SceneManager.LoadScene(0);
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
