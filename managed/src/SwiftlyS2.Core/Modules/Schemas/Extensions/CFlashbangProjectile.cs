using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Core.SchemaDefinitions;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CFlashbangProjectile
{
    /// <summary>
    /// Creates a flashbang grenade projectile.
    /// </summary>
    /// <param name="pos">The position where the flashbang grenade projectile will be created.</param>
    /// <param name="angle">The angle at which the flashbang grenade projectile will be created.</param>
    /// <param name="velocity">The velocity of the flashbang grenade projectile.</param>
    /// <param name="owner">The owner of the flashbang grenade projectile.</param>
    /// <returns>The created flashbang grenade projectile.</returns>
    public static CFlashbangProjectile EmitGrenade( Vector pos, QAngle angle, Vector velocity, CBasePlayerPawn? owner )
        => CFlashbangProjectileImpl.EmitGrenade(pos, angle, velocity, owner);
}