using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR  // Direktiva kompajleru: Da li je igra pokrenuta iz Unity editora?
using UnityEditor;  // Ako jeste, uvrsti odgovarajuću biblioteku da bi se omogućilo pristup klasi EditorApplication
#endif  // Kraj Direktive kompajleru

public class GameEnding : MonoBehaviour
{
    public GameObject player;  // Referenca ka Johnovom objektu
    public CanvasGroup exitImage;  // Referenca ka slici koja se prikazuje kada se igra uspješno završi
    public CanvasGroup caughtImage;  // Referenca ka slici koja se prikazuje kada je John uhvaćen
    public float fadeDuration = 1f;  // Trajanje fade efekta u sekundama
    public float displayImageDuration = 1f;  // Vrijeme prikazivanja završne slike, nakon završetka fade efekta

    bool playerAtExit = false;  // Flag koji govori da li je igrač stigao do kraja nivoa
    bool playerCaught = false;  // Flag koji govori da li je igrač uhvaćen

    float timer = 0f;  // Tajmer za fade i kraj/restart nivoa

    public void CaughtPlayer()  // Javna metoda koja omogućava da se igrač proglasi uhvaćenim
    {
        playerCaught = true;
    }

    void Update()
    {
        if (playerAtExit)
        {
            EndLevel(exitImage, false);  // Ako je igrač stigao do kraja nivoa prikaži odgovarajuću sliku i završi igru
        }
        else if (playerCaught)
        {
            EndLevel(caughtImage, true);  // Ako je igrač uhvaćen prikaži odgovarajuću sliku i restartuj nivo
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player)  // Da li je objekat koji je prošao kroz collider kraja nivoa John?
        {
            playerAtExit = true;  // Postavi odgovarajući flag
        }
    }

    void EndLevel(CanvasGroup image, bool restart)
    {
        timer += Time.deltaTime;  // Uvećaj tajmer za vrijeme proteklo od prethodnog frejma
        image.alpha = timer / fadeDuration;  // Podesi providnost završne slike u skladu sa tajmerom

        if (timer >= fadeDuration + displayImageDuration) // Da li je slika prikazana u skladu sa zadatim trajanjima?
        {
            if (restart)
            {
                SceneManager.LoadScene(0);  // Restartuj scenu
            }
            else
            {
#if UNITY_EDITOR  // Direktiva kompajleru: Da li je igra pokrenuta iz Unity editora?
                EditorApplication.isPlaying = false;  // Vrati se u editor
#else  // Direktiva kompajleru: U suprotnom
                Application.Quit();  // Zatvori aplikaciju
#endif  // Kraj Direktive kompajleru
            }
        }
    }
}
