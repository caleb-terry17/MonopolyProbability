namespace Monopoly.Functionality
{
    public class BoardProb
    {
        private int startPos { get; set; }  // starting square on board (0 would be start)
        private int boardSize { get; set; }  // number of spots on board
        private int LOWEST_ROLL = 2;
        private int HIGHEST_ROLL = 12;

        public BoardProb(int startPos, int boardSize) 
        {
            this.startPos = startPos;
            this.boardSize = boardSize;
        }

        // probability to land on the next x spots given a # of rolls
        // the current position is referred to as "spot 0"
        public List<BoardSpot> RollProb(int roll)
        {
            // just return vector of probabilities
            if (roll == 1) 
            {
                // NEED TO IMPLEMENT
            }

            // initial matrix
            List<List<BoardSpot>> matrix = NewMatrix(HIGHEST_ROLL - LOWEST_ROLL + 1);

            // initial vector
            List<BoardSpot> vec = new List<BoardSpot>();
            // initialize with initial probabilities
            for (int i = LOWEST_ROLL; i <= HIGHEST_ROLL; ++i) 
            {
                vec.Add(new BoardSpot(i, DiceRoll.P(i)));
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

            // compute probabilities
            vec = InitVector(matrix);

            return vec;
        }

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
                return "(" + this.TotalSpots.ToString() + "," + this.Prob.ToString() + ")";
            }

            public static BoardSpot operator *(BoardSpot a, BoardSpot b)
            {
                return new BoardSpot(a.TotalSpots + b.TotalSpots, a.Prob * b.Prob);
            }
        }

        private List<List<BoardSpot>> NewMatrix(int cols)
        {
            List<List<BoardSpot>> board = new List<List<BoardSpot>>();

            for (int row = LOWEST_ROLL; row < HIGHEST_ROLL; ++row) 
            {
                board.Add(new List<BoardSpot>());
                for (int col = 0; col < cols; ++col)
                {
                    board[row - LOWEST_ROLL].Add(new BoardSpot(row, DiceRoll.P(row)));
                }
            }

            return board;
        }

        private void MatrixMult(ref List<List<BoardSpot>> matrix, List<BoardSpot> vec) 
        {
            for (int row = 0; row < matrix.Count(); ++row) 
            {
                for (int col = 0; col < matrix[row].Count(); ++col)
                {
                    matrix[row][col] *= vec[col];
                }
            }
        }

        private void PrintMatrix(List<List<BoardSpot>> matrix)
        {
            for (int row = 0; row < matrix.Count(); ++row)
            {
                for (int col = 0; col < matrix[row].Count(); ++col)
                {
                    Console.Write(matrix[row][col].ToString() + " ");
                }
                Console.Write("\n");
            }
        }

        private List<BoardSpot> InitVector(List<List<BoardSpot>> matrix)
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
    }
}