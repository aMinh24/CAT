using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : BaseUIElement
{
    public override void Hide()
    {
        base.Hide();
        uiType = UIType.Popup;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
}
