using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoveInformationPanel : MonoBehaviour
{
    [SerializeField] private GameObject NextButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private Transform[] pointTransforms;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite currentIndexSprite;
    [SerializeField] private GameObject[] idSlides3;
    [SerializeField] private GameObject[] idSlides5;
    private int currentIndex = 0;
    private float moveDuration = 1.5f;
    private bool isMoving = false;
    [SerializeField] public bool MovementBlocked { get; set; } = true;
    public int maxAllowedMoves  = 3;

    private void Direction(bool forward)
    {
        if (!isMoving && currentIndex < maxAllowedMoves)
        {
            isMoving = true;
            if (forward)
            {
                //currentIndex = (currentIndex + 1) % maxAllowedMoves;
                currentIndex++;
            }
            else
            {
                //currentIndex = (currentIndex - 1 + maxAllowedMoves ) % maxAllowedMoves;
                currentIndex--;
            }
            MoveTo(pointTransforms[currentIndex]);
            UpdateSlidesSprites();
        }
    }

    public void MoveTo(Transform pointMove)
    {
        transform.DOMove(pointMove.position, moveDuration).OnComplete(() => isMoving = false);
        UpdateGameObjectsVisibility();
    }

    public void NextMove()
    {
        if (currentIndex < maxAllowedMoves - 1)
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
        UpdateSlidesSprites();
        UpdateGameObjectsVisibility();
    }
    private void UpdateGameObjectsVisibility()
    {
        BackButton.SetActive(currentIndex == maxAllowedMoves - 1 || currentIndex > 0);
        NextButton.SetActive(currentIndex == 0 || currentIndex < maxAllowedMoves - 1);
    }
    private void UpdateSlidesSprites()
    {
        if (maxAllowedMoves < 5)
        {
            for (int i = 0; i < idSlides3.Length; i++)
            {
                if (i == currentIndex)
                {
                    SetSprite(idSlides3[i], currentIndexSprite);
                }
                else
                {
                    SetSprite(idSlides3[i], defaultSprite);
                }
            }
        }
        else
        {
            for (int i = 0; i < idSlides5.Length; i++)
            {
                if (i == currentIndex)
                {
                    SetSprite(idSlides5[i], currentIndexSprite);
                }
                else
                {
                    SetSprite(idSlides5[i], defaultSprite);
                }
            }
        }
    }

    private void SetSprite(GameObject obj, Sprite sprite)
    {
        var spriteRenderer = obj.GetComponent<Image>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}