using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectEnable : MonoBehaviour
{
    
    public List<GameObject> objects = new List<GameObject>();
    private void OnEnable()
    {
        foreach (var obj in objects)
            if (!obj.activeSelf)
                obj.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (var obj in objects)
            if (obj.activeSelf)
                obj.SetActive(false);
    }
}
