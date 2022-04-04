using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CatapultScript : AbstractTower
{
    public float fireDelay;
    public CatapultBall bulletPrefab;
    private float timeRemainingBeforeNextFire;
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
    float[] rangeUpgrades = new float[] { 0.2f, 0.4f };
    float[] delayUpgrades = new float[] { 0.1f, 0.2f };

    private List<CrabScript> crabsInRange = new List<CrabScript>();
    void Start()
    {
        timeRemainingBeforeNextFire = fireDelay;
        gm = GameManager.Instance;
        colliderToRangeDisplayRatio = rangeCollider.radius / RangeDisplayer.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemainingBeforeNextFire -= Time.deltaTime * gm.GameSpeed;
        if (timeRemainingBeforeNextFire < 0)
            Fire();
    }

    void Fire()
    {
        var target = ChooseTarget();
        if (target is null)
            return;
        SoundManager.Instance.PlayShoot();
        timeRemainingBeforeNextFire = fireDelay;
        int bullets = 1 + specificUpgrades[3];
        for (int i = 0; i < bullets; i++)
        {
            target = ChooseTarget();
            CatapultBall bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.target = transform.position + UnityEngine.Random.Range(0.75f,1.05f)*(target.transform.position-transform.position);
            bullet.dmg = 1 + specificUpgrades[1] + specificUpgrades[2];
            bullet.range = 1f + 0.25f * (specificUpgrades[0] + specificUpgrades[2]);
        }

    }

    public void UpgradeRange(float baseRatio)
    {
        rangeCollider.radius += baseRange * baseRatio;
        RangeDisplayer.transform.localScale = new Vector3(rangeCollider.radius / colliderToRangeDisplayRatio,
            rangeCollider.radius / colliderToRangeDisplayRatio);
        upgradeCost *= 2;
        upgradeText.text = $"Upgrade: {upgradeCost}$";
    }

    public void UpgradeDelay(float reduceByPercent)
    {
        fireDelay *= (1 - reduceByPercent);
        upgradeCost *= 2;
        upgradeText.text = $"Upgrade: {upgradeCost}$";
    }

    public void UpgradeSpecial(int rarity)
    {
        specificUpgrades[rarity]++;
        upgradeCost *= 2;

        upgradeText.text = $"Upgrade: {upgradeCost}$";
    }

    private CrabScript ChooseTarget()
    {
        //if no crab in range, return null
        if (crabsInRange.Count == 0)
            return null;
        //if random crab is dead, clean dead crabs from list
        int crabIndex = UnityEngine.Random.Range(0, crabsInRange.Count);
        if (crabsInRange[crabIndex] is null)
            crabsInRange.RemoveAll(x => x is null);
        else
            return crabsInRange[crabIndex];
        //if all crabs died, return null
        if (crabsInRange.Count == 0)
            return null;

        crabIndex = UnityEngine.Random.Range(0, crabsInRange.Count);
        //otherwise return oldest crab
        return crabsInRange[crabIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
            crabsInRange.Add(cs);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CrabScript>(out CrabScript cs))
            crabsInRange.Remove(cs);
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
                int upgrType = UnityEngine.Random.Range(0, 3);
                if (upgrType == 1 && rangeCollider.radius >= baseRange * 2)
                    upgrType = UnityEngine.Random.Range(0, 2) == 0 ? 0 : 2;
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
                    //delay
                    case 2:
                        actionList[i] += () => UpgradeDelay(delayUpgrades[rarity]);
                        upgradesInfo.Add(upgrades[rarity + 6]);
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

