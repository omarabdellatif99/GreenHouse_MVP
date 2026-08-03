using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string interactionPrompt = "[E] Use";
    [SerializeField] private Vector3 promptOffset;

    private int outlineLayer = 6;
    private int initLayer;

    public string InteractionPrompt => interactionPrompt;

    public Vector3 PromptOffset => promptOffset;
    [SerializeField] protected WebGLOutline outline;

    protected virtual void Awake()
    {
        initLayer = gameObject.layer;
    }

    // void Start()
    // {
    //     TryGetComponent(out outline);
    //     if (!outline)
    //         outline = GetComponentInChildren<WebGLOutline>();
    //     if (!outline)
    //         outline = GetComponentInParent<WebGLOutline>();
    // }

    public virtual void SetHighlighted(bool value)
    {
        // gameObject.layer = value ? outlineLayer : initLayer;
        outline?.EnableOutline(value);
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).gameObject.layer = value ? outlineLayer : initLayer;
        // }
    }
    public virtual void Interact()
    {
        AudioManager.Instance.PlaySFX();
    }
}