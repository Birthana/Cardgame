public class DealDamage : BaseCardEffect
{
    public override void Resolve()
    {
        //Debug.Log("Deal " + damage + " Damage");
        target.GetComponent<Health>().TakeDamage(value);
    }
}
