namespace Monopoly.Functionality
{
    public class JointRollProb
    {
        public static RollProb GetProb(int rolls)
        {
            RollProb total = new RollProb(1);

            for (int roll = 2; roll <= rolls; ++roll)
            {
                total += new RollProb(roll);
            }

            total.Probs.ForEach((spot) =>
            {
                spot.Prob /= rolls;
            });

            return total;
        }
    }
}