using UnityEngine;
using UnityEngine.UI;

public class ObjectScrolling : MonoBehaviour
{
    [SerializeField] private GameObject scrollBar;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite activeSprite;

    private float scrollPos = 0;
    public float[] pos;
    public float distance;
    [SerializeField] private GameObject[] StoryID;

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
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            UpdateScrollBar();
        }
        UpdateObjectScale();
        UpdateObjectSprites();
    }

    void UpdateScrollBar()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollBar.GetComponent<Scrollbar>().value =
                    Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.25f);
            }
        }
    }

    void UpdateObjectScale()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.3f);
                for (int j = 0; j < pos.Length; j++)
                {

                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(i).localScale,
                            new Vector2(0.5f, 0.5f), 0.3f);
                    }
                }
            }
        }
    }

    void UpdateObjectSprites()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
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
}