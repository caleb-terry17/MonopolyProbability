using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<RollProb.BoardSpot> rolls2 = RollProb.Compute(2);
            double sum = 0;
            rolls2.ForEach(prob => 
            {
                sum += prob.Prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);

            List<RollProb.BoardSpot> rolls3 = RollProb.Compute(3);
            sum = 0;
            rolls3.ForEach(prob => 
            {
                sum += prob.Prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);
        }
    }
}