using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("MusicManager");
                instance = singletonObject.AddComponent<MusicManager>();
            }
            return instance;
        }
    }

    private AudioSource source;
    public AudioClip bgMusic;
    private string situation;

    // Bu metot sýnýfýn durumunu döndürür
    public string Situation
    {
        get { return situation; }
        set
        {
            if (situation != value)
            {
                situation = value;
                ChangeSituation(situation);
            }
        }
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        source = GetComponent<AudioSource>();
        Situation = "on";
    }

    public void ChangeSituation(string situation_)
    {
        if (situation_ == "on")
        {
            source.loop = true;
            source.clip = bgMusic;
            source.Play();
        }
        else if (situation_ == "off")
        {
            source.loop = false;
            source.Stop();
        }
    }
}
