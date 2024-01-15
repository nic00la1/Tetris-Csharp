namespace Tetris
{
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset(); // Reset the block's position

                for (int i = 0; i < 2; i++) // Move the block up two rows
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10); // 22 rows, 10 columns
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        private bool BlockFits() // Checks if the block fits in the grid
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.isEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW() // Rotates the block clockwise
        {
            CurrentBlock.RotateCW(); // Rotate the block

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW(); // Undo the rotation
            }
        }

        public void RotateBlockCCW() // Rotates the block counter-clockwise
        {
            CurrentBlock.RotateCCW(); // Rotate the block

            if (!BlockFits())
            {
                CurrentBlock.RotateCW(); // Undo the rotation
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.isRowEmpty(0) && GameGrid.isRowEmpty(1)); // If the first two rows are not empty, the game is over
        }

        private void PlaceBlock() // Places the block in the grid
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}
