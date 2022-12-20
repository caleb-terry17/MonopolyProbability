using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.Write("Enter # of Spaces on Board: ");
            // int boardSpaces = Console.Read();

            // Console.Write("Enter # of rolls you'd like to look forward on: ");
            // string line = Console.ReadLine();
            // int numRolls = Convert.ToInt32(line);
            // Console.WriteLine(numRolls);
            
            // RollProb prob = JointRollProb.GetProb(numRolls);
            // RollProb.PrintVector(prob.Probs);
            // Console.WriteLine(RollProb.SumVector(prob.Probs));

            RollProb prob1 = new RollProb(1);
            RollProb prob2 = new RollProb(2);
            RollProb prob3 = new RollProb(3);
            RollProb total = prob1 + prob2;
            RollProb.PrintVector(total.Probs);
            Console.WriteLine(RollProb.SumVector(total.Probs));
            RollProb.PrintVector(prob1.Probs);
            Console.WriteLine(RollProb.SumVector(prob1.Probs));
            RollProb.PrintVector(prob2.Probs);
            Console.WriteLine(RollProb.SumVector(prob2.Probs));
            // Console.WriteLine(RollProb.SumVector(prob3.Probs));
        }
    }
}