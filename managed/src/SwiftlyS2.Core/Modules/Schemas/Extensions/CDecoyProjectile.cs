using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CDecoyProjectile
{
    /// <summary>
    /// Creates a decoy grenade projectile.
    /// </summary>
    /// <param name="pos">The position where the decoy grenade projectile will be created.</param>
    /// <param name="angle">The angle at which the decoy grenade projectile will be created.</param>
    /// <param name="velocity">The velocity of the decoy grenade projectile.</param>
    /// <param name="owner">The owner of the decoy grenade projectile.</param>
    /// <returns>The created decoy grenade projectile.</returns>
    public static CDecoyProjectile EmitGrenade( Vector pos, QAngle angle, Vector velocity, CBasePlayerPawn? owner )
        => CDecoyProjectileImpl.EmitGrenade(pos, angle, velocity, owner);
}