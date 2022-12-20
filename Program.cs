using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // List<RollProb> rollProb = new List<RollProb>();
            // for (int i = 1; i < 3; ++i)
            // {
            //     Console.WriteLine("i: " + i);
            //     RollProb roll = new RollProb();
            //     roll.Probs = RollProb.Compute(i);
            //     RollProb.PrintVector(roll.Probs);
            //     Console.WriteLine(RollProb.SumVector(roll.Probs));
            //     Console.WriteLine();
            //     rollProb.Add(roll);
            // }

            // RollProb rolls = rollProb[0] + rollProb[1];
            // RollProb.PrintVector(rolls.Probs);
            // Console.WriteLine(RollProb.SumVector(rolls.Probs));

            Console.Write("Enter # of Spaces on Board: ");
            int boardSpaces = Console.Read();

            Console.Write("Enter # of rolls you'd like to look forward on: ");
            int numRolls = Console.Read();

            
        }
    }
}