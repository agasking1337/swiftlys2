using System.Runtime.InteropServices;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class GameService : IGameService
{
    private readonly IEntitySystemService entitySystemService;
    private static readonly Lazy<nint> MatchStructOffsetLazy = new(() => Schema.GetOffset(0x6295CF65D3F4FD4D) + 0x02, LazyThreadSafetyMode.None);
    private static nint MatchStructOffset => MatchStructOffsetLazy.Value;

    public GameService( IEntitySystemService entitySystemService )
    {
        this.entitySystemService = entitySystemService;
    }

    public CCSMatch MatchData => Marshal.PtrToStructure<CCSMatch>(GetMatchStructAddress());

    public void SwapTeamScores()
    {
        var match = MatchData;

        (match.TerroristScoreFirstHalf, match.CTScoreFirstHalf) = (match.CTScoreFirstHalf, match.TerroristScoreFirstHalf);
        (match.TerroristScoreSecondHalf, match.CTScoreSecondHalf) = (match.CTScoreSecondHalf, match.TerroristScoreSecondHalf);
        (match.TerroristScoreOvertime, match.CTScoreOvertime) = (match.CTScoreOvertime, match.TerroristScoreOvertime);
        (match.TerroristScoreTotal, match.CTScoreTotal) = (match.CTScoreTotal, match.TerroristScoreTotal);

        UpdateTeamScores(match);
        WriteMatchData(match);
    }

    public void UpdateTeamScores( CCSMatch match )
    {
        var teams = entitySystemService.GetAllEntitiesByDesignerName<CCSTeam>("cs_team_manager");

        foreach (var team in teams)
        {
            switch (team.TeamNum)
            {
                case 2: // Terrorist
                    UpdateTeamEntity(team, match.TerroristScoreTotal, match.TerroristScoreFirstHalf, match.TerroristScoreSecondHalf, match.TerroristScoreOvertime);
                    break;

                case 3: // Counter-Terrorist
                    UpdateTeamEntity(team, match.CTScoreTotal, match.CTScoreFirstHalf, match.CTScoreSecondHalf, match.CTScoreOvertime);
                    break;
            }
        }
    }

    private void WriteMatchData( CCSMatch match )
    {
        var structOffset = GetMatchStructAddress();
        Marshal.StructureToPtr(match, structOffset, true);
    }

    private nint GetMatchStructAddress()
    {
        var gameRules = entitySystemService.GetGameRules() ?? throw new InvalidOperationException("GameRules not found");
        return gameRules.Address + MatchStructOffset;
    }

    private static void UpdateTeamEntity( CCSTeam team, int totalScore, int firstHalfScore, int secondHalfScore, int overtimeScore )
    {
        team.Score = totalScore;
        team.ScoreFirstHalf = firstHalfScore;
        team.ScoreSecondHalf = secondHalfScore;
        team.ScoreOvertime = overtimeScore;
        team.ScoreUpdated();
        team.ScoreFirstHalfUpdated();
        team.ScoreSecondHalfUpdated();
        team.ScoreOvertimeUpdated();
    }
}