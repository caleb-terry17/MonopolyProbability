using Monopoly.Constants;

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
        }
    }
}