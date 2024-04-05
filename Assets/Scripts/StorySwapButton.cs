using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StorySwapButton : MonoBehaviour
{
    private ObjectScrolling objectScrolling;
    [SerializeField] private int speedSlide = 20;


    private void Start()
    {
        objectScrolling = gameObject.GetComponent<ObjectScrolling>();
    }
    public void NextStory()
    {
        Debug.Log("nextStory");
        if (objectScrolling.scrollPos < 1)
        {
            objectScrolling.scrollBar.GetComponent<Scrollbar>().value += 0.20f;
            objectScrolling.scrollPos++;
        }

        objectScrolling.UpdateScrollBar();
    }

    public void BackStory()
    {
        Debug.Log("backStory");
        if (objectScrolling.scrollPos > 0)
        {
            objectScrolling.scrollBar.GetComponent<Scrollbar>().value -= 0.20f;
            objectScrolling.scrollPos--;
        }
        objectScrolling.UpdateScrollBar();
    }
    
}