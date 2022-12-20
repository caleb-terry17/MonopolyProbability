using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.Write("Enter # of Spaces on Board: ");
            // int boardSpaces = Console.Read();

            Console.Write("Enter # of rolls you'd like to look forward on: ");
            string line = Console.ReadLine();
            int numRolls = Convert.ToInt32(line);
            Console.WriteLine(numRolls);
            
            RollProb total = new RollProb(1);
            for (int roll = 2; roll < numRolls; ++roll)
            {
                total += new RollProb(roll);
            }
            RollProb.PrintVector(total.Probs);
            Console.WriteLine(RollProb.SumVector(total.Probs));
        }
    }
}