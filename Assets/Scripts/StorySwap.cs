using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySwap : MonoBehaviour
{
    [SerializeField] private GameObject[] stories;
    private int storyIndex = 0;
    
    public void NextStory()
    {
        Debug.Log("nextStory");
        storyIndex = (storyIndex + 1) % stories.Length;
        ActivateStory(storyIndex);
    }

    public void BackStory()
    {
        Debug.Log("backStory");
        storyIndex = (storyIndex - 1 + stories.Length) % stories.Length;
        ActivateStory(storyIndex);
    }

    private void ActivateStory(int _story)
    {
        foreach (var story in stories)
        {
            story.SetActive(false);
        }
        stories[_story].SetActive(true);
    }
}