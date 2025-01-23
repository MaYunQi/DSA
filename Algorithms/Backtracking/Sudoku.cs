
namespace DSA.Algorithms.Backtracking
{
    public class Sudoku
    {
        public char[,] SudokuSolver(char[,] board)
        {
            Solver(board);
            return board;
        }
        private bool Solver(char[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row,col]=='.')
                    {
                        for(char ch='1';ch<='9';ch++)
                        {
                            if(IsValid(row,col,ch,board))
                            {
                                board[row,col] = ch;
                                if(Solver(board))
                                {
                                    return true;
                                }
                                board[row,col] = '.';
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }
        private bool IsValid(int row,int col, char ch, char[,] board)
        {
            for(int i=0;i<9;i++)
            {
                if (board[row,i]==ch)
                    return false;
            }
            for (int i = 0; i < 9; i++) 
            {
                if (board[i,col]==ch)
                    return false;
            }
            int startRow = row / 3 * 3;
            int startCol=col / 3 * 3;
            for(int i = startRow; i < startRow + 3; i++)
            {
                for(int j=startCol;j<startCol+3;j++)
                {
                    if (board[i,j]==ch)
                        return false;
                }    
            }
            return true;
        }
    }
}
