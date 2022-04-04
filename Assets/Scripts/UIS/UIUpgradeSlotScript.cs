using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeSlotScript : MonoBehaviour
{
    public Image frame, image;
    public void SetUpgrade(Color colorOfFrame,Sprite img,string title,string description)
    {
        frame.color = colorOfFrame;
        image.sprite = img;
        var dc = GetComponent<DescriptionComponentUI>();
        dc.Title = title;
        dc.Description = description;
    }
}
