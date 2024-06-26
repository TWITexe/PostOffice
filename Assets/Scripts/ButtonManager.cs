using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    // К ней закреплены все длинные странички для свайпов
    [SerializeField] private GameObject informationPanel;
    
    [SerializeField] private GameObject navigationButton;
    [SerializeField] private ObjectScrolling objectMemoryScrolling;
    [SerializeField] public ObjectScrolling storyContent;
    [SerializeField] public ObjectScrolling memoryContent;
    
    
    [SerializeField] private GameObject homeButton;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject secondScreen;
    [SerializeField] private GameObject censorship;
    [SerializeField] private GameObject censorshipUI;
    [SerializeField] private GameObject frontTriangle;
    [SerializeField] private GameObject frontTriangleUI;
    [SerializeField] private GameObject letterPath;
    [SerializeField] private GameObject letterPathUI;
    [SerializeField] public GameObject memory;
    [SerializeField] private GameObject navigationStoryButton;
    [SerializeField] public GameObject storyPanel;
    [SerializeField] public GameObject icoSlide3;
    [SerializeField] public GameObject icoSlide5;
    // remake "letterOneMemory"
    [SerializeField] public GameObject letterMemory;

    public void StartButton()
    {
        SetActiveObjects(true, false, true, false, false, false, false, false, false,true);
    }

    public void Censorship()
    {
        SetActiveObjects(true, false, false, true, false, false, false, false, true, false);
        informationPanel.GetComponent<MoveInformationPanel>().maxAllowedMoves = 3;
    }

    public void FrontTriangle()
    {
        SetActiveObjects(true, false, false, false, true, false, false, false, true,false);
        informationPanel.GetComponent<MoveInformationPanel>().maxAllowedMoves = 5;
    }

    public void Memory()
    {
        SetActiveObjects(true, false, false, false, false, false, true, false,false,true);
        memoryContent.RestartScrollBar();
    }

    public void LetterPath()
    {
        SetActiveObjects(true, false, false, false, false, true, false, false, true, false);
        informationPanel.GetComponent<MoveInformationPanel>().maxAllowedMoves = 3;
    }

    public void Home()
    {
        if (storyContent.isScrolling == false && memoryContent.isScrolling == false)
        SetActiveObjects(false, true, false, false, false, false, false, false, false,true);
    }

    public void Story()
    {
        SetActiveObjects(true, false, false, false, false, false, false, true, false, true);
        storyContent.RestartScrollBar();

    }
    private void SetActiveObjects(bool homeButtonActive, bool mainScreenActive, bool secondScreenActive, bool censorshipActive, bool frontTriangleActive, bool letterPathActive, bool memoryActive, bool storyActive, bool navigation, bool moveIsBlock)
    {
        navigationButton.SetActive(navigation);
        homeButton.SetActive(homeButtonActive);
        mainScreen.SetActive(mainScreenActive);
        secondScreen.SetActive(secondScreenActive);
        censorship.SetActive(censorshipActive);
        censorshipUI.SetActive(censorshipActive);
        frontTriangle.SetActive(frontTriangleActive);
        frontTriangleUI.SetActive(frontTriangleActive);
        letterPath.SetActive(letterPathActive);
        letterPathUI.SetActive(letterPathActive);
        memory.SetActive(memoryActive);
        navigationStoryButton.SetActive(storyActive);
        storyPanel.SetActive(storyActive);
        informationPanel.GetComponent<MoveInformationPanel>().MovementBlocked = moveIsBlock;
        informationPanel.GetComponent<MoveInformationPanel>().FirstPosition();
        
        icoSlide5.SetActive(frontTriangleActive);
        if(censorshipActive || letterPathActive)
            icoSlide3.SetActive(true);
        else
            icoSlide3.SetActive(false);


    }
    
    public void CloseLetter(GameObject OpenLetter)
    {
        letterMemory.SetActive(false);
        OpenLetter.SetActive(false);
        homeButton.SetActive(true);
    }
    public void OpenLetter(GameObject OpenLetter)
    {
        if (objectMemoryScrolling.isScrolling == false)
        {
            letterMemory.SetActive(true);
            OpenLetter.SetActive(true);
            homeButton.SetActive(false);
        }
    }
    
}