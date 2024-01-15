namespace Tetris
{
    public class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new J_Block(),
            new L_Block(),
            new OBlock(),
            new S_Block(),
            new T_Block(),
            new Z_Block()
        };

        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }


        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }


        private Block RandomBlock() //returns a random block
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            do
            {
                NextBlock = RandomBlock();
            } while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
