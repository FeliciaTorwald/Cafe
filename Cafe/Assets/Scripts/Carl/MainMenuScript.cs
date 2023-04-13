using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuHolder;
    [SerializeField] private GameObject optionsMenuHolder;
    [SerializeField] private GameObject creditsMenuHolder;
    [SerializeField] private Animator thisAnimator;

    private string menuInAnimatorTrigger = "MenuIn";
    private string menuOutAnimatorTrigger = "MenuOut";
    private string creditsInAnimatorTrigger = "CreditsIn";
    private string creditsOutAnimatorTrigger = "CreditsOut";

    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    public void StartGame(int sceneID)
    {
        //Loads scene by scene ID
        SceneManager.LoadScene(sceneID);
    }

    public void ShowOptionsMenu(bool fadeIn)
    {
        Debug.Log("Options Click");
        //Toggle display of the Options Menu
        ResetTriggers();
        if (fadeIn)
        {
           thisAnimator.SetTrigger(menuInAnimatorTrigger);
        }
        else if (!fadeIn)
            thisAnimator.SetTrigger(menuOutAnimatorTrigger);
    }

    public void ShowCreditsMenu(bool fadeIn)
    {
        Debug.Log("Credits Click");
        //Toggle display of the Credits Menu
        ResetTriggers();
        if (fadeIn)
        {
            thisAnimator.SetTrigger(creditsInAnimatorTrigger);
        }
        else if (!fadeIn)
            thisAnimator.SetTrigger(creditsOutAnimatorTrigger);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ResetTriggers()
    {
        foreach (AnimatorControllerParameter param in thisAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                thisAnimator.ResetTrigger(param.name);
        }
    }
}
