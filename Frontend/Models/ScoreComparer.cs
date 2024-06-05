using System.Collections;

namespace Frontend.Models
{
    public class ScoreComparer : IComparer<LeaderboardPosition>
    {

        public int Compare(LeaderboardPosition? x, LeaderboardPosition? y)
        {
            if (x.Score > y.Score) return 1;
            if (x.Score < y.Score) return -1;
            return 0;
        }
    }
}
