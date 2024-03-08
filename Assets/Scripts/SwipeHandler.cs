using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    [SerializeField] private MoveInformationPanel informationPanel;
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
        switch (direction)
        {
            case SwipeDetection.SwipeDirection.Left:
                informationPanel.BackMove();
                Debug.Log("left");
                break;
            case SwipeDetection.SwipeDirection.Right:
                informationPanel.NextMove();
                Debug.Log("right");
                break;
        }
    }
}