using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RollProb board = new RollProb(0, 40);
            List<RollProb.BoardSpot> rolls2 = board.ComputeProb(2);
            double sum = 0;
            rolls2.ForEach(prob => 
            {
                sum += prob.Prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);

            List<RollProb.BoardSpot> rolls3 = board.ComputeProb(3);
            sum = 0;
            rolls2.ForEach(prob => 
            {
                sum += prob.Prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);
        }
    }
}