using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public GameObject upgradeMenu;
    public GameObject ThirdUpgradeSlot;
    public List<UIUpgradeSlotScript> upgradeSlots;

    public List<Color> frameColors;
    private UpgradeManager()
    {
        Instance = this;
    }
    Action upgr0, upgr1, upgr2;
    public void ShowUpgradeUI(Action upgrade1, Action upgrade2, Action upgrade3, List<UpgradeEntry> entries)
    {
        upgr0 = upgrade1;
        upgr1 = upgrade2;
        upgr2 = upgrade3;
        GameManager.Instance.IsRunning = false;
        upgradeMenu.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            var ent = entries[i];
            upgradeSlots[i].SetUpgrade(frameColors[ent.rarity], ent.sprite, ent.Title, ent.Desc);
        }
        if (GameManager.Instance.DisplayThirdUpgrade())
            ThirdUpgradeSlot.SetActive(true);
        else
            ThirdUpgradeSlot.SetActive(false);

    }

    public void ActiveUpgrade(int index)
    {
        switch (index)
        {
            case 0:
                upgr0.Invoke();
                break;
            case 1:
                upgr1.Invoke();
                break;
            case 2:
                upgr2.Invoke();
                break;
            default:
                break;
        }
        upgradeMenu.SetActive(false);
        GameManager.Instance.IsRunning = true;
    }
}
