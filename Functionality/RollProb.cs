namespace Monopoly.Functionality
{
    // Computes the probability for each possible number of spaces from the current position
    public class RollProb
    {
        public class Spot
        {
            public int TotalSpots { get; set; }  // total # of spots from start pos
            public double Prob { get; set; }  // the probability of landing in the current spot

            public Spot(int totalSpots, double prob = 0)
            {
                this.TotalSpots = totalSpots;
                this.Prob = prob;
            }

            public override string ToString()
            {
                return "(" + this.TotalSpots.ToString() + "," + Math.Round(this.Prob, 4).ToString() + ")";
            }

            public static Spot operator *(Spot a, Spot b)
            {
                return new Spot(a.TotalSpots + b.TotalSpots, a.Prob * b.Prob);
            }
        }
        
        public List<Spot> Probs;
        private static int LOWEST_ROLL = 2;
        private static int HIGHEST_ROLL = 12;

        public RollProb() 
        {
            Probs = new List<Spot>();
        }

        public static RollProb operator +(RollProb a, RollProb b)
        {
            RollProb c = new RollProb();

            int aPtr = 0;
            int bPtr = 0;
            
            while (aPtr < a.Probs.Count() || bPtr < b.Probs.Count())
            {
                if (aPtr >= a.Probs.Count())
                {
                    c.Probs.Add(b.Probs[bPtr]);
                    bPtr++;
                }
                else if (bPtr >= a.Probs.Count())
                {
                    c.Probs.Add(a.Probs[aPtr]);
                    aPtr++;
                }
                else if (a.Probs[aPtr].TotalSpots < b.Probs[bPtr].TotalSpots)
                {
                    c.Probs.Add(a.Probs[aPtr]);
                    aPtr++;
                }
                else if (a.Probs[aPtr].TotalSpots > b.Probs[bPtr].TotalSpots)
                {
                    c.Probs.Add(b.Probs[bPtr]);
                    bPtr++;
                }
                else
                {
                    Spot spot = new Spot(a.Probs[aPtr].TotalSpots, a.Probs[aPtr].Prob + b.Probs[bPtr].Prob);
                    c.Probs.Add(spot);
                    aPtr++;
                    bPtr++;
                }
            }

            c.Probs.ForEach((spot) =>
            {
                spot.Prob /= 2;
            });

            return c;
        }

        // probability to land on the next x spots given a # of rolls
        // the current position is referred to as "spot 0"
        public static List<Spot> Compute(int roll)
        {
            // initial matrix
            List<List<Spot>> matrix = NewMatrix(HIGHEST_ROLL - LOWEST_ROLL + 1);

            // initial vector
            List<Spot> vec = new List<Spot>();
            // initialize with initial probabilities
            for (int i = LOWEST_ROLL; i <= HIGHEST_ROLL; ++i) 
            {
                vec.Add(new Spot(i, DiceRoll.P(i)));
            }

            // just return vector of probabilities
            if (roll == 1) 
            {
                return vec;
            }

            for (int iter = 1; iter < roll; ++iter)
            {
                // compute possibilities for roll iter + 1
                MatrixMult(ref matrix, vec);

                // create new vector and store probabilities in it 
                vec = InitVector(matrix);

                // create new matrix for next iter
                matrix = NewMatrix(vec.Count());
            }

            // probabilities are stored in vec list => return that
            return vec;
        }

        public static void PrintVector(List<Spot> vec)
        {
            vec.ForEach((spot) =>
            {
                Console.WriteLine(spot.ToString() + " ");
            });
        }

        public static double SumVector(List<Spot> vec)
        {
            double sum = 0;

            vec.ForEach((spot) =>
            {
                sum += spot.Prob;
            });

            return sum;
        }

        private static List<List<Spot>> NewMatrix(int cols)
        {
            List<List<Spot>> board = new List<List<Spot>>();

            for (int row = LOWEST_ROLL; row <= HIGHEST_ROLL; ++row) 
            {
                board.Add(new List<Spot>());
                for (int col = 0; col < cols; ++col)
                {
                    board[row - LOWEST_ROLL].Add(new Spot(row, DiceRoll.P(row)));
                }
            }

            return board;
        }

        private static void MatrixMult(ref List<List<Spot>> matrix, List<Spot> vec) 
        {
            for (int row = 0; row < matrix.Count(); ++row) 
            {
                for (int col = 0; col < matrix[row].Count(); ++col)
                {
                    matrix[row][col] *= vec[col];
                }
            }
        }

        private static void PrintMatrix(List<List<Spot>> matrix)
        {
            matrix.ForEach((row) => 
            {
                row.ForEach((spot) =>
                {
                    Console.Write(spot.ToString() + " ");
                });
                Console.Write("\n");
            });
        }

        private static double SumMatrix(List<List<Spot>> matrix) 
        {
            double sum = 0;

            matrix.ForEach((row) =>
            {
                row.ForEach((spot) =>
                {
                    sum += spot.Prob;
                });
            });

            return sum;
        }

        private static List<Spot> InitVector(List<List<Spot>> matrix)
        {
            List<Spot> vec = new List<Spot>();

            int rows = matrix.Count();
            int cols = matrix[0].Count();

            int minSpots = matrix[0][0].TotalSpots;
            int maxSpots = matrix[rows - 1][cols - 1].TotalSpots;

            for (int row = minSpots; row <= maxSpots; ++row)
            {
                vec.Add(new Spot(row));
            }

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    Spot spot = matrix[row][col];
                    vec[spot.TotalSpots - minSpots].Prob += spot.Prob;
                }
            }

            return vec;
        }
    }
}