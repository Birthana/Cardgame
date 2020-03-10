using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This is the parent class for any effect that a card can have. The ApplyEffect() method is called
/// when the card the effect is a part of is being played. The context provided gives information
/// about what entity(ies) the card is being played on. See ActionContext for more detail.
public abstract class CardEffect : ScriptableObject
{
    public abstract void ApplyEffect(ActionContext context);
}
