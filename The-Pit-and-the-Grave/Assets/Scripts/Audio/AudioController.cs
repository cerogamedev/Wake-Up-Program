using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioClip ilkSes;
    public AudioClip ikinciSes;
    public AudioClip startGameSound;

    private AudioSource sesKaynagi;
    public GameObject canva_;

    void Start()
    {
        sesKaynagi = this.GetComponent<AudioSource>();
        sesKaynagi.clip = ilkSes;
        sesKaynagi.Play();
        canva_.SetActive(false);
        Invoke("IkinciSesiCal", ilkSes.length); // Ýlk ses bittiðinde IkinciSesiCal fonksiyonunu çaðýr
    }

    void IkinciSesiCal()
    {

        if (sesKaynagi.isPlaying)
        {
            sesKaynagi.Stop(); // Eðer ilk ses hala çalýyorsa durdur
        }

        sesKaynagi.clip = ikinciSes;
        sesKaynagi.loop = true;
        sesKaynagi.Play();
        canva_.SetActive(true);
    }
    public void StartTheGame()
    {
        sesKaynagi.loop = false;
        sesKaynagi.clip = startGameSound;
        sesKaynagi.Play();
        canva_.SetActive(false);
        Invoke("LoadGameScene", 6.0f);
    }
    void LoadGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
