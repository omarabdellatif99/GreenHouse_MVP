using TMPro;
using UnityEngine;

public enum ModuleType
{
    Irrigation,
    Nutrient,
    Energy,
    ClimateControl
}
[CreateAssetMenu(menuName = "Scriptables/Module")]
public class Module : ScriptableObject
{
    public ModuleType moduleType;
    public float water;
    public float nutrient;
    public float energy;
    public float climate;
    public float health;
    public float yieldProjection;
}
