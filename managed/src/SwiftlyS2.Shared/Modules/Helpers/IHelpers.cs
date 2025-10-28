using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Helpers;

public interface IHelpers
{
    /// <summary>
    /// Get weapon vdata from key.
    /// </summary>
    /// <param name="unknown">Not sure what this argument is for, but in general it's -1.</param>
    /// <param name="key">The key of the weapon (usually item idx).</param>
    /// <returns>The weapon vdata.</returns>
    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int unknown, string key);

    /// <summary>
    /// Get weapon vdata from item definition index.
    /// </summary>
    /// <param name="itemDefinitionIndex">The item definition index of the weapon.</param>
    /// <returns>The weapon vdata.</returns>
    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int itemDefinitionIndex);


}