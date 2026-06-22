using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using StarterAssets;


[Serializable]
public class ControlStationUI
{
    public ModuleType moduleType;
    public TextMeshProUGUI valueTxt;
    public GameObject optimizeButton;
}

[Serializable]
public class CropbedUI
{
    public CropAttributeType attributeType;
    public TextMeshProUGUI valueTxt;
    public TextMeshProUGUI satisfiedText;
}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("References")]
    [SerializeField] private CanvasGroup controlStationUI;
    [SerializeField] private CanvasGroup cropbedUI;

    [Space]
    [Header("Control Station UI")]
    public ControlStationUI[] moduleStatsUI;

    [Space]
    [Header("Crop Bed UI")]
    public CropbedUI[] cropbedStatsUI;

    public bool IsUIOpened { get; private set; } = false;
    private StarterAssetsInputs inputs;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        inputs = FindAnyObjectByType<StarterAssetsInputs>();
    }

    public void ShowControlStationUI()
    {
        controlStationUI.gameObject.SetActive(true);
        controlStationUI.DOFade(1.0f, 0.3f);
        UpdateControlStationUI();

        inputs.SetCursorState(false);
        inputs.cursorInputForLook = false;
        IsUIOpened = true;
    }

    public void UpdateControlStationUI()
    {
        foreach (var moduleUI in moduleStatsUI)
        {
            moduleUI.valueTxt.text = GreenhouseManager.Instance.GetModuleValue(moduleUI.moduleType).ToString("F1");
            moduleUI.optimizeButton.SetActive(!GreenhouseManager.Instance.IsModuleOptimized(moduleUI.moduleType));
        }
    }

    public void HideControlStationUI()
    {
        IsUIOpened = false;
        inputs.cursorInputForLook = true;
        controlStationUI.DOFade(0.0f, 0.3f)
        .OnComplete(() =>
        {
            controlStationUI.gameObject.SetActive(false);
        });
    }


    public void ShowCropbedUI(CropbedData data)
    {
        cropbedUI.gameObject.SetActive(true);
        cropbedUI.DOFade(1.0f, 0.3f);

        foreach (var cropbedUI in cropbedStatsUI)
        {
            float value = 0f;
            switch (cropbedUI.attributeType)
            {
                case CropAttributeType.Water:
                    value = data.water;
                    break;
                case CropAttributeType.Energy:
                    value = data.energy;
                    break;
                case CropAttributeType.Nutrients:
                    value = data.nutrients;
                    break;
                case CropAttributeType.Climate:
                    value = data.climate;
                    break;
                case CropAttributeType.Health:
                    value = data.health;
                    break;
                case CropAttributeType.Yield:
                    value = data.yieldProjection;
                    break;
            }
            cropbedUI.valueTxt.text = value.ToString("F1");
            cropbedUI.satisfiedText.text = GreenhouseManager.Instance.IsValueSatisfied(value, cropbedUI.attributeType) ? "Satisfied" : "Not Satisfied";
            cropbedUI.satisfiedText.color = GreenhouseManager.Instance.IsValueSatisfied(value, cropbedUI.attributeType) ? Color.green : Color.red;
        }
        inputs.SetCursorState(false);
        inputs.cursorInputForLook = false;
        IsUIOpened = true;
    }

    public void HideCropbedUI()
    {
        IsUIOpened = false;
        inputs.cursorInputForLook = true;
        cropbedUI.DOFade(0.0f, 0.3f)
        .OnComplete(() =>
        {
            cropbedUI.gameObject.SetActive(false);
        });
    }

    public void OptimizeButtonCallback(string module)
    {
        ModuleType moduleType = (ModuleType)Enum.Parse(typeof(ModuleType), module);
        GreenhouseManager.Instance.OptimizeModule(moduleType);
        UpdateControlStationUI();
    }
}