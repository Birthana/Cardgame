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
            int leftoverDamage = damage - block;
            block = 0;
            currentHealth -= leftoverDamage;
        }
        else
        {
            block -= damage;
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
