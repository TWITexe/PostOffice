using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StorySwap : MonoBehaviour
{
    private ObjectScrolling objectScrolling;


    private void Start()
    {
        objectScrolling = gameObject.GetComponent<ObjectScrolling>();
    }
    public void NextStory()
    {
        Debug.Log("nextStory");
        //objectScrolling.scrollPos = Mathf.Clamp(objectScrolling.scrollPos + 0.25f, 0, objectScrolling.pos.Length - 1); 
        if (objectScrolling.scrollPos < 1)
        {
            objectScrolling.scrollBar.GetComponent<Scrollbar>().value += 0.25f;
            objectScrolling.scrollPos++;
        }

        objectScrolling.UpdateScrollBar();
    }

    public void BackStory()
    {
        Debug.Log("backStory");
        //objectScrolling.scrollPos = Mathf.Clamp(objectScrolling.scrollPos - 0.25f, 0, objectScrolling.pos.Length - 1);
        if (objectScrolling.scrollPos > 0)
        {
            objectScrolling.scrollBar.GetComponent<Scrollbar>().value -= 0.25f;
            objectScrolling.scrollPos--;
        }
        objectScrolling.UpdateScrollBar();
    }
}