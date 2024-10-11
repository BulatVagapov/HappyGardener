using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    private const string AudioSettingsSaveKey = "AudioSettings";
    private int audioSettingsIndicator;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainGameMusic;
    [SerializeField] private AudioClip miniGameMusic;

    public int AudioSettingsIndicator => audioSettingsIndicator;

    protected override void Awake()
    {
        base.Awake();
        audioSettingsIndicator = PlayerPrefs.GetInt(AudioSettingsSaveKey, 0);
        audioSource.Stop();
    }

    public void SetStartState()
    {
        audioSource.clip = mainGameMusic;

        if (audioSettingsIndicator == 1) return;

        audioSource.Play();
    }

    public void ChangeMusicToMiniGame()
    {
        if (audioSettingsIndicator == 1) return;
        
        audioSource.clip = miniGameMusic;
        audioSource.Play();
    }

    public void ChangeMusicToMainGame()
    {
        if (audioSettingsIndicator == 1) return;

        audioSource.clip = mainGameMusic;
        audioSource.Play();
    }

    public void ChangeAudioSettings()
    {
        if(audioSettingsIndicator == 0)
        {
            audioSource.Stop();
            audioSettingsIndicator = 1;
            PlayerPrefs.SetInt(AudioSettingsSaveKey, audioSettingsIndicator);
        }
        else if(audioSettingsIndicator == 1)
        {
            audioSource.Play();
            audioSettingsIndicator = 0;
            PlayerPrefs.SetInt(AudioSettingsSaveKey, audioSettingsIndicator);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
