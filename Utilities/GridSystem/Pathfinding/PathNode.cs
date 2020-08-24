namespace CodeEasyYT.Utilities.GridSystem.Pathfinding
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        public readonly int x;
        public readonly int y;

        public int gCost;
        public int fCost;
        public int hCost;

        public bool isWalkable;
        public PathNode cameFromNode;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.isWalkable = true;
        }

        public override string ToString()
        {
            return x + ":" + y;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}