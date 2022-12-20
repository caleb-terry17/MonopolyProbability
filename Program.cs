using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 12; ++i) 
            {
                double x = DiceRoll.P(i);
                Console.WriteLine("{0:0.00}", x);
            }

            BoardProb board = new BoardProb(0, 40);
            List<double> rolls2 = board.RollProb(2);
            double sum = 0;
            rolls2.ForEach(prob => 
            {
                sum += prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);
        }
    }
}