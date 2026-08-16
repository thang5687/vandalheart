using UnityEngine;
public class SoundManager : MonoBehaviour
{
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #region attributes 
    #endregion
    #region components 
    public static SoundManager Instance;
    public AudioSource sound, music;
    public AudioClip[] soundclip, musicclip;
    #endregion
    #region functions 
    public void playSoundone(int clip)
    {
        sound.PlayOneShot(soundclip[clip]);
    }
    public void playMusic(int clip)
    {
        music.clip = musicclip[clip];
        music.Play();
    }
    #endregion
}
