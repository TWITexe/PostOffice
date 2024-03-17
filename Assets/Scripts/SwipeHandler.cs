using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private MoveInformationPanel informationPanel;
    [SerializeField] private GameObject story;
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
        if (!informationPanel.MovementBlocked || story.GetComponent<ButtonManager>().storyPanel.activeSelf == true)
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
        if (story.GetComponent<ButtonManager>().storyPanel.activeSelf == true)
        {
            
            story.GetComponent<StorySwap>().NextStory();
        }
    }
    private void SwipeActionRight()
    {
        if (!informationPanel.MovementBlocked)
        {
            informationPanel.BackMove();
        }
        if (story.GetComponent<ButtonManager>().storyPanel.activeSelf == true)
        {
            
            story.GetComponent<StorySwap>().BackStory();
        }
    }
}