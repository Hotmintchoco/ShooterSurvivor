using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Scriptable Object/AbilityData")]
public class AbilityData : ScriptableObject
{
    public enum AbilityType { Range, Divine, Atk, Shoe, Heal }

    [Header("# Main Info")]
    public AbilityType abilityType;
    public int abilityId;
    public string abilityName;
    public string abilityDesc;
    public Sprite abilityIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public float[] damages;
}
