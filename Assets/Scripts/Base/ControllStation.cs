using UnityEngine;

public class ControllStation : Interactable
{
    private GreenhouseManager greenhouseManager;
    public bool ActionRequired = false;
    private bool ActionPerformed = false;
    public string ActionText;

    protected override void Awake()
    {
        greenhouseManager = GetComponentInParent<GreenhouseManager>();
    }
    public void SetActionPerformed(bool val)
    {
        ActionPerformed = val;
    }
    public override void Interact()
    {
        if (ActionRequired && !ActionPerformed)
        {
            UIManager.Instance.ShowAlertMessage(ActionText);
            return;
        }
        base.Interact();
        UIManager.Instance.ShowControlStationUI(greenhouseManager);
    }
}