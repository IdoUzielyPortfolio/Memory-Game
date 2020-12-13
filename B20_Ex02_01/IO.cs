using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02_01
{
    class IO
    {
        private PlayGame m_PlayGame;
        char[] m_LettersToShow;
        
        public IO()//nice
        {
            StartingMenu();
        }
        private void InitLetterArray(int i_Rows,int i_Cols)
        {
            m_LettersToShow = new char[(i_Rows * i_Cols) / 2];
            char currLetter = 'A';
            for (int i = 0; i < m_LettersToShow.Length; i++)
            {
                m_LettersToShow[i] = currLetter++;
            }
        }
        public void StartingMenu()
        {
            int  rowsInput, colsInput;
            eGameType gameTypeInput;
            ExitOrCont exitOrCont=ExitOrCont.Continue;
            string firstName, secondName = "pc";
            Console.WriteLine("Hello, Welcome to the game, please enter your name");
            GetValidName(out firstName);
            GetValidChoise(out gameTypeInput);
            while (exitOrCont!=ExitOrCont.Exit)
            {
                GetValidBoardSize(out rowsInput, out colsInput);
                InitLetterArray(rowsInput, colsInput);
                if (gameTypeInput == eGameType.AgainstPlayer2)
                {
                    Console.WriteLine("Please enter second player name: ");
                    GetValidName(out secondName);
                }
                m_PlayGame = new PlayGame(firstName, secondName, rowsInput, colsInput, gameTypeInput);
                StartMoves(out exitOrCont);
                if(exitOrCont==ExitOrCont.Continue)
                {
                    GetValidExitOrCont(out exitOrCont);
                }
            }
            Console.WriteLine("Have a nice day");
        }
       
        
        private void GetValidExitOrCont(out ExitOrCont o_UserChoiseExitOrCont)
        {
            int userInput;
            Console.WriteLine("Do you want to play another game? press 1 for yes and 2 for no ");
            int.TryParse(Console.ReadLine(), out userInput);
            while(!CheckIfValidChoise(userInput))
            {
                Console.WriteLine("Wrong Choise!");
                Console.WriteLine("Do you want to play another game? press 1 for yes and 2 for no ");
                int.TryParse(Console.ReadLine(), out userInput);
            }
            o_UserChoiseExitOrCont = (ExitOrCont)userInput;
        }
        private void GetValidName(out string o_Name)
        {
            o_Name = Console.ReadLine();
            while(o_Name.Length == 0)
            {
                Console.WriteLine("Name is empty. Please enter a correct name");
                o_Name = Console.ReadLine();
            }
        }
        private void GetValidBoardSize(out int o_Rows, out int o_Cols)//p
        {
            bool isValidRows, isValidCols;
            Console.WriteLine("Please enter the row and col of the board(from 4X4 to 6X6 and num of cells has to be even)");
            isValidRows = int.TryParse(Console.ReadLine(), out o_Rows);
            isValidCols = int.TryParse(Console.ReadLine(), out o_Cols);
            while (!CheckValidBoardSize(o_Rows, o_Cols))
            {
                Console.WriteLine("Please enter valid row and col of the board");
                isValidRows = int.TryParse(Console.ReadLine(), out o_Rows);
                isValidCols = int.TryParse(Console.ReadLine(), out o_Cols);
            }
          }
        private bool CheckValidBoardSize(int i_Rows, int i_Cols)//p
        {
            return i_Rows >= 4 && i_Cols >= 4 && i_Rows <= 6 && i_Cols <= 6 && i_Rows * i_Cols % 2 == 0;
        }
        private void GetValidChoise(out eGameType o_UserChoise)//ready
        {
            bool isNum;
            int choise;
            Console.WriteLine("Please enter 1 to play agains pc or 2 to play against friend");
            isNum = int.TryParse(Console.ReadLine(), out choise);
            while (!CheckIfValidChoise(choise))
            {
                Console.WriteLine("Please enter valid choise 1 or 2");
                isNum = int.TryParse(Console.ReadLine(), out choise);
            }
            o_UserChoise = (eGameType)choise;
        }
  
        public void StartMoves(out ExitOrCont o_ExitOrCont)
        {
           
            eCurrentPlayer currPlayer = eCurrentPlayer.Player1;
            o_ExitOrCont = ExitOrCont.Continue;
            while (!m_PlayGame.IsEndOfGame()&&o_ExitOrCont==ExitOrCont.Continue)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                showBoard();
                if (currPlayer == eCurrentPlayer.Player1)
                {
                    TurnOfPlayer(currPlayer,out o_ExitOrCont);
                    currPlayer = eCurrentPlayer.Player2;
                }
                else if(m_PlayGame.GameType== eGameType.AgainstComp)
                {
                    TurnOfPc();
                    currPlayer = eCurrentPlayer.Player1;
                }
                else
                {
                    TurnOfPlayer(currPlayer, out o_ExitOrCont);
                    currPlayer = eCurrentPlayer.Player1;
                }
            }
            if(o_ExitOrCont==ExitOrCont.Continue)
                WinnerDecleration();
           }
        public void TurnOfPc()
        {
            int row1, col1, row2, col2;
            m_PlayGame.PcOneMove(out row1, out col1);
            m_PlayGame.PcOneMove(out row2, out col2);
            Ex02.ConsoleUtils.Screen.Clear();
            showBoard();
            Console.WriteLine("PC TURN!");
            System.Threading.Thread.Sleep(2000);

            if (m_PlayGame.AddPointsIfCorrectAns(row1, col1, row2, col2, eCurrentPlayer.Player2))
            {
                Console.WriteLine("Pc had a match!");
            }
            else
            {
                Console.WriteLine("Pc didn't has a match!");
            }
            System.Threading.Thread.Sleep(2000);
        }
        public bool CheckIfValidChoise(int i_Choise)//ready
        {
            return (i_Choise == 1 || i_Choise == 2);
        }

        private void GetValidTurn(out int o_Row, out int o_Col, out ExitOrCont o_ExitOrCont)
        {
            string turn;
            turn = Console.ReadLine();

            if (turn.Length >= 2)
            {
                o_Col = GetNumOfColFromChar(turn[0]);
                o_Row = GetNumOfRowFromChar(turn[1]);
            }
            else
            {
                o_Col = 0;
                o_Row = 0;
            }
            o_ExitOrCont = ExitOrCont.Continue;
            while ((!CheckValidFormatOfATurn(turn) || !m_PlayGame.CheckValidRowCol(o_Row, o_Col)) && turn != "Q")
            {
                Console.WriteLine("Wrong Input, please pick a right cell");
                turn = Console.ReadLine();
                if (turn != "Q")
                {
                    o_Col = GetNumOfColFromChar(turn[0]);
                    o_Row = GetNumOfRowFromChar(turn[1]);
                }

            }

           if (turn == "Q")
            {
                o_ExitOrCont = ExitOrCont.Exit;
                
            }
        }
        private int GetNumOfColFromChar(char i_Char)
        {
            return i_Char - 'A';
        }
        private int GetNumOfRowFromChar(char i_Char)
        {
            int row;
            int.TryParse(i_Char.ToString(), out row);
            row--;
            return row;
        }
        private bool CheckValidFormatOfATurn(string i_strToCheck)
        {
            return i_strToCheck.Length == 2 && char.IsUpper(i_strToCheck[0]) && char.IsDigit(i_strToCheck[1]);
        }

        private void PlayerStepInputAndShowBoard(out int o_Row, out int o_Col,out ExitOrCont o_ExitOrCont)
        {
            
            GetValidTurn(out o_Row, out o_Col,out o_ExitOrCont);
            if (o_ExitOrCont == ExitOrCont.Continue)
            {
                m_PlayGame.MarkCell(o_Row, o_Col);
                Ex02.ConsoleUtils.Screen.Clear();
                showBoard();
            }
        }
        private void TurnOfPlayer(eCurrentPlayer i_CurrPlayer,out ExitOrCont o_ExitOrCont)
        {
          //  string turn;
            int row1, col1, row2, col2;
            Console.WriteLine("Please enter the first card for " + i_CurrPlayer);
            PlayerStepInputAndShowBoard(out row1, out col1,out o_ExitOrCont);
            if (o_ExitOrCont == ExitOrCont.Continue)
            {
                Console.WriteLine("Please enter the second card");
                PlayerStepInputAndShowBoard(out row2, out col2, out o_ExitOrCont);
            
                if (m_PlayGame.AddPointsIfCorrectAns(row1, col1, row2, col2, i_CurrPlayer))
                {
                    Console.WriteLine("Congrats! Right Choise");
                }
                else
                {
                    Console.WriteLine("Not bad. Maybe Next Time");
                }
            }
            System.Threading.Thread.Sleep(2000);
        }
        private StringBuilder GetLineOfEquals(int i_HowManyToPutInLine)
        {
            StringBuilder LineOfEquals = new StringBuilder();

            for (int i = 0; i < i_HowManyToPutInLine * 6; i++)
            {
                LineOfEquals.Append("=");
            }
            return LineOfEquals;
        }
        private void AddColsSignsToString(StringBuilder i_ShowGameBoard, int i_Cols)
        {
            char colsSign = 'A';
            for (int i = 0; i < i_Cols; i++)
            {
                i_ShowGameBoard.Append("   ");
                i_ShowGameBoard.Append(colsSign);
                i_ShowGameBoard.Append("  ");
                colsSign++;
            }
            i_ShowGameBoard.AppendLine(Environment.NewLine);


        }
        private void AddCellsToBoard(StringBuilder i_ShowGameBoard, StringBuilder i_LineOfEquals, Board i_BoardGame)
        {
            int numOfLine = 1, numOfObjectsInLine = 0;
            foreach (GameCell item in i_BoardGame.BoardGameMat)
            {
                if (numOfObjectsInLine == 0)
                {
                    i_ShowGameBoard.Append(numOfLine.ToString());
                    i_ShowGameBoard.Append(" | ");

                }
                if (item.IsVisible)
                {
                    i_ShowGameBoard.Append(m_LettersToShow[item.Value].ToString() + " ");
                }
                else
                {
                    i_ShowGameBoard.Append("  ");

                }
                numOfObjectsInLine++;
                i_ShowGameBoard.Append(" | ");
                if (numOfObjectsInLine == i_BoardGame.Cols)
                {
                    i_ShowGameBoard.AppendLine(Environment.NewLine);
                    i_ShowGameBoard.Append(i_LineOfEquals);
                    i_ShowGameBoard.AppendLine(Environment.NewLine);
                    numOfObjectsInLine = 0;
                    numOfLine++;
                }
            }
        }

        private void showBoard()
        {
            Board boardToPrint = m_PlayGame.BoardGame;
            StringBuilder showGameBoard = new StringBuilder();
            StringBuilder LineOfEquals = GetLineOfEquals(boardToPrint.Cols);
            AddColsSignsToString(showGameBoard,boardToPrint.Cols);
            showGameBoard.Append(LineOfEquals);
            showGameBoard.AppendLine(Environment.NewLine);
            AddCellsToBoard(showGameBoard, LineOfEquals, boardToPrint);
            showGameBoard.AppendLine();
            Console.WriteLine(showGameBoard);
        }

        private void WinnerDecleration()
        {
            StringBuilder outputMess = new StringBuilder();
            int scorePlayer1, scorePlayer2;
            string namePlayer1, namePlayer2;
            m_PlayGame.GetScore(out scorePlayer1,out scorePlayer2);
            m_PlayGame.GetPlayersNames(out namePlayer1, out namePlayer2);
            outputMess.Append(String.Format(@"Game has ended, score sheet is:
{0} has {1} points,
{2} has {3} points", namePlayer1,scorePlayer1,namePlayer2,scorePlayer2));
            outputMess.AppendLine();
            if (scorePlayer1 > scorePlayer2)
            {
              outputMess.AppendLine("The winner is: " + namePlayer1);
            }
            else if(scorePlayer2 > scorePlayer1)
            {
                outputMess.AppendLine("The winner is: " + namePlayer2);    
            }
            else
            {
                outputMess.AppendLine("This is a tie ");
            }
            Console.WriteLine(outputMess);

        }
     
    }
}
