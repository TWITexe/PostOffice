using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private MoveInformationPanel informationPanel;
    [SerializeField] private GameObject story;
    [SerializeField] private GameObject letters;
    [SerializeField] private ButtonManager panelBank;
    private void OnEnable()
    {
        SwipeDetection.OnSwipe += HandleSwipe;
    }

    private void OnDisable()
    {
        SwipeDetection.OnSwipe -= HandleSwipe;
    }
 
    private void HandleSwipe(SwipeDetection.SwipeDirection direction)
    {
        if (!informationPanel.MovementBlocked || 
            panelBank.GetComponent<ButtonManager>().storyPanel.activeSelf == true ||
            panelBank.GetComponent<ButtonManager>().memory.activeSelf == true)
        {
            switch (direction)
            {
                case SwipeDetection.SwipeDirection.Left:
                    SwipeActionLeft();
                    Debug.Log("left");
                    break;
                case SwipeDetection.SwipeDirection.Right:
                    SwipeActionRight();
                    Debug.Log("right");
                    break;
            }
        }
    }

    private void SwipeActionLeft()
    {
        if (!informationPanel.MovementBlocked)
        {
            informationPanel.NextMove();
        }
        if (panelBank.GetComponent<ButtonManager>().storyPanel.activeSelf == true)
        {
            
            story.GetComponent<StorySwapButton>().NextStory();
        }
        if (panelBank.GetComponent<ButtonManager>().memory.activeSelf == true && panelBank.GetComponent<ButtonManager>().letterMemory.activeSelf == false)
        {
            letters.GetComponent<StorySwapButton>().NextStory();
        }
    }
    private void SwipeActionRight()
    {
        if (!informationPanel.MovementBlocked)
        {
            informationPanel.BackMove();
        }
        if (panelBank.GetComponent<ButtonManager>().storyPanel.activeSelf == true)
        {
            story.GetComponent<StorySwapButton>().BackStory();
        }
        if (panelBank.GetComponent<ButtonManager>().memory.activeSelf == true && panelBank.GetComponent<ButtonManager>().letterMemory.activeSelf == false)
        {
            letters.GetComponent<StorySwapButton>().BackStory();
        }
    }
}