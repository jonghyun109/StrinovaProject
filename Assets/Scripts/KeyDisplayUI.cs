using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class KeyDisplayUI : MonoBehaviour
{
    public Button wKey, aKey, sKey, dKey, ctrlKey, shiftKey; // UI 버튼들
    private Dictionary<KeyCode, Button> keyMappings; // 키와 버튼 매핑

    private Color defaultColor = Color.gray; // 기본 색상
    private Color pressedColor = Color.blue; // 눌렀을 때 색상

    void Start()
    {
        //** 키와 UI 버튼 매핑
        keyMappings = new Dictionary<KeyCode, Button>
        {
            { KeyCode.W, wKey },
            { KeyCode.A, aKey },
            { KeyCode.S, sKey },
            { KeyCode.D, dKey },
            { KeyCode.LeftControl, ctrlKey },
            { KeyCode.LeftShift, shiftKey }
        };

        foreach (var button in keyMappings.Values)
        {
            SetButtonColor(button, defaultColor);
        }
    }

    void Update()
    {
        foreach (var key in keyMappings.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                SetButtonColor(keyMappings[key], pressedColor); // 키를 누르면 색 변경
            }
            if (Input.GetKeyUp(key))
            {
                SetButtonColor(keyMappings[key], defaultColor);
            }
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        button.colors = cb;
    }
}
