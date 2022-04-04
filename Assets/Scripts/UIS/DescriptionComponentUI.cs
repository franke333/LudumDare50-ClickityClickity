using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionComponentUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Title, Description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionScript.Instance.ShowDetails(Title, Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionScript.Instance.Close();
    }
}
