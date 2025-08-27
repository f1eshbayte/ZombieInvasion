using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "Game/DoorData")]
public class DoorData : ScriptableObject
{
    public int MaxHealth = 100;
    public AudioClip damageSound; // Звук получения урона

    [System.Serializable]
    public class DoorStage
    {
        public int threshold; // HP <= threshold
        public Sprite sprite; // Спрайт для этой стадии
        public AudioClip thresholdSound; 
    }

    public List<DoorStage> stages;
    public AudioClip openSound;
    public AudioClip closeSound;
}