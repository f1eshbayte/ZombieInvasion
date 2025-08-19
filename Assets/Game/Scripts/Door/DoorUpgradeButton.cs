// using UnityEngine;
//
// public class DoorUpgradeButton : MonoBehaviour
// {
//     [SerializeField] private Door _leftDoor;
//     [SerializeField] private Door _rightDoor;
//
//     public void OnUpgradeButtonClicked()
//     {
//         if (_leftDoor != null)
//         {
//             _leftDoor.Upgrade();
//             Debug.Log("Левая дверь прокачана!");
//         }
//
//         if (_rightDoor != null)
//         {
//             _rightDoor.Upgrade();
//             Debug.Log("Правая дверь прокачана!");
//         }
//     }
// }
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
