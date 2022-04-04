using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionScript : MonoBehaviour
{
    public static DescriptionScript Instance;
    [SerializeField]
    private Text titleT, descriptionT;
    [SerializeField]
    private GameObject helperUI;

    private DescriptionScript()
    {
        Instance = this;
    }

    public void ShowDetails(string title,string desc)
    {
        titleT.text = title;
        descriptionT.text = desc;
        helperUI.SetActive(true);
    }

    public void Close() => helperUI.SetActive(false);
}
