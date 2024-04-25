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
        int currentIndex = objectScrolling.GetCurrentIndex();
        if (objectScrolling.scrollBar.GetComponent<Scrollbar>().value < 1)
        {
            float targetPos = objectScrolling.pos[currentIndex + 1];
            objectScrolling.MoveToStory(targetPos,objectScrolling.smoothSpeed);
        }
    }
    public void BackStory()
    {
        Debug.Log("backStory");
        int currentIndex = objectScrolling.GetCurrentIndex();
        if (objectScrolling.scrollBar.GetComponent<Scrollbar>().value > 0)
        {
            float targetPos = objectScrolling.pos[currentIndex - 1];
            objectScrolling.MoveToStory(targetPos,objectScrolling.smoothSpeed);
        }
    }

    
}