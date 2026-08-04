using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Prompt")]
    [SerializeField] private string interactionPrompt = "[E] Use";
    [SerializeField] private Vector3 promptOffset;

    private int outlineLayer = 6;
    private int initLayer = 7;

    public string InteractionPrompt => interactionPrompt;

    public Vector3 PromptOffset => promptOffset;
    [SerializeField] protected WebGLOutline outline;
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    protected virtual void Awake() { }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        // TryGetComponent(out outline);
        // if (!outline)
        //     outline = GetComponentInChildren<WebGLOutline>();
        // if (!outline)
        //     outline = GetComponentInParent<WebGLOutline>();
        Initialize();
    }

    protected virtual void Initialize()
    {
        Debug.Log("Mesh renderers filling in: " + gameObject.name);
        meshRenderers.AddRange(GetComponentsInChildren<MeshRenderer>(true));
    }

    public virtual void SetHighlighted(bool value)
    {
        // outline?.EnableOutline(value);
        // gameObject.layer = value ? outlineLayer : initLayer;
        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].gameObject.layer = value ? outlineLayer : initLayer;
        }
    }
    public virtual void Interact()
    {
        AudioManager.Instance.PlaySFX();
    }
}