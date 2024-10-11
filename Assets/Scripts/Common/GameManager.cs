using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MiniGameManager miniGameManager;

    [SerializeField] private StartMiniGameScreen startMiniGamePanel;
    [SerializeField] private EndMiniGameScreen endMiniGamePanel;
    [SerializeField] private MiniGameRulesScreen miniGameRulesScreen;

    [SerializeField] private Button firstPlayButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject[] miniGameObjects;

    private ConsumablesType currentMiniGameType;



    private void Start()
    {
        miniGameRulesScreen.gameObject.SetActive(false);
        startMiniGamePanel.gameObject.SetActive(false);
        endMiniGamePanel.gameObject.SetActive(false);
        Gardener.Instance.NeedMoreWaterEvent += OnNeedMoreWater;
        Gardener.Instance.NeedMoreFertilizerEvent += OnNeedMoreFertilizer;
        firstPlayButton.onClick.AddListener(OnFirstPlayButtonClick);
        playButton.onClick.AddListener(StartMinGame);
        backButton.onClick.AddListener(ReturnToGame);
        miniGameManager.EndOfMiniGameEvent += EndOfMiniGame;
        closeButton.onClick.AddListener(() => startMiniGamePanel.gameObject.SetActive(false));
    }

    private void OnNeedMoreWater()
    {
        currentMiniGameType = ConsumablesType.Water;
        startMiniGamePanel.SetStartMiniGameText(currentMiniGameType);
        startMiniGamePanel.gameObject.SetActive(true);
    }

    private void OnNeedMoreFertilizer()
    {
        currentMiniGameType = ConsumablesType.Fertilizer;
        startMiniGamePanel.SetStartMiniGameText(currentMiniGameType);
        startMiniGamePanel.gameObject.SetActive(true);
    }

    private void OnFirstPlayButtonClick()
    {
        miniGameRulesScreen.SetMiniGameRulesScreen(currentMiniGameType);
        startMiniGamePanel.gameObject.SetActive(false);
        miniGameRulesScreen.gameObject.SetActive(true);
    }

    private void StartMinGame()
    {
        miniGameRulesScreen.gameObject.SetActive(false);
        endMiniGamePanel.gameObject.SetActive(false);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }

        for (int i = 0; i < miniGameObjects.Length; i++)
        {
            miniGameObjects[i].SetActive(true);
        }
        miniGameManager.StartMiniGame(currentMiniGameType);

        AudioManager.Instance.ChangeMusicToMiniGame();
    }

    private void ReturnToGame()
    {
        endMiniGamePanel.gameObject.SetActive(false);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }

        for (int i = 0; i < miniGameObjects.Length; i++)
        {
            miniGameObjects[i].SetActive(false);
        }

        AudioManager.Instance.ChangeMusicToMainGame();
    }

    private void EndOfMiniGame()
    {
        endMiniGamePanel.SetImage(currentMiniGameType);
        endMiniGamePanel.SetResultText(miniGameManager.GotNumberOfConsumables.ToString());
        endMiniGamePanel.gameObject.SetActive(true);
    }
}
