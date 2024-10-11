using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    private const string TutorialSaveKey = "Tutorial";
    private int currentTutorial;

    [SerializeField] private GameObject firstTutorialText;
    [SerializeField] private GameObject seconTutorialText;
    [SerializeField] private GameObject thirdTutorialText;
    [SerializeField] private GameObject chestButton;
    [SerializeField] private TMP_Text fourthTuturialText;
    [SerializeField] private Button nextButton;
    private string fourthTutorialTextString = "Now you can see your plants in the chest";
    private string currentfourthTutorialTextString;

    private List<Action> tutorialsActions =  new(); 

    private void Start()
    {
        tutorialsActions.Add(AddFirstTutorialAtEvent);
        tutorialsActions.Add(AddSecondTutorialAtEvent);
        tutorialsActions.Add(AddThirdTutorialAtEvent);
        tutorialsActions.Add(AddFourthTutorialAtEvent);

        currentTutorial = PlayerPrefs.GetInt(TutorialSaveKey, 0);

        for(int i = currentTutorial; i < tutorialsActions.Count; i++)
        {
            tutorialsActions[i].Invoke();
        }

        if(currentTutorial == 2 && !Gardener.Instance.PlantController.needTimerText)
        {
            OnThierdTutorial();
        }
    }

    private void AddFirstTutorialAtEvent()
    {
        Gardener.Instance.PlantIsPlanted += OnFirstTutorial;
    }

    private void AddSecondTutorialAtEvent()
    {
        Gardener.Instance.PlantController.StartStage += OnSecondTutorial;
    }

    private void AddThirdTutorialAtEvent()
    {
        Gardener.Instance.PlantController.GrowStageReached += OnThierdTutorial;
    }

    private void AddFourthTutorialAtEvent()
    {
        Gardener.Instance.PlantController.FinalStageReached += OnFourthTutorial;
    }

    private void OnFirstTutorial()
    {
        firstTutorialText.SetActive(true);
        currentTutorial = 1;
        SaveTutorial();

        Gardener.Instance.PlantIsPlanted -= OnFirstTutorial;
    }

    private void OnSecondTutorial()
    {
        firstTutorialText.SetActive(false);
        seconTutorialText.SetActive(true);
        currentTutorial = 2;
        SaveTutorial();

        Gardener.Instance.PlantController.StartStage -= OnSecondTutorial;
    }

    private void OnThierdTutorial()
    {
        if (Gardener.Instance.PlantModel.Stage < 2) return;
        
        seconTutorialText.SetActive(false);
        thirdTutorialText.SetActive(true);
        currentTutorial = 3;
        SaveTutorial();

        Gardener.Instance.PlantController.StartStage += OnEndOfThirdTutorial;

        Gardener.Instance.PlantController.GrowStageReached -= OnThierdTutorial;
    }

    private void OnEndOfThirdTutorial()
    {
        thirdTutorialText.SetActive(false);
        Gardener.Instance.PlantController.StartStage -= OnEndOfThirdTutorial;
    }

    private void OnNextButtonClick()
    {
        currentfourthTutorialTextString = fourthTuturialText.text;
        fourthTuturialText.text = fourthTutorialTextString;
        chestButton.SetActive(true);
        currentTutorial = 4;
        SaveTutorial();

        nextButton.onClick.RemoveListener(OnNextButtonClick);
        nextButton.onClick.AddListener(ResetText);
        nextButton.onClick.AddListener(Gardener.Instance.StartGameCycle);
    }

    private void OnFourthTutorial()
    {
        nextButton.onClick.RemoveListener(Gardener.Instance.StartGameCycle);
        nextButton.onClick.AddListener(OnNextButtonClick);
        Gardener.Instance.PlantController.FinalStageReached -= OnFourthTutorial;
    }

    private void ResetText()
    {
        fourthTuturialText.text = currentfourthTutorialTextString;
        nextButton.onClick.RemoveListener(ResetText);
    }

    private void SaveTutorial()
    {
        PlayerPrefs.SetInt(TutorialSaveKey, currentTutorial);
    }
}
