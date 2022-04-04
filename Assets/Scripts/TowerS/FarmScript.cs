using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarmScript : AbstractTower
{
    private GameManager gm;

    private int upgradeCost = 5;
    [SerializeField]
    private float baseRange;
    [SerializeField]
    private CircleCollider2D rangeCollider;

    [Space]
    public List<UpgradeEntry> upgrades;


    float colliderToRangeDisplayRatio;
    //common,rare,epic,legend
    int[] specificUpgrades = new int[] { 0, 0, 0, 0 };
    float[] rangeUpgrades = new float[] { 0.25f, 0.5f };

    private List<CrabScript> crabsInRange = new List<CrabScript>();
    void Start()
    {
        gm = GameManager.Instance;
        colliderToRangeDisplayRatio = rangeCollider.radius / RangeDisplayer.transform.localScale.x;
    }

    // Update is called once per frame
    void ProcessDeath()
    {
        int extraMeat = 1;
        for (int j = 0; j < specificUpgrades[0]; j++)
            if (UnityEngine.Random.Range(0f, 1f) <= 0.5f)
                extraMeat++;
        for (int j = 0; j < specificUpgrades[1]; j++)
            if (UnityEngine.Random.Range(0f, 1f) <= 0.33f)
                extraMeat+=2;
        for (int j = 0; j < specificUpgrades[2]; j++)
            if (UnityEngine.Random.Range(0f, 1f) <= 0.2f)
                extraMeat+=4;
        for (int j = 0; j < specificUpgrades[3]; j++)
            if (UnityEngine.Random.Range(0f, 1f) <= 0.01f)
                extraMeat+=100;
        gm.Money += extraMeat;
    }
    public void UpgradeRange(float baseRatio)
    {
        rangeCollider.radius += baseRange * baseRatio;
        RangeDisplayer.transform.localScale = new Vector3(rangeCollider.radius / colliderToRangeDisplayRatio,
            rangeCollider.radius / colliderToRangeDisplayRatio);
        upgradeCost *= 2;
        upgradeText.text = $"Upgrade: {upgradeCost}$";
    }
    public void UpgradeSpecial(int rarity)
    {
        specificUpgrades[rarity]++;
        upgradeCost *= 2;
        upgradeText.text = $"Upgrade: {upgradeCost}$";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
        {
            crabsInRange.Add(cs);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
        {
            crabsInRange.Remove(cs);
            if (cs.HP <= 0)
                ProcessDeath();
        }
    }

    public override void Upgrade()
    {
        if (gm.Money < upgradeCost)
        {
            return;
        }
        gm.Money -= upgradeCost;

        Action[] actionList = new Action[3];
        List<UpgradeEntry> upgradesInfo = new List<UpgradeEntry>();
        for (int i = 0; i < 3; i++)
        {
            int rarity = gm.GetLuck();
            if (rarity < 2)
            {
                int upgrType = UnityEngine.Random.Range(0, 2);
                if (upgrType == 1 && rangeCollider.radius >= baseRange * 2)
                    upgrType = 0;
                switch (upgrType)
                {
                    //special
                    case 0:
                        actionList[i] += () => UpgradeSpecial(rarity);
                        upgradesInfo.Add(upgrades[rarity]);
                        break;
                    //range
                    case 1:
                        actionList[i] += () => UpgradeRange(rangeUpgrades[rarity]);
                        upgradesInfo.Add(upgrades[rarity + 4]);
                        break;
                }
            }
            else
            {
                actionList[i] += () => UpgradeSpecial(rarity);
                upgradesInfo.Add(upgrades[rarity]);
            }


        }

        UpgradeManager.Instance.ShowUpgradeUI(actionList[0], actionList[1], actionList[2], upgradesInfo);

        UIToggableScript.Close();
    }
}
