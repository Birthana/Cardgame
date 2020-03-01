using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPortalStrength : BaseCardEffect
{
    public override void Resolve()
    {
        Player.instance.gameObject.GetComponent<PortalManager>().portalStrength += value;
    }
}
