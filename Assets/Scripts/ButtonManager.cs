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
    [SerializeField] private GameObject frontTriangle;
    [SerializeField] private GameObject letterPath;
    [SerializeField] private GameObject memory;

    public void StartButton()
    {
        SetActiveObjects(true, false, true, false, false, false, false);
    }

    public void Censorship()
    {
        SetActiveObjects(true, false, false, true, false, false, false);
    }

    public void FrontTriangle()
    {
        SetActiveObjects(true, false, false, false, true, false, false);
    }

    public void Memory()
    {
        SetActiveObjects(true, false, false, false, false, false, true);
    }

    public void LetterPath()
    {
        SetActiveObjects(true, false, false, false, false, true, false);
    }

    public void Home()
    {
        SetActiveObjects(false, true, false, false, false, false, false);
    }
    private void SetActiveObjects(bool homeButtonActive, bool mainScreenActive, bool secondScreenActive, bool censorshipActive, bool frontTriangleActive, bool letterPathActive, bool memoryActive)
    {
        homeButton.SetActive(homeButtonActive);
        mainScreen.SetActive(mainScreenActive);
        secondScreen.SetActive(secondScreenActive);
        censorship.SetActive(censorshipActive);
        frontTriangle.SetActive(frontTriangleActive);
        letterPath.SetActive(letterPathActive);
        memory.SetActive(memoryActive);
    }
    
}