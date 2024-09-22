using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;

    private void Awake()
    {
        // 씬 이동시 파괴되지 않게함
        DontDestroyOnLoad(this.gameObject);

        // AudioManager 객체 instance가 한개만 존재하도록함
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();

        bgm[bgmToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay, bool randomPitch = false)
    {
        if (sfxToPlay >= sfx.Length)
            return;

        if (randomPitch)
            sfx[sfxToPlay].pitch = Random.Range(0.9f, 1.1f);

        sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToStop) => sfx[sfxToStop].Stop();
}
