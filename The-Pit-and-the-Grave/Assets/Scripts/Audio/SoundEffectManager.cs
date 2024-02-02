using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;
    private AudioSource sound;

    public AudioClip button;

    public static SoundEffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("SoundEffectManager");
                instance = singletonObject.AddComponent<SoundEffectManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Eðer baþka bir örnek varsa, bu nesneyi yok et
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Singleton örneðini ayarla
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // AudioSource bileþenini al
            sound = this.GetComponent<AudioSource>();
        }
    }

    // Ýsteðe baðlý diðer sýnýf özellikleri ve metotlar buraya eklenebilir
    public void PlayButtonSound()
    {
        if (button != null)
        {
            sound.PlayOneShot(button);
        }
    }
}
