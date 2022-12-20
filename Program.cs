using Monopoly.Functionality;

namespace Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardProb board = new BoardProb(0, 40);
            List<BoardProb.BoardSpot> rolls2 = board.RollProb(2);
            double sum = 0;
            rolls2.ForEach(prob => 
            {
                sum += prob.Prob;
                Console.WriteLine(prob);
            });
            Console.WriteLine(sum);

            List<BoardProb.BoardSpot> rolls3 = board.RollProb(3);
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