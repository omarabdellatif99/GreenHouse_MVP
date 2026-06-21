using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CropbedData
{
    public float water;
    public float nutrients;
    public float energy;
    public float climate;
    public float health;
    public float yieldProjection;
}
public class Cropbed : Interactable
{
    public CropbedData data;
    private Dictionary<ModuleType, CropbedData> scannedModules = new();


    private void OnEnable()
    {
        GameEvents.OnCalculateCropbeds += CalculateData;
    }
    private void OnDisable()
    {
        GameEvents.OnCalculateCropbeds -= CalculateData;
    }

    private void CalculateData(Module[] modules)
    {
        foreach (Module module in modules)
        {
            if (!IsValueChanged(module))
                continue;
            data.water += module.water;
            data.nutrients += module.nutrient;
            data.energy += module.energy;
            data.climate += module.climate;
            data.health += module.health;
            data.yieldProjection += module.yieldProjection;

            // Add this module to scanner
            scannedModules[module.moduleType] = new CropbedData
            {
                water = module.water,
                nutrients = module.nutrient,
                energy = module.energy,
                climate = module.climate,
                health = module.health,
                yieldProjection = module.yieldProjection
            };
        }
    }

    private bool IsValueChanged(Module module)
    {
        if (!scannedModules.ContainsKey(module.moduleType))
            return true;

        CropbedData data = scannedModules[module.moduleType];

        if (module.water != data.water)
            return true;
        
        if (module.nutrient != data.nutrients)
            return true;
            
        if (module.energy != data.energy)
            return true;
            
        if (module.climate != data.climate)
            return true;
            
        if (module.health != data.health)
            return true;
            
        if (module.yieldProjection != data.yieldProjection)
            return true;
        
        return false;
    }
}