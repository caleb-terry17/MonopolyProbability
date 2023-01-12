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
    }
}