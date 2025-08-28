using UnityEngine;
using Zenject;

public class DoorUpgradeButton : MonoBehaviour
{
    private DoorService _service;

    [Inject]
    public void Construct(DoorService service)
    {
        _service = service;
    }

    public void OnUpgradeButtonClicked()
    {
        _service.UpgradeBothDoors();
    }
}
