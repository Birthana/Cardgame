using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/how-to-create-your-own-c-script-template.459977/#post-5365599
public class CallPower : Card
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AddToIndex()
    {
        // Add an instance of this card to the card index so that it shows up in the game.
        CardIndex.instance.Add(ScriptableObject.CreateInstance<CallPower>());
    }

    public CallPower()
    {
        title = "Call Power"; // Name of the card.
        level = 3; // Amount of energy required to play the card.
        effectText = "Opens a portal.\nIf a portal is already open, its strength is increased."; // Text shown on the lower half of the card.
        targetMode = TargetMode.Player; // What the card should target (player, enemy, all enemies, etc.)
    }

    /// This is called when the card is played. Use it to apply the effects of the card. The
    /// variable "context" contains many useful fields and functions, view the documentation for
    /// ActionContext to find out about them.
    protected override IEnumerator Play(ActionContext context)
    {
        if (BattleManager.instance.friendlyPortal == null)
        {
            Portal portal = (Instantiate(Resources.Load("Portals/VoidPortal")) as GameObject).GetComponent<Portal>();
            BattleManager.instance.SetFriendlyPortal(portal);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return BattleManager.instance.friendlyPortal.Upgrade();
        }
    }
}