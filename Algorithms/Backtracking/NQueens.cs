
namespace DSA.Algorithms.Backtracking
{
    public class NQueens
    {
        public List<List<string>> SolveNQueens(int n)
        {
            List<List<string>> result = new List<List<string>>();
            char[,] board=new char[n,n];
            for (int i = 0;i<n;i++)
            {
                for (int j = 0; j < n; j++)
                {
                    board[i, j] = '.';
                }
            }
            Backtracking(n,0,board, result);
            return result;
        }
        public void Backtracking(int n, int row,char[,] board, List<List<string>> result)
        {
            if(row==n)
            {
                result.Add(ConvertBoradToList(board,n));
                return;
            }
            for(int col=0; col < n; col++)
            {
                if(IsSafe(board,row,col,n))
                {
                    board[row, col] = 'Q';
                    Backtracking(n,row+1,board,result);
                    board[row, col] = '.';
                }
            }
        }
        private bool IsSafe(char[,] borad,int row,int col,int n)
        {
            for(int i=0;i<n;i++)
            {
                if (borad[i,col]=='Q')
                    return false;
            }
            for(int i=row-1,j=col-1; i>=0&&j>=0 ;i--,j--)
            {
                if (borad[i,j]=='Q')
                    return false;
            }
            for(int i = row - 1, j = col + 1; i >= 0 && j <n; i--, j++)
            {
                if (borad[i, j] == 'Q')
                    return false;
            }
            return true;
        }
        private List<string> ConvertBoradToList(char[,] board,int n)
        {
            List<string> result= new List<string>();
            for(int i=0;i<n;i++)
            {
                char[] row = new char[n];
                for(int j=0;j<n;j++)
                {
                    row[j]=board[i,j];
                }
                result.Add(new string(row));
            }
            return result;
        }
    }
}
