using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Services;

/// <summary>
/// Service for managing game-level operations and match data.
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Swaps the team scores between Terrorist and Counter-Terrorist.
    /// This includes first half, second half, overtime, and total scores.
    /// </summary>
    void SwapTeamScores();

    /// <summary>
    /// Updates the team score entities based on the provided match data.
    /// </summary>
    /// <param name="match">The match data structure containing updated scores.</param>
    void UpdateTeamScores( CCSMatch match );

    /// <summary>
    /// Gets the current match data from the game rules.
    /// </summary>
    CCSMatch MatchData { get; }
}