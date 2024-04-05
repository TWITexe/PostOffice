using UnityEngine;
using UnityEngine.UI;

public class ButtonScrolling : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 0.1f;
    public void OnButtonClick()
    {
        float buttonPosition = transform.GetSiblingIndex() / (float)(transform.parent.childCount - 1);
        ObjectScrolling scrollView = transform.parent.GetComponent<ObjectScrolling>();
        scrollView.MoveToStory(buttonPosition, smoothSpeed);
    }
}