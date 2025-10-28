using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Helpers;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class HelpersService : IHelpers
{
    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int unknown, string key)
    {
        nint weaponDataPtr = GameFunctions.GetWeaponCSDataFromKey(unknown, key);
        if (weaponDataPtr == 0) {
            return null;
        }
        return new CCSWeaponBaseVDataImpl(weaponDataPtr);
    }

    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int itemDefinitionIndex)
    {
        return GetWeaponCSDataFromKey(-1, itemDefinitionIndex.ToString());
    }

}