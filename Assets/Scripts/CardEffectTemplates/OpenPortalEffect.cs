using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// An effect which opens a portal when played.
[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Open Portal", fileName = "OpenPortal")]
public class OpenPortalEffect : CardEffect
{
    public Portal portalPrefab;

    public override IEnumerator ApplyEffect(ActionContext context)
    {
        GameObject instance = GameObject.Instantiate(portalPrefab.gameObject, Vector3.zero, Quaternion.identity);
        Portal portalInstance = instance.GetComponent<Portal>();
        BattleManager.instance.AddFriendlyPortal(portalInstance);
        yield return new WaitForSeconds(1f);
    }
}
