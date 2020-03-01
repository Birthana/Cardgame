public class DealDamagePlusPortalStrength : BaseCardEffect
{
    public override void Resolve()
    {
        target.GetComponent<Health>().TakeDamage(value + Player.instance.gameObject.GetComponent<PortalManager>().portalStrength);
    }
}
