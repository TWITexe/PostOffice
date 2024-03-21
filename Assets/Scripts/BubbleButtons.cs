using UnityEngine;
using UnityEngine.UI;

public class BubbleButtons : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] pressedSprites;
    [SerializeField] private GameObject[] windows;

    private bool isWindowOpen = false;
    private GameObject currentWindow;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Создаем локальную копию переменной i
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (isWindowOpen && currentWindow == windows[buttonIndex])
        {
            CloseWindow();
            return;
        }

        if (isWindowOpen)
            CloseWindow();

        OpenWindow(buttonIndex);
    }

    private void OpenWindow(int windowIndex)
    {
        windows[windowIndex].SetActive(true);
        isWindowOpen = true;
        currentWindow = windows[windowIndex];

        buttons[windowIndex].image.sprite = pressedSprites[windowIndex];
    }

    private void CloseWindow()
    {
        currentWindow.SetActive(false);
        isWindowOpen = false;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = normalSprites[i];
        }

        currentWindow = null;
    }
}