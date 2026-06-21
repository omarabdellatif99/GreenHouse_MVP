using UnityEngine;
public interface IInteractable
{
    string InteractionPrompt { get; }
    Vector3 PromptOffset { get; }
    void SetHighlighted(bool value);
    void Interact();
}