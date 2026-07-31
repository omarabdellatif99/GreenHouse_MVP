using DG.Tweening;
using UnityEngine;

public class WellController : Interactable
{
    [SerializeField] private Transform handleT;
    [SerializeField] private ControllStation controlStation;
    public override void Interact()
    {
        StartWell();
        gameObject.layer = 0;
        outline.EnableOutline(false);
    }

    public void StartWell()
    {
        handleT.DORotate(new Vector3(360f, 0f, 0f), 2.0f, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        controlStation.SetActionPerformed(true);
    }
}