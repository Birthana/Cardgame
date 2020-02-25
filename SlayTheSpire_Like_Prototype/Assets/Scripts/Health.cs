using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private TextMeshPro healthText;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "" + currentHealth + " / " + maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "" + currentHealth + " / " + maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddBlock(int block)
    {
        currentHealth += block;
        healthText.text = "" + currentHealth + " / " + maxHealth;
    }
}
