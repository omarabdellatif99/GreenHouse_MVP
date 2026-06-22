using UnityEngine;

public class ControllStation : Interactable
{
    public override void Interact()
    {
        base.Interact();
        UIManager.Instance.ShowControlStationUI();
    }
}