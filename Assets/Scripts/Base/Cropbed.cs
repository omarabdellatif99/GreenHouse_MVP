using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CropAttributeType
{
    Water, Nutrients, Energy, Climate, Health, Yield
}

[Serializable]
public class CropbedData

{
    public CropState state;
    public float water;
    public float nutrients;
    public float energy;
    public float climate;
    public float health;
    public float yieldProjection;
}

public enum CropState
{
    Active, Dead
}
public class Cropbed : Interactable
{
    public CropbedData data;
    // public ConfigurationData configurationData;

    public Material[] cropbedMats;

    public GameObject[] foliage;

    private MeshRenderer meshRenderer;

    private Dictionary<ModuleType, CropbedData> scannedModules = new();

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        GameEvents.OnCalculateCropbeds += CalculateData;
    }
    private void OnDisable()
    {
        GameEvents.OnCalculateCropbeds -= CalculateData;
    }

    private void Start()
    {
        CalculateData();
        RefreshState();
    }

    public override void Interact()
    {
        base.Interact();
        UIManager.Instance.ShowCropbedUI(data);
    }


    private void CalculateData()
    {
        foreach (Module module in GreenhouseManager.Instance.modules)
        {
            if (!IsValueChanged(module))
                continue;

            if (!GreenhouseManager.Instance.IsValueSatisfied(data.water, CropAttributeType.Water))
                data.water += module.water;
            if (!GreenhouseManager.Instance.IsValueSatisfied(data.nutrients, CropAttributeType.Nutrients))
                data.nutrients += module.nutrient;
            if (!GreenhouseManager.Instance.IsValueSatisfied(data.energy, CropAttributeType.Energy))
                data.energy += module.energy;
            if (!GreenhouseManager.Instance.IsValueSatisfied(data.climate, CropAttributeType.Climate))
                data.climate += module.climate;
            if (!GreenhouseManager.Instance.IsValueSatisfied(data.health, CropAttributeType.Health))
                data.health += module.health;
            if (!GreenhouseManager.Instance.IsValueSatisfied(data.yieldProjection, CropAttributeType.Yield))
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

        data.state = GreenhouseManager.Instance.IsCropbedActive(data) ? CropState.Active : CropState.Dead;
        RefreshState();
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


    private void RefreshState()
    {
        for (int i = 0; i < foliage.Length; i++)
        {
            foliage[i].transform.GetChild(0).gameObject.SetActive(data.state == CropState.Active);
            foliage[i].transform.GetChild(1).gameObject.SetActive(data.state == CropState.Dead);
        }
        meshRenderer.material = data.state == CropState.Active ? cropbedMats[0] : cropbedMats[1];
    }
}