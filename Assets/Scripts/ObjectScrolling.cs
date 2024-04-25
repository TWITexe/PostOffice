using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectScrolling : MonoBehaviour
{
    public GameObject scrollBar;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private float normalSizeScaling = 1f;
    [SerializeField] private float minSizeScaling = 0.8f;
    [SerializeField] private float scrollingThresholdMultiplier = 1f;
    // Порог скорости скролла, при котором скролл считается движущимся достаточно быстро
    [SerializeField] private float fastScrollSpeedThreshold = 0.5f; 
    // Минимальное расстояние для автоматического доведения скролла, независимо от скорости
    [SerializeField] private float minScrollDistance = 0.05f; 
    // Последнее положение скролла для вычисления скорости
    private float lastScrollPos; 
    
    public float smoothSpeed = 0.1f;
    public float scrollPos = 0;
    public float[] pos;
    public float distance;
    [SerializeField] private GameObject[] StoryID;

    // Флаг для отслеживания процесса перемещения ScrollView
    public bool isScrolling = false; 

    void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        if (!isScrolling && (Input.GetMouseButton(0) || Input.touchCount > 0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            UpdateScrollBar();
        }
        UpdateObjectScale();
        UpdateObjectSprites();
        
        // Обновление lastScrollPos при каждом обновлении
        lastScrollPos = scrollBar.GetComponent<Scrollbar>().value;
    }

    public void UpdateScrollBar()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (!isScrolling && scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollBar.GetComponent<Scrollbar>().value =
                    Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
            }
        }
    }
    
    

    void UpdateObjectScale()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (!isScrolling && scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(normalSizeScaling, normalSizeScaling), 0.20f);
            }
            else
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(minSizeScaling, minSizeScaling), 0.20f);
            }
        }
    }

    void UpdateObjectSprites()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (!isScrolling && scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                // Устанавливаем активный спрайт выбранному объекту
                StoryID[i].GetComponent<Image>().sprite = activeSprite;
                for (int j = 0; j < pos.Length; j++)
                {
                    // Устанавливаем спрайт по умолчанию для не выбранных объектов
                    if (j != i)
                        StoryID[j].GetComponent<Image>().sprite = defaultSprite;
                }
            }
        }
    }

    public void RestartScrollBar()
    {
        scrollBar.GetComponent<Scrollbar>().value = 0;
        scrollPos = 0;
    }

    public void MoveToStory(float position, float smoothSpeed)
    {
        if (isScrolling) return;
        // флаг в true перед началом перемещения
        isScrolling = true; 

        float targetValue = Mathf.Clamp01(position);
        StartCoroutine(SmoothScrolling(targetValue, smoothSpeed));
        // Обновляю значение scrollPos
        scrollPos = position; 
    }

    public IEnumerator SmoothScrolling(float targetValue, float smoothSpeed)
    {
        float currentValue = scrollBar.GetComponent<Scrollbar>().value;

        while (Mathf.Abs(currentValue - targetValue) > 0.01f)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, smoothSpeed * Time.deltaTime);
            scrollBar.GetComponent<Scrollbar>().value = currentValue;
            yield return null;
        }

        UpdateObjectSprites();
        // флаг в false после завершения перемещения
        isScrolling = false; 
    }
    public int GetCurrentIndex()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                return i;
            }
        }
        return -1;
    }
}