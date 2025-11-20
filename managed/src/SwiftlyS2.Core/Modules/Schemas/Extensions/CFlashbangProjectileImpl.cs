using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CFlashbangProjectileImpl : CFlashbangProjectile
{
    public static CFlashbangProjectile EmitGrenade( Vector pos, QAngle angle, Vector velocity, CBasePlayerPawn? owner )
    {
        return new CFlashbangProjectileImpl(GameFunctions.CFlashbangProjectile_EmitGrenade(pos, angle, velocity, owner?.Address ?? nint.Zero, (uint)HelpersService.WeaponItemDefinitionIndices["weapon_decoy"]));
    }
}