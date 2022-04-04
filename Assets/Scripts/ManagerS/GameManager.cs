using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public struct LuckProbabilities
{
    public int cost;
    public int common;
    public int rare;
    public int epic;
    public int legendary;
    
    public int GetRarityIndex()
    {
        int sum = common + rare + epic + legendary;
        int drop = UnityEngine.Random.Range(0, sum);
        if (drop < common)
            return 0;
        drop -= common;
        if (drop < rare)
            return 1;
        drop -= rare;
        if (drop < epic)
            return 2;
        return 3;
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<TowerSlotScript> slotsList;
    [SerializeField]
    List<int> slotsCost;
    [SerializeField]
    List<LuckProbabilities> lucksList;
    int slotsIndex = 0,luckIndex=-1;
    [SerializeField]
    Text tComm, tRare, tEpic, TLege;
    private bool isRunning;
    public GameObject Strawberry;
    public Text textButtonSlots, textButtonLuck;
    [SerializeField]
    GameObject EndScreen;
    [SerializeField]
    Text endScreenText;
    public bool IsRunning { get => isRunning; set => isRunning = value; }

    public int GetWave { get => CrabSpawning.Instance.GetWave; }

    private float gameSpeed = 1f;

    [SerializeField]
    private int money=20;

    public int Score { get; set; }

    public uint KilledCrabs { get; private set; }
    public float GameSpeed { get => isRunning ? gameSpeed : 0; set => gameSpeed = value; }
    public int Money { get => money; set => money = value; }

    public static GameManager Instance;

    private GameManager()
    {
        Instance = this;
    }

    public void AddKilledCrab()
    {
        KilledCrabs++;
    }

    public void BuyExpansion()
    {
        if (slotsCost[slotsIndex] > Money)
            return;
        SoundManager.Instance.PlayUpgrade();
        Money -= slotsCost[slotsIndex];
        slotsList[slotsIndex * 2].gameObject.SetActive(true);
        slotsList[slotsIndex * 2 + 1].gameObject.SetActive(true);
        slotsIndex++;
        if (slotsIndex == slotsCost.Count)
            textButtonSlots.transform.parent.gameObject.SetActive(false);
        else
            textButtonSlots.text = $"Slots+\n{slotsCost[slotsIndex]}$";
    }

    public void BuyLuck()
    {
        if (lucksList[luckIndex+1].cost > Money)
            return;
        SoundManager.Instance.PlayUpgrade();
        Money -= lucksList[luckIndex + 1].cost;
        luckIndex++;
        if (luckIndex+1 == lucksList.Count)
            textButtonLuck.transform.parent.gameObject.SetActive(false);
        else
            textButtonLuck.text = $"Luck+\n{lucksList[luckIndex + 1].cost}$";
        tComm.text = lucksList[luckIndex].common.ToString();
        tRare.text = lucksList[luckIndex].rare.ToString();
        tEpic.text = lucksList[luckIndex].epic.ToString();
        TLege.text = lucksList[luckIndex].legendary.ToString();
    }

    void Start()
    {
        IsRunning = true;
        foreach (var slot in slotsList)
            slot.gameObject.SetActive(false);
        BuyExpansion();
        BuyLuck();
    }

    public int GetLuck() => lucksList[luckIndex].GetRarityIndex();

    public bool DisplayThirdUpgrade() => UnityEngine.Random.Range(0, lucksList.Count-2) < luckIndex;

    public void End()
    {
        isRunning = false;
        endScreenText.text += $" {Score}";
        EndScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
