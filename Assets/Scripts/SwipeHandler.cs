using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
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
                Debug.Log("Swipe Left");
                break;
            case SwipeDetection.SwipeDirection.Right:
                Debug.Log("Swipe Right");
                break;
        }
    }
}