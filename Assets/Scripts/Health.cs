using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int block;
    public TextMeshPro healthText;

    private void Start()
    {
        currentHealth = maxHealth;
        block = 0;
        healthText.text = "" + (currentHealth + block) + " / " + maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (block < damage)
        {
            block = 0;
            damage -= block;
            currentHealth -= damage;
        }
        else if (block >= damage)
        {
            block -= damage;
        }
        else
        {
            currentHealth -= damage;
        }
        healthText.text = "" + (currentHealth + block) + " / " + maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddBlock(int block)
    {
        this.block += block;
        healthText.text = "" + (currentHealth + this.block) + " / " + maxHealth;
    }

    public void EndTurn()
    {
        block = 0;
        healthText.text = "" + (currentHealth + this.block) + " / " + maxHealth;
    }
}
