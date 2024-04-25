using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLetterView : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject openLetterGameObject;
    [SerializeField] private Sprite firstImage;
    [SerializeField] private Sprite secondImage;
    public void NextPage()
    {
        openLetterGameObject.GetComponent<Image>().sprite = secondImage;
        nextButton.SetActive(false);
        backButton.SetActive(true);
    }
    public void BackPage()
    {
        openLetterGameObject.GetComponent<Image>().sprite = firstImage;
        nextButton.SetActive(true);
        backButton.SetActive(false);
    }
}
