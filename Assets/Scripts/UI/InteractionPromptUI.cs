using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private RectTransform root;
    [SerializeField] private TMP_Text promptText;
    [SerializeField] private Camera playerCamera;

    private Transform target;
    private Vector3 offset;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 screenPos = playerCamera.WorldToScreenPoint(target.position + offset);

        root.position = screenPos;
    }

    public void Show(string prompt, Transform targetTransform, Vector3 promptOffset)
    {
        target = targetTransform;
        offset = promptOffset;

        promptText.text = prompt;
        root.gameObject.SetActive(true);
    }

    public void Hide()
    {
        target = null;
        root.gameObject.SetActive(false);
    }
}