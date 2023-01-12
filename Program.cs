using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // testing
            Console.WriteLine(ExpectedRoll.Ev());


            // testing


            // Console.Write("Enter # of Spaces on Board: ");
            // int boardSpaces = Console.Read();

            Console.Write("Enter # of rolls you'd like to look forward on: ");
            string? line = Console.ReadLine();
            int numRolls = Convert.ToInt32(line);
            
            RollProb prob = RollProb.GetJointProb(numRolls);
            RollProb.PrintVector(prob.Probs);
            Console.WriteLine(RollProb.SumVector(prob.Probs));
        }
    }
}