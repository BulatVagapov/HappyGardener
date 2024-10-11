using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button policyButton;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    [SerializeField] private Image musicStateImage;

    [SerializeField] private GameObject policyScreen;

    private void Start()
    {
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        policyButton.onClick.AddListener(OnPolicyButtonClick);
        gameObject.SetActive(false);

        SetMusicButtonImage();

        musicButton.onClick.AddListener(OnMusicButtonClick);
    }

    private void SetMusicButtonImage()
    {
        if (AudioManager.Instance.AudioSettingsIndicator == 0)
        {
            musicStateImage.sprite = musicOnSprite;
        }
        else if (AudioManager.Instance.AudioSettingsIndicator == 1)
        {
            musicStateImage.sprite = musicOffSprite;
        }
    }

    private void OnMusicButtonClick()
    {
        AudioManager.Instance.ChangeAudioSettings();
        SetMusicButtonImage();
    }

    private void OnPolicyButtonClick()
    {
        policyScreen.SetActive(true);
    }

}
