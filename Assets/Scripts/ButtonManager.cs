using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject homeButton;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject secondScreen;
    [SerializeField] private GameObject censorship;

    public void StartButton()
    {
        homeButton.SetActive(true);
        mainScreen.SetActive(false);
        censorship.SetActive(false);
        secondScreen.SetActive(true);
    }
    public void FrontTriangle()
    {
        homeButton.SetActive(true);
        mainScreen.SetActive(false);
        censorship.SetActive(true);
        secondScreen.SetActive(false);
    }

    public void Home()
    {
        homeButton.SetActive(false);
        mainScreen.SetActive(true);
        censorship.SetActive(false);
        secondScreen.SetActive(false);
    }
}