using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIToggableScript : MonoBehaviour
{
    static bool isUIOverride;
    public static bool IsUIOverride 
    { 
        get => isUIOverride && activeElement!=null;
        private set => isUIOverride = value; }
    static UIToggableScript activeElement;
    [SerializeField]
    public Canvas canvas;

    public static void Close()
    {
        if(activeElement!=null)
        {
            activeElement.canvas.gameObject.SetActive(false);
            activeElement = null;
        }
    }

    private void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }

    private void OnMouseDown()
    {
        if (IsUIOverride)
            return;
        Debug.Log($"{name} clicked");
        if(activeElement==this)
        {
            activeElement = null;
            canvas.gameObject.SetActive(false);
            return;
        }
        if (activeElement)
            activeElement.canvas.gameObject.SetActive(false);
        activeElement = this;
        canvas.gameObject.SetActive(true);
    }
}
