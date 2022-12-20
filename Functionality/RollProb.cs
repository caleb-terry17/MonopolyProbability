namespace Monopoly.Functionality
{
    // Computes the probability for each possible number of spaces from the current position
    public class RollProb
    {
        public class BoardSpot
        {
            public int TotalSpots { get; set; }  // total # of spots from start pos
            public double Prob { get; set; }  // the probability of landing in the current spot

            public BoardSpot(int totalSpots, double prob = 0)
            {
                this.TotalSpots = totalSpots;
                this.Prob = prob;
            }

            public override string ToString()
            {
                return "(" + this.TotalSpots.ToString() + "," + Math.Round(this.Prob, 4).ToString() + ")";
            }

            public static BoardSpot operator *(BoardSpot a, BoardSpot b)
            {
                return new BoardSpot(a.TotalSpots + b.TotalSpots, a.Prob * b.Prob);
            }
        }
        
        private static int LOWEST_ROLL = 2;
        private static int HIGHEST_ROLL = 12;

        // probability to land on the next x spots given a # of rolls
        // the current position is referred to as "spot 0"
        public static List<BoardSpot> Compute(int roll)
        {
            // initial matrix
            List<List<BoardSpot>> matrix = NewMatrix(HIGHEST_ROLL - LOWEST_ROLL + 1);

            // initial vector
            List<BoardSpot> vec = new List<BoardSpot>();
            // initialize with initial probabilities
            for (int i = LOWEST_ROLL; i <= HIGHEST_ROLL; ++i) 
            {
                vec.Add(new BoardSpot(i, DiceRoll.P(i)));
            }

            // just return vector of probabilities
            if (roll == 1) 
            {
                return vec;
            }

            for (int iter = 1; iter < roll; ++iter)
            {
                PrintMatrix(matrix);
                Console.WriteLine(SumMatrix(matrix) + "\n");
                // compute possibilities for roll iter + 1
                MatrixMult(ref matrix, vec);
                PrintMatrix(matrix);
                Console.WriteLine(SumMatrix(matrix) + "\n");

                // create new vector and store probabilities in it 
                vec = InitVector(matrix);
                PrintVector(vec);
                Console.WriteLine(SumVector(vec) + "\n");

                // create new matrix for next iter
                matrix = NewMatrix(vec.Count());
            }

            // probabilities are stored in vec list => return that
            return vec;
        }

        private static List<List<BoardSpot>> NewMatrix(int cols)
        {
            List<List<BoardSpot>> board = new List<List<BoardSpot>>();

            for (int row = LOWEST_ROLL; row <= HIGHEST_ROLL; ++row) 
            {
                board.Add(new List<BoardSpot>());
                for (int col = 0; col < cols; ++col)
                {
                    board[row - LOWEST_ROLL].Add(new BoardSpot(row, DiceRoll.P(row)));
                }
            }

            return board;
        }

        private static void MatrixMult(ref List<List<BoardSpot>> matrix, List<BoardSpot> vec) 
        {
            for (int row = 0; row < matrix.Count(); ++row) 
            {
                for (int col = 0; col < matrix[row].Count(); ++col)
                {
                    matrix[row][col] *= vec[col];
                }
            }
        }

        private static void PrintMatrix(List<List<BoardSpot>> matrix)
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

        private static double SumMatrix(List<List<BoardSpot>> matrix) 
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

        private static List<BoardSpot> InitVector(List<List<BoardSpot>> matrix)
        {
            List<BoardSpot> vec = new List<BoardSpot>();

            int rows = matrix.Count();
            int cols = matrix[0].Count();

            int minSpots = matrix[0][0].TotalSpots;
            int maxSpots = matrix[rows - 1][cols - 1].TotalSpots;

            for (int row = minSpots; row <= maxSpots; ++row)
            {
                vec.Add(new BoardSpot(row));
            }

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    BoardSpot spot = matrix[row][col];
                    vec[spot.TotalSpots - minSpots].Prob += spot.Prob;
                }
            }

            return vec;
        }

        private static void PrintVector(List<BoardSpot> vec)
        {
            vec.ForEach((spot) =>
            {
                Console.WriteLine(spot.ToString() + " ");
            });
        }

        private static double SumVector(List<BoardSpot> vec)
        {
            double sum = 0;

            vec.ForEach((spot) =>
            {
                sum += spot.Prob;
            });

            return sum;
        }
    }
}