using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainTheme;

    void Start()
    {
        AudioManager.instance.PlayMusic(mainTheme, 2);
    }
}
