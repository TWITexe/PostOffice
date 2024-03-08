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
        if (!informationPanel.MovementBlocked)
        {
            switch (direction)
            {
                case SwipeDetection.SwipeDirection.Left:
                    informationPanel.NextMove();
                    Debug.Log("left");
                    break;
                case SwipeDetection.SwipeDirection.Right:
                    informationPanel.BackMove();
                    Debug.Log("right");
                    break;
            }
        }
    }
}