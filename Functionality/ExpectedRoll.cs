namespace Monopoly.Functionality
{
    public class ExpectedRoll
    {
        // the expected value that any roll will yield
        public static double Ev()
        {
            return RollProb.SingleRollProb() 
                .Sum(spot => spot.TotalSpots * spot.Prob);
        }

        // the total expected number of rolls it's expected to take to get around the board
        // given the number of spaces on the board
        public static double Er(int spaces)
        {
            return (double)spaces / Ev();
        }
    }
}