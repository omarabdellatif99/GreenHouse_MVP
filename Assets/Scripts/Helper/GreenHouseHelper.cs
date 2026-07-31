using UnityEngine;

public static class GreenHouseHelper
{
    public static ConfigurationData configurationData { get; private set; }

    public static void Initialize(ConfigurationData configData)
    {
        configurationData = configData;
    }

    public static bool IsValueSatisfied(float value, CropAttributeType attributeType)
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

    
    public static bool IsCropbedActive(CropbedData data)
    {
        return (data.water >= configurationData.acitveWaterRange &&
            data.nutrients >= configurationData.acitveNutrientsRange &&
            data.energy >= configurationData.acitveEnergyRange &&
            data.climate >= configurationData.acitveClimateRange &&
            data.health >= configurationData.acitveHealthRange &&
            data.yieldProjection >= configurationData.acitveYieldProjectionRange);
    }

    
}