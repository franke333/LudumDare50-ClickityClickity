using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSlotScript : MonoBehaviour
{
    public AbstractTower tower;
    public Canvas UpgradeCanvas;
    public SpriteRenderer slotSprite;
    public void BuyTower(AbstractTower tower)
    {
        GameManager gm = GameManager.Instance;
        if(gm.Money >= tower.cost)
        {
            UIToggableScript.Close();
            gm.Money -= tower.cost;
            var tGO=Instantiate(tower, transform.position, Quaternion.identity);
            tGO.slot = this;
            gameObject.GetComponent<UIToggableScript>().canvas = UpgradeCanvas;
            UpgradeCanvas.GetComponentInChildren<Button>().onClick.AddListener(() => tGO.Upgrade());
            transform.GetComponentInChildren<ConnectEnable>(includeInactive:true).objects.Add(tGO.RangeDisplayer.gameObject);
            var desc = GetComponent<DescriptionComponent>();
            var towerDesc = tGO.GetComponent<DescriptionComponent>();
            slotSprite.enabled = false;
            desc.Title = towerDesc.Title;
            desc.Description = towerDesc.Description;
            tGO.upgradeText = UpgradeCanvas.GetComponentInChildren<Text>();
        }
    }
}
