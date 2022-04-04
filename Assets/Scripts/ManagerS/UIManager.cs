using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text infoText;

    public float infoRefreshRate;

    private GameManager gm;
    private float timeUntilNextRefresh;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextRefresh = infoRefreshRate;
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextRefresh -= Time.deltaTime;
        if (timeUntilNextRefresh < 0)
        {
            infoText.text = BuildInfoString();
            timeUntilNextRefresh = infoRefreshRate;
        }
    }

    private string BuildInfoString()
    {
        return $"Slain Crabs: {gm.KilledCrabs}" +
            $"\nCrab Meat: {gm.Money}$" +
            $"\nScore: {gm.Score}\nWave: {gm.GetWave}";
    }
}
