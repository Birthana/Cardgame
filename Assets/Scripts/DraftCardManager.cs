using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftCardManager : MonoBehaviour
{

    public static DraftCardManager instance = null;
    public List<Card> cards;
    public int chooseFromNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Draft()
    {
        int[] rngIndex = GetRandomNumbers(chooseFromNumber);
        for (int i = 0; i < rngIndex.Length; i++)
        {
            Instantiate(cards[rngIndex[i]], this.transform.position, Quaternion.identity);
        }
    }

    public int[] GetRandomNumbers(int chooseFromNumber)
    {
        int[] rngIndex = new int[chooseFromNumber];
        int count = 0;
        while (count < chooseFromNumber)
        {
            int tempIndex = Random.Range(0, cards.Count);
            if (CheckUniqueness(rngIndex, tempIndex))
            {
                rngIndex[count] = tempIndex;
                count++;
            }
        }
        return rngIndex;
    }

    public bool CheckUniqueness(int[] rngIndex, int tempIndex)
    {
        bool unique = true;
        foreach (int index in rngIndex)
        {
            if (index.Equals(tempIndex))
            {
                unique = false;
            }
        }
        return unique;
    }
}
