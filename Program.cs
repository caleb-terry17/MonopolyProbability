using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 5; ++i)
            {
                Console.WriteLine("i: " + i);
                List<RollProb.BoardSpot> rolls = RollProb.Compute(i);
                RollProb.PrintVector(rolls);
                Console.WriteLine(RollProb.SumVector(rolls));
                Console.WriteLine();
            }
        }
    }
}