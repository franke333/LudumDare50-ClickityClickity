using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct UpgradeEntry
{
    public string Title, Desc;
    public Sprite sprite;
    public int rarity;
}

public abstract class AbstractTower : MonoBehaviour
{
    public int cost;
    public TowerSlotScript slot;
    public GameObject RangeDisplayer;
    public Text upgradeText;
    public abstract void Upgrade();

}
