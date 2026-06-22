using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Settings")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI promptUI;
    private IInteractable currentInteractable;
    private RaycastHit currentHit;
    private Vector3 currentHitPoint;

    private void Update()
    {
        DetectInteractable();
        DetectInteractInput();
    }

    private void DetectInteractable()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != currentInteractable)
            {
                ClearCurrent();
                currentInteractable = interactable;
                currentHit = hit;
                currentHitPoint = hit.point;
                if (currentInteractable != null)
                {
                    currentInteractable.SetHighlighted(true);

                    promptUI.Show(
                        currentInteractable.InteractionPrompt,
                        hit.collider.transform,
                        currentInteractable.PromptOffset);
                }
            }
            currentHit = hit;
        }
        else
        {
            ClearCurrent();
        }
    }

    private void DetectInteractInput()
    {
        if (currentInteractable == null) return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void ClearCurrent()
    {
        if (currentInteractable != null)
        {
            currentInteractable.SetHighlighted(false);
            // currentInteractable.HidePrompt();
            currentInteractable = null;
        }
        promptUI.Hide();
    }

}