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
        // E�er ba�ka bir �rnek varsa, bu nesneyi yok et
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Singleton �rne�ini ayarla
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // AudioSource bile�enini al
            sound = this.GetComponent<AudioSource>();
        }
    }

    // �ste�e ba�l� di�er s�n�f �zellikleri ve metotlar buraya eklenebilir
    public void PlayButtonSound()
    {
        if (button != null)
        {
            sound.PlayOneShot(button);
        }
    }
}
