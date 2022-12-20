namespace Monopoly.Functionality
{
    public class BoardProb
    {
        private int startPos;  // starting square on board (0 would be start)
        private int boardSize;  // number of spots on board
        private int LOWEST_ROLL = 2;
        private int HIGHEST_ROLL = 12;

        public BoardProb(int startPos, int boardSize) 
        {
            this.startPos = startPos;
            this.boardSize = boardSize;
        }

        // probability to land on the next x spots given a # of rolls
        // the current position is referred to as "spot 0"
        public List<double> RollProb(int roll)
        {
            if (roll == 1) 
            {
                // NEED TO IMPLEMENT
            }

            int maxSpots = roll * HIGHEST_ROLL;
            List<double> prob = new List<double>();
            for (int i = 0; i <= maxSpots; ++i) 
            {
                prob.Add(0);
            }

            List<List<BoardSpot>> board = new List<List<BoardSpot>>();
            List<BoardSpot> vec = new List<BoardSpot>();
            // initialize with initial probabilities
            for (int row = LOWEST_ROLL; row <= HIGHEST_ROLL; ++row) 
            {
                board.Add(new List<BoardSpot>());
                for (int col = LOWEST_ROLL; col <= HIGHEST_ROLL; ++col) 
                {
                    board[row - LOWEST_ROLL].Add(new BoardSpot(row, DiceRoll.P(row)));
                }

                vec.Add(new BoardSpot(row, DiceRoll.P(row)));
            }

            for (int i = 0; i < roll - 1; ++i) 
            {
                board = MatrixMult(board, vec);
            }

            board.ForEach((row) => 
            {
                row.ForEach((spot) =>
                {
                    Console.WriteLine(spot.totalSpots);
                    prob[spot.totalSpots] += spot.prob;
                });
            });

            return prob;
        }

        private class BoardSpot
        {
            public int totalSpots;  // total # of spots from start pos
            public double prob;  // the probability of landing in the current spot

            public BoardSpot(int totalSpots, double prob = 0)
            {
                this.totalSpots = totalSpots;
                this.prob = prob;
            }

            public static BoardSpot operator *(BoardSpot a, BoardSpot b)
            {
                return new BoardSpot(a.totalSpots + b.totalSpots, a.prob * b.prob);
            }
        }

        private List<List<BoardSpot>> MatrixMult(List<List<BoardSpot>> board, List<BoardSpot> vec) 
        {
            for (int row = LOWEST_ROLL; row <= HIGHEST_ROLL; ++row) 
            {
                for (int col = LOWEST_ROLL; col <= HIGHEST_ROLL; ++col)
                {
                    board[row - LOWEST_ROLL][col - LOWEST_ROLL] *= vec[col - LOWEST_ROLL];
                }
            }

            return board;
        }
    }
}