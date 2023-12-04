using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCrance : InteractItem
{
    public CranceController Controller;
    public override void Interact(object data)
    {
        Controller.nextStepLevers();
    }
}
