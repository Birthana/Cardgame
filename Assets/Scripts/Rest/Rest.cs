using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public int percentToHeal;     //Amount of maxHealth to heal.

    private int amountToHeal;

    public void RestPlayer()
    {
        float actualPercent = percentToHeal * 0.01f;
        //amountToHeal = Mathf.Min(Mathf.RoundToInt(Player.instance.maxHealth * percentToHeal), Player.instance.maxHealth);
        //Player.instance.AddHealth(amountToHeal);
        //Debug.Log(Mathf.RoundToInt(80 * actualPercent));
    }
}
