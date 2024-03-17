using DG.Tweening;
using UnityEngine;

public class MoveInformationPanel : MonoBehaviour
{
    [SerializeField] private Transform[] pointTransforms;
    private int currentIndex = 0;
    private float moveDuration = 1.5f;
    private bool isMoving = false;
    public bool MovementBlocked { get; set; } = true;

    private void Direction(bool forward)
    {
        if (!isMoving)
        {
            isMoving = true;
            if (forward)
            {
                currentIndex = (currentIndex + 1) % pointTransforms.Length;
            }
            else
            {
                currentIndex = (currentIndex - 1 + pointTransforms.Length) % pointTransforms.Length;
            }
            MoveTo(pointTransforms[currentIndex]);
        }
    }

    public void MoveTo(Transform pointMove)
    {
        transform.DOMove(pointMove.position, moveDuration).OnComplete(() => isMoving = false);
    }

    public void NextMove()
    {
        if (currentIndex < pointTransforms.Length - 1)
        {
            Direction(true);
        }
    }

    public void BackMove()
    {
        if (currentIndex > 0)
        {
            Direction(false);
        }
    }

    public void FirstPosition()
    {
        currentIndex = 0;
        transform.position = pointTransforms[0].position;
    }
}