using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ReuseSystem;
using UnityEngine;

public class BasePopUp : UICanvas
{
    [SerializeField] private RectTransform mainPopUP;
    [SerializeField] private float timeScale;

    public override void Open()
    {
        base.Open();
        mainPopUP.localScale = Vector3.zero;
        mainPopUP.DOScale(1f, timeScale);
    }
}
