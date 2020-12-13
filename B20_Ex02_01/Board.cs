using System;
using System.Text;
namespace B20_Ex02_01
{
   public class Board
    {
        private GameCell[,] m_BoardGameMat;
        private int m_HowManyCellsAreOpen;
        private readonly int m_Rows;
        private readonly int m_Cols;
        public Board(int i_Height,int i_Width)
        {
            m_BoardGameMat = new GameCell[i_Height, i_Width];
            m_Rows = i_Height;
            m_Cols = i_Width;
            InitialBoard();
            RandomizeBoard();

        }
        public bool IsAllBoardFull()
        {
            return m_HowManyCellsAreOpen == m_Cols * m_Rows;
        }
        public bool CheckIfEqual(int i_Row1,int i_Col1,int i_Row2,int i_Col2)
        {
            return m_BoardGameMat[i_Row1, i_Col1].Value == m_BoardGameMat[i_Row2, i_Col2].Value;
        }

        private void RandomizeBoard()//ready
        {
            int rowFirst;
            int colFirst;
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    rowFirst = CommonFunctions.Random(m_Cols);
                    colFirst = CommonFunctions.Random(m_Rows);
                    CommonFunctions.Swap(ref m_BoardGameMat[i,j], ref m_BoardGameMat[rowFirst, colFirst]);
                }
            }
        }

        
       
        private void InitialBoard()
        {
            int toAdd = 0;
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    m_BoardGameMat[i, j].Value = toAdd;
                    if (j%2 == 1)
                    {
                        toAdd++;
                    }
                }
            }

        }

        public void MarkCellInBoard(int i_Line,int i_Col)
        {
            m_BoardGameMat[i_Line, i_Col].IsVisible = true;
            m_HowManyCellsAreOpen++;
        }
        public bool IsCellVisible(int i_Row,int i_Col)
        {
            return m_BoardGameMat[i_Row, i_Col].IsVisible;


        }
        public void UnMarkCellInBoard(int i_Line, int i_Col)
        {
            m_BoardGameMat[i_Line, i_Col].IsVisible = false;
            m_HowManyCellsAreOpen--;

        }

        public GameCell[,] BoardGameMat
        {
            get
            {
                return m_BoardGameMat;
            }
        }
        public int Cols
        {
            get
            {
                return m_Cols;
            }
        }
        public int Rows
        {
            get
            {
                return m_Rows;
            }
        }
       
        
        
    }
}
