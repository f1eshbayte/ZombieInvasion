using UnityEngine;
using Zenject;

public class DoorUpgradeButton : MonoBehaviour
{
    private DoorUpgradeManager _manager;

    [Inject]
    public void Construct(DoorUpgradeManager manager)
    {
        _manager = manager;
    }

    public void OnUpgradeButtonClicked()
    {
        _manager.UpgradeBothDoors();
    }
}
