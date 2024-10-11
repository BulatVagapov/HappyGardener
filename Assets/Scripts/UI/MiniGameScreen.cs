using TMPro;
using UnityEngine;

public class MiniGameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private MiniGameManager miniGameManager;
    
    void Start()
    {
        miniGameManager.MiniGameTimer.CurrentTimeIsChanged += SetTimerText;
        gameObject.SetActive(false);
    }

    private void SetTimerText(int time)
    {
        timerText.text = (time / 60) + ":" + ((time % 60) < 10 ? "0" + (time % 60) : (time % 60));
    }
}
