using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "Game/DoorData")]
public class DoorData : ScriptableObject
{
    public int MaxHealth = 100;
    public AudioClip DamageSound; // Звук получения урона
    public int RepairPrice;

    [System.Serializable]
    public class DoorStage
    {
        public int Threshold; // HP <= threshold
        public Sprite Sprite; // Спрайт для этой стадии
        public AudioClip ThresholdSound;
    }

    public List<DoorStage> Stages;
    public AudioClip OpenSound;
    public AudioClip CloseSound;
}