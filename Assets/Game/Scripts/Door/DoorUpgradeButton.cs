using UnityEngine;
using Zenject;

public class DoorUpgradeButton : MonoBehaviour
{
    private DoorUpgradeService _service;

    [Inject]
    public void Construct(DoorUpgradeService service)
    {
        _service = service;
    }

    public void OnUpgradeButtonClicked()
    {
        _service.UpgradeBothDoors();
    }
}
