using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 fingerDownPosition; 
    private Vector2 fingerUpPosition;
    
    // Определяет, должен ли происходить обнаружение свайпа только после отпускания пальца
    [SerializeField] private bool detectSwipeOnlyAfterRelease = false; 
    [SerializeField] private float minDistanceForSwipe = 20f; 

    // Обновил позицию отпускания
    public delegate void SwipeAction(SwipeDirection direction);
    public static event SwipeAction OnSwipe;

    public enum SwipeDirection
    {
        Left, 
        Right
    }

    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0); 

            switch (touch.phase)
            {
                // палец только коснулся экрана
                case TouchPhase.Began: 
                    fingerDownPosition = touch.position; // Запоминаем позицию касания
                    fingerUpPosition = touch.position; // Инициализируем позицию отпускания также
                    break;
                case TouchPhase.Moved:
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        // Обновляем позицию отпускания
                        fingerUpPosition = touch.position; 
                        DetectSwipe();
                    }
                    break;
                // палец был отпущен
                case TouchPhase.Ended: 
                    fingerUpPosition = touch.position; 
                    DetectSwipe();
                    break;
            }
        }
    }
    
    private void DetectSwipe()
    {
        if (Vector2.Distance(fingerDownPosition, fingerUpPosition) >= minDistanceForSwipe) 
        {
            // Вычисляем вектор направления свайпа
            Vector2 direction = fingerUpPosition - fingerDownPosition;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
            {
                OnSwipe?.Invoke(direction.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
            }
        }
    }
}