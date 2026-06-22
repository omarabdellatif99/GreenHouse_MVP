using UnityEngine;

[System.Serializable]
public class GreenhouseState
{
    public float water;
    public float nutrients;
    public float energy;
    public float climate;
    public float health;
    public float yieldProjection;
}

public class GreenhouseManager : MonoBehaviour
{
    public static GreenhouseManager Instance;
    public ConfigurationData configurationData;
    public GreenhouseState currentState;
    public Module[] modules;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RandomizeModules();
        RefreshCurrentState();
    }

    private void RandomizeModules()
    {
        foreach (var module in modules)
        {
            switch (module.moduleType)
            {
                case ModuleType.Irrigation:
                    module.water = Random.Range(configurationData.initWaterRandomization.x, configurationData.initWaterRandomization.y);
                    break;
                case ModuleType.Nutrient:
                    module.nutrient = Random.Range(configurationData.initNutrientRandomization.x, configurationData.initNutrientRandomization.y);
                    break;
                case ModuleType.Energy:
                    module.energy = Random.Range(configurationData.initEnergyRandomization.x, configurationData.initEnergyRandomization.y);
                    break;
                case ModuleType.ClimateControl:
                    module.climate = Random.Range(configurationData.initClimateRandomization.x, configurationData.initClimateRandomization.y);
                    break;
            }
            module.health = Random.Range(configurationData.initHealthRandomization.x, configurationData.initHealthRandomization.y);
            module.yieldProjection = Random.Range(configurationData.initYieldRandomization.x, configurationData.initYieldRandomization.y);
        }
    }


    public void RefreshCurrentState()
    {
        currentState = new GreenhouseState();
        foreach (var module in modules)
        {
            currentState.water += module.water;
            currentState.nutrients += module.nutrient;
            currentState.energy += module.energy;
            currentState.climate += module.climate;
            currentState.health += module.health;
            currentState.yieldProjection += module.yieldProjection;
        }
    }


    public bool IsValueSatisfied(float value, CropAttributeType attributeType)
    {
        switch (attributeType)
        {
            case CropAttributeType.Water:
                return value >= configurationData.acitveWaterRange;
            case CropAttributeType.Nutrients:
                return value >= configurationData.acitveNutrientsRange;
            case CropAttributeType.Energy:
                return value >= configurationData.acitveEnergyRange;
            case CropAttributeType.Climate:
                return value >= configurationData.acitveClimateRange;
            case CropAttributeType.Health:
                return value >= configurationData.acitveHealthRange;
            case CropAttributeType.Yield:
                return value >= configurationData.acitveYieldProjectionRange;
        }
        return false;
    }

    public bool IsCropbedActive(CropbedData data)
    {
        return (data.water >= configurationData.acitveWaterRange &&
            data.nutrients >= configurationData.acitveNutrientsRange &&
            data.energy >= configurationData.acitveEnergyRange &&
            data.climate >= configurationData.acitveClimateRange &&
            data.health >= configurationData.acitveHealthRange &&
            data.yieldProjection >= configurationData.acitveYieldProjectionRange);
    }

    public bool IsModuleOptimized(ModuleType cropAttributeType)
    {
        switch (cropAttributeType)
        {
            case ModuleType.Irrigation:
                return currentState.water >= configurationData.acitveWaterRange;
            case ModuleType.Nutrient:
                return currentState.nutrients >= configurationData.acitveNutrientsRange;
            case ModuleType.Energy:
                return currentState.energy >= configurationData.acitveEnergyRange;
            case ModuleType.ClimateControl:
                return currentState.climate >= configurationData.acitveClimateRange;
            case ModuleType.Health:
                return currentState.health >= configurationData.acitveHealthRange;
            case ModuleType.YieldProjection:
                return currentState.yieldProjection >= configurationData.acitveYieldProjectionRange;
        }
        return false;
    }

    public void OptimizeModule(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Irrigation:
                GetModule(ModuleType.Irrigation).water = configurationData.acitveWaterRange;
                break;
            case ModuleType.Nutrient:
                GetModule(ModuleType.Nutrient).nutrient = configurationData.acitveNutrientsRange;
                break;
            case ModuleType.Energy:
                GetModule(ModuleType.Energy).energy = configurationData.acitveEnergyRange;
                break;
            case ModuleType.ClimateControl:
                GetModule(ModuleType.ClimateControl).climate = configurationData.acitveClimateRange;
                break;
            case ModuleType.Health:
                SetUniversalAttribute(CropAttributeType.Health);
                break;
            case ModuleType.YieldProjection:
                SetUniversalAttribute(CropAttributeType.Yield);
                break;
        }
        GameEvents.OnCalculateCropbeds?.Invoke();
        RefreshCurrentState();
    }

    public float GetModuleValue(ModuleType moduleType)
    {
        switch (moduleType)
        {
            case ModuleType.Irrigation:
                return currentState.water;
            case ModuleType.Nutrient:
                return currentState.nutrients;
            case ModuleType.Energy:
                return currentState.energy;
            case ModuleType.ClimateControl:
                return currentState.climate;
            case ModuleType.Health:
                return currentState.health;
            case ModuleType.YieldProjection:
                return currentState.yieldProjection;
        }
        return 0f;
    }

    private Module GetModule(ModuleType moduleType)
    {
        foreach (var module in modules)
        {
            if (module.moduleType == moduleType)
                return module;
        }
        return null;
    }

    private void SetUniversalAttribute(CropAttributeType attributeType)
    {
        float reqSatisfiedVal = attributeType == CropAttributeType.Health ?
            configurationData.acitveHealthRange :
            configurationData.acitveYieldProjectionRange;

        float valueToPutIn = reqSatisfiedVal / (float)modules.Length;
        Debug.Log($"reqSatisfiedVal: {reqSatisfiedVal} & modules.Length: {(float)modules.Length}");
        Debug.Log($"Value to put in: {valueToPutIn}");

        foreach (var module in modules)
        {
            if (attributeType == CropAttributeType.Health)
            {
                module.health = valueToPutIn;
            }
            else if (attributeType == CropAttributeType.Yield)
            {
                module.yieldProjection = valueToPutIn;
            }
        }
    }

}