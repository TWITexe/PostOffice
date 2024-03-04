using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public void SecondMenu()
    {
        ButtonManager.instance.DisableButton("StartButton");
        ButtonManager.instance.DisableButton("StoryButton");
        ButtonManager.instance.EnableButton("HomeButton");
        ButtonManager.instance.EnableButton("FrontTriangleButton");
        ButtonManager.instance.EnableButton("LetterPathButton");
        ButtonManager.instance.EnableButton("CensorshipButton");
        ButtonManager.instance.EnableButton("MemoryButton");
        
        Camera.main.transform.position = new Vector3(0f, -10.45f, -10f);
    }

    public void Home() // trash
    {
        ButtonManager.instance.DisableButton("HomeButton");
        ButtonManager.instance.EnableButton("StartButton");
        ButtonManager.instance.EnableButton("StoryButton");
        
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
    }

    public void FrontTriangle()
    {
        ButtonManager.instance.DisableButton("FrontTriangleButton");
        ButtonManager.instance.DisableButton("LetterPathButton");
        ButtonManager.instance.DisableButton("CensorshipButton");
        ButtonManager.instance.DisableButton("MemoryButton");
        
        Camera.main.transform.position = new Vector3(0f, -21.3f, -10f);
    }
    
}
