using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionComponent : MonoBehaviour
{
    public string Title,Description;

    private void OnMouseEnter()
    {
        Debug.Log("mouse entered");
        if (!EventSystem.current.IsPointerOverGameObject())
            DescriptionScript.Instance.ShowDetails(Title, Description);
    }

    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) 
            DescriptionScript.Instance.Close();
    }
}
