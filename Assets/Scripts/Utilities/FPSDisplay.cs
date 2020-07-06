using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    public bool Show = false;

    void Update()
    {
        deltaTime += ( Time.unscaledDeltaTime - deltaTime ) * 0.1f;
    }

    void OnGUI()
    {
        if (Show)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            //Rect rect = new Rect(0, h - h * 4 / 100, w, h * 4 / 100);
            Rect rect = new Rect(0, 0, w, h * 4 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 4 / 100;
            style.normal.textColor = Color.red;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            //string text = string.Format("{0:0.0} ms ({1:0.} fps)" , msec , fps);
            string text = $"fps:{Mathf.FloorToInt(fps)}";
            GUI.Label(rect , text , style);
        }
    }
}