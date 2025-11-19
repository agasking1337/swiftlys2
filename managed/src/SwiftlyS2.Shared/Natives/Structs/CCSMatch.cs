using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CCSMatch
{
    public short TotalScore;
    public short ActualRoundsPlayed;
    public short NOvertimePlaying;
    public short CTScoreFirstHalf;
    public short CTScoreSecondHalf;
    public short CTScoreOvertime;
    public short CTScoreTotal;
    public short TerroristScoreFirstHalf;
    public short TerroristScoreSecondHalf;
    public short TerroristScoreOvertime;
    public short TerroristScoreTotal;
    public short Unknown;
    public int Phase;
}