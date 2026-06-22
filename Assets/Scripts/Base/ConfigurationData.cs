using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Configuration")]
public class ConfigurationData : ScriptableObject
{
    [Tooltip("Water value at which this crop value will be considered satisfied")]
    public float acitveWaterRange;
    [Tooltip("Nutrients value at which this crop value will be considered satisfied")]
    public float acitveNutrientsRange;
    [Tooltip("Energy value at which this crop value will be considered satisfied")]
    public float acitveEnergyRange;
    [Tooltip("Climate value at which this crop value will be considered satisfied")]
    public float acitveClimateRange;
    [Tooltip("Health value at which this crop value will be considered satisfied")]
    public float acitveHealthRange;
    [Tooltip("Yield Projection value at which this crop value will be considered satisfied")]
    public float acitveYieldProjectionRange;


    [Space]
    public Vector2 initWaterRandomization;
    public Vector2 initNutrientRandomization;
    public Vector2 initEnergyRandomization;
    public Vector2 initClimateRandomization;
    public Vector2 initHealthRandomization;
    public Vector2 initYieldRandomization;
}