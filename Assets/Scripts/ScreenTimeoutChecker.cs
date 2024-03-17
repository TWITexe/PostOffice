using UnityEngine;

public class ScreenTimeoutChecker : MonoBehaviour
{
    public float timeoutDuration = 60f;
    private float lastInputTime;
    [SerializeField] private ButtonManager HomeMethod;

    void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            lastInputTime = Time.time;
        }
        else
        {
            if (Time.time - lastInputTime > timeoutDuration) 
            {
                HomeMethod.GetComponent<ButtonManager>().Home();
            }
        }
    }
}
