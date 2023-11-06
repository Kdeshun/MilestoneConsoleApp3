using System;
using System.Collections.Generic;
using MilestoneConsoleApp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;

namespace MilestoneConsoleApp
{
    public class Board
    {
        public int boardSize;
        private Cell[,] boardGrid;
        public int difficulty;

        public Board(int boardSize)
        {
            this.boardSize = boardSize;
            boardGrid = new Cell[boardSize, boardSize];
            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    boardGrid[r, c] = new Cell(r, c);
                    boardGrid[r, c].row = r;
                    boardGrid[r, c].col = c;
                }
            }
            this.difficulty = 10;
        }

        public int Boardsize
        {
            get { return boardSize; }
            set { boardSize = value; }
        }

        public Cell[,] BoardGrid
        {
            get { return boardGrid; }
            set { boardGrid = value; }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        public void SetupLiveNeighbors()
        {
            Random random = new Random();
            int liveCount = (int)Math.Round((double)(boardSize * boardSize * (difficulty / 100)));

            while (liveCount > 0)
            {
                int r = random.Next(0, boardSize);
                int c = random.Next(0, boardSize);
                if (!boardGrid[r, c].IsLive)
                {
                    boardGrid[r, c].IsLive = true;
                    liveCount--;
                }
            }
        }

        public void CalculateLiveNeighbors()
        {
            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    Cell cell = boardGrid[r, c];
                    if (cell.IsLive)
                    {
                        cell.LiveNeighbors = 9;
                    }
                    else
                    {
                        int liveCount = 0;
                        for (int i = -1; i <= 0; i--)
                        {
                            for (int j = -1; j <= 0; j++)
                            {
                                if (i == 0 && j == 0) continue;
                                int row = r + i;
                                int col = c + j;

                                if (row >= 0 && row < boardSize && col >= 0 && c < boardSize && boardGrid[r, c].IsLive)
                                {
                                    liveCount++;
                                }
                            }
                        }
                        cell.LiveNeighbors = liveCount;
                    }
                }
            }

        }

        public void floodFill(int r, int c)
        {
            // Check if the current cell is valid and has no live neighbors
            if (r >= 0 && r < boardSize && c >= 0 && c < boardSize && !boardGrid[r, c].visited && boardGrid[r, c].LiveNeighbors == 0)
            {
                // Mark the cell as visited
                boardGrid[r, c].visited = true;

                // Recursively call floodFill on surrounding cells
                floodFill(r - 1, c); // up
                floodFill(r + 1, c); // down
                floodFill(r, c - 1); // left
                floodFill(r, c + 1); // right
            }
        }
    }
}







