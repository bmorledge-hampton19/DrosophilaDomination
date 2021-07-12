using System.Linq;
using System.Collections.Generic;

public class UpgradeDB : DataObjectOrganizer<Upgrade> {

    private Dictionary<GamePhase, Dictionary<UpgradeCategory, List<Upgrade>>> upgradesByCategory;


    override public void Awake() {

        base.Awake();

        initializeUpgradesByCategory();

    }

    private void initializeUpgradesByCategory() {

        upgradesByCategory = new Dictionary<GamePhase, Dictionary<UpgradeCategory, List<Upgrade>>>();
        foreach(GamePhase gamePhase in EnumHelper.GetEnumerable<GamePhase>()) {
            upgradesByCategory[gamePhase] = new Dictionary<UpgradeCategory, List<Upgrade>>();
            foreach(UpgradeCategory upgradeCategory in EnumHelper.GetEnumerable<UpgradeCategory>()) {
                upgradesByCategory[gamePhase][upgradeCategory] = new List<Upgrade>();
            }
        }

        foreach(GamePhase gamePhase in EnumHelper.GetEnumerable<GamePhase>()) {
            foreach(Upgrade upgrade in objectTiers[gamePhase]) {
                upgradesByCategory[gamePhase][upgrade.upgradeCategory].Add(upgrade);
            }
        }

    }

    public List<Upgrade> getUpgradesByCategory(UpgradeCategory upgradeCategory, bool? discovered = null) {

        var upgrades = upgradesByCategory[gameManager.getGamePhase()][upgradeCategory];

        if (discovered is null)
            return upgrades;
        else if ((bool)discovered)
            return upgrades.Where(upgrade => upgrade.isDiscovered()).ToList();
        else
            return upgrades.Where(upgrade => !upgrade.isDiscovered()).ToList();

    }

}