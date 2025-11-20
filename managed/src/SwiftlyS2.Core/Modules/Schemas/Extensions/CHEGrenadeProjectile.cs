using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CHEGrenadeProjectile
{
    /// <summary>
    /// Creates a HE grenade projectile.
    /// </summary>
    /// <param name="pos">The position where the HE grenade projectile will be created.</param>
    /// <param name="angle">The angle at which the HE grenade projectile will be created.</param>
    /// <param name="velocity">The velocity of the HE grenade projectile.</param>
    /// <param name="owner">The owner of the HE grenade projectile.</param>
    /// <returns>The created HE grenade projectile.</returns>
    public static CHEGrenadeProjectile EmitGrenade( Vector pos, QAngle angle, Vector velocity, CBasePlayerPawn? owner )
        => CHEGrenadeProjectileImpl.EmitGrenade(pos, angle, velocity, owner);
}