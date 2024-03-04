using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    // Ссылка на единственный экземпляр для удобного доступа
    public static ButtonManager instance;
    // Создал словарь со всеми кнопками для удобства
    private Dictionary<string, Button> namedButtons = new Dictionary<string, Button>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        // Заполнение словаря именованных кнопок
        PopulateButtonDictionary();

    }

    private void Start()
    {
        // рефакторинг \/
        DisableButton("HomeButton");
        DisableButton("FrontTriangleButton");
        DisableButton("LetterPathButton");
        DisableButton("CensorshipButton");
        DisableButton("MemoryButton");
    }

    private void PopulateButtonDictionary()
    {
        // Нахождение всех кнопок на сцене и добавление их в словарь с их именами
        Button[] foundButtons = GameObject.FindObjectsOfType<Button>();
        foreach (Button button in foundButtons)
        {
            // У каждой кнопки уникальное имя
            namedButtons.Add(button.name, button);
        }
    }

    // Деактивируем кнопку
    public void DisableButton(string buttonName)
    {
        if (namedButtons.ContainsKey(buttonName))
        {
            namedButtons[buttonName].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Кнопка с именем " + buttonName + " не найдена.");
        }
    }

    // Активируем кнопку
    public void EnableButton(string buttonName)
    {
        if (namedButtons.ContainsKey(buttonName))
        {
            namedButtons[buttonName].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Кнопка с именем " + buttonName + " не найдена.");
        }
    }
}