namespace Monopoly.Functionality
{
    public class DiceRoll 
    {
        /* P(X = x) = ?
         * P(X = 1) = 0/36
         * P(X = 2) = 1/36
         * P(X = 3) = 2/36
         * P(X = 4) = 3/36
         * P(X = 5) = 4/36
         * P(X = 6) = 5/36
         * P(X = 7) = 6/36
         * P(X = 8) = 5/36
         * P(X = 9) = 4/36
         * P(X = 10) = 3/36
         * P(X = 11) = 2/36
         * P(X = 12) = 1/36
         */
        static public double P(int x)
        {
            return (x < 8) ? (double)(x - 1) / 36 : (double)(13 - x) / 36;
        }
    }
}