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

    protected virtual void Awake()
    {
        initLayer = gameObject.layer;
    }

    public virtual void SetHighlighted(bool value)
    {
        gameObject.layer = value ? outlineLayer : initLayer;
    }
    public virtual void Interact() {}
}