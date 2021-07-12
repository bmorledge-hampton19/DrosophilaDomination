using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeReadout : MonoBehaviour
{

    public Text title;
    public Text description;
    public Text cost;
    public Button purchaseButton;

    public void initialize(Upgrade upgrade, UpgradeManager upgradeManager) {

        title.text = upgrade.objectName;
        description.text = upgrade.description;

        cost.text = "";
        foreach (PlayerResource playerResource in upgrade.resourceCosts.Keys) {

            if (playerResource == PlayerResource.money) {
                cost.text += (upgrade.resourceCosts[playerResource].ToString("C2") + ", ");
            } else {
                cost.text += (upgrade.resourceCosts[playerResource] + " " + EnumHelper.GetDescription(playerResource) + ", ");
            }

        }
        cost.text = cost.text.Remove(cost.text.Length-2);

        purchaseButton.onClick.AddListener(delegate() {if (upgradeManager.purchaseUpgrade(upgrade)) {disablePurchaseButton();} } );

    }

    private void disablePurchaseButton() {
        purchaseButton.interactable = false;
    }

}
