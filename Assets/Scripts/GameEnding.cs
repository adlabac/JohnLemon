using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Ukoliko se igra pokreće iz editora treba uključiti UnityEditor namespace, zbog korištenja klase EditorApplication
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEnding : MonoBehaviour
{
    public GameObject player;                   // Referenca ka objektu igrača
    public float fadeDuration = 1f;             // Trajanje fade efekta u sekundama
    public float displayImageDuration = 1f;     // Trajanje prikaza slike nakon fade efekta u sekundama
    public CanvasGroup exitImageCanvasGroup;    // Canvas grupa slike uspješnog završetka
    public CanvasGroup caughtImageCanvasGroup;  // Canvas grupa slike kada je igrač uhvaćen

    bool playerAtExit = false;  // Da li je igrač na izlazu
    bool playerCaught = false;  // Da li je igrač uhvaćen
    float timer = 0;            // Ukupno proteklo vrijeme od kraja nivoa

    // Javna metoda kojom se signalizira da je igrač uhvaćen
    public void CaughtPlayer()
    {
        playerCaught = true;
    }

    // Igrač je došao do izlaza
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }

    // Ukoliko je igra završena, ili je igrač uhvaćen, prikazati odgovorajuću animaciju
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

    // Kraj nivoa
    void EndLevel(CanvasGroup canvasGroup, bool restart)
    {
        // Uvećavanje tajmer za vrijeme proteklo od prethodnog frejma
        timer += Time.deltaTime;

        // Podešavanje providnost canvasa u skladu sa proteklim vremenom
        canvasGroup.alpha = timer / fadeDuration;

        // Da li je došlo vrijeme za kraj nivoa?
        if (timer > fadeDuration + displayImageDuration)
        {
            // Treba li restartovati nivo?
            if (restart)
            {
                // Ponovo učitaj nivo "Level1"
                SceneManager.LoadScene("Level1");
            }
            else
            {
#if UNITY_EDITOR
                // Ako se igra pokreće iz samog editora, onda prekini izvršavanje
                EditorApplication.isPlaying = false;
#else
                // Ako se pokreće samostalna igra, onda zatvori aplikaciju
                Application.Quit();
#endif
            }
        }
    }
}
