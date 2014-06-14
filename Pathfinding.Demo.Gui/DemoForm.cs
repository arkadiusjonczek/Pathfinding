using Pathfinding.Core;
using Pathfinding.Finders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pathfinding.Demo.Gui
{
    /// <summary>
    /// DemoForm class
    /// </summary>
    public partial class DemoForm : Form
    {
        #region Static Members

        /// <summary>
        /// Grid Width
        /// </summary>
        private static int gridWidth = 25;

        /// <summary>
        /// Grid Height
        /// </summary>
        private static int gridHeight = 20;

        /// <summary>
        /// Grid Node Size
        /// </summary>
        private static int gridNodeSize = 20;

        #endregion

        #region Members

        /// <summary>
        /// Our Graph
        /// </summary>
        private Graph graph;

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DemoForm()
        {
            InitializeComponent();

            this.comboBoxAlgorithm.SelectedIndex = 0;

            this.graph = new Graph(gridWidth, gridHeight);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// OnPaint Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGrid_Paint(object sender, PaintEventArgs e)
        {
            this.ResetGrid(e.Graphics);
            base.OnPaint(e);
        }

        /// <summary>
        /// OnClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            this.graph.Reset();
            this.ResetGrid();
        }

        /// <summary>
        /// OnClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.ResetGrid();

            ISearchAlgorithm finder = null;

            if (this.comboBoxAlgorithm.Text == "Dijkstra")
            {
                finder = new Dijkstra(this.graph);
            }
            else if (this.comboBoxAlgorithm.Text == "A* Manhattan")
            {
                finder = new Astar(this.graph);
            }
            else if (this.comboBoxAlgorithm.Text == "A* Euclidean")
            {
                finder = new Astar(this.graph, HeuristicType.Euclidean);
            }
            else if (this.comboBoxAlgorithm.Text == "A* Chebyshev")
            {
                finder = new Astar(this.graph, HeuristicType.Chebyshev);
            }

            finder.CheckingNode += finder_CheckingNode;
            //finder.CheckingNodeAdjacent += finder_CheckingNodeAdjacent;
            finder.FoundPath += finder_FoundPath;

            SearchOptions searchOptions = new SearchOptions(checkBoxAllowDiagonal.Checked, checkBoxAllowCrossCorners.Checked);

            SearchResult result = finder.Search(searchOptions);
        }

        /// <summary>
        /// FoundPath Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finder_FoundPath(object sender, GraphPathEventArgs e)
        {
            textBoxInfo.Text = string.Format("Cost: {0}", this.graph.End.Cost);

            Stack<Node> path = e.Path;

            foreach (Node node in path)
            {
                this.DrawRectArrow(node.Parent, node, Brushes.Violet);
            }
        }

        /// <summary>
        /// CheckingNode Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finder_CheckingNode(object sender, NodeEventArgs e)
        {
            Node node = e.Node;

            if (node.Type == NodeType.Empty)
            {
                this.DrawNodeRect(DemoForm.GetNodeRectX(node.X), DemoForm.GetNodeRectY(node.Y), Brushes.Blue);
            }
        }

        /// <summary>
        /// CheckingNodeAdjacent Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finder_CheckingNodeAdjacent(object sender, AdjacentNodeEventArgs e)
        {
            Node parent = e.Parent;
            Node adjacent = e.Adjacent;

            if (adjacent.Type == NodeType.Empty)
            {
                this.DrawRectArrow(parent, adjacent, Brushes.Yellow);
            }
        }

        /// <summary>
        /// MouseClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X % DemoForm.gridNodeSize + 1 == 0 || e.Y % DemoForm.gridNodeSize + 1 == 0)
                return;

            int gridNodeX = e.X / (DemoForm.gridNodeSize + 1);
            int gridNodeY = e.Y / (DemoForm.gridNodeSize + 1);
            NodeType gridNodeType = NodeType.Empty;

            if (e.Button == MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {

                    gridNodeType = NodeType.Wall;
                }
                else
                {
                    gridNodeType = NodeType.Start;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {

                    gridNodeType = NodeType.Empty;
                }
                else
                {
                    gridNodeType = NodeType.End;
                }
            }

            Node currentNode = this.graph.Map[gridNodeX, gridNodeY];

            if (gridNodeType == NodeType.Start && this.graph.Start != null ||
                gridNodeType == NodeType.End && this.graph.End != null ||
                gridNodeType == NodeType.Wall && currentNode.Type != NodeType.Empty)
                return;

            this.DrawNode(gridNodeX, gridNodeY, gridNodeType);
        }

        /// <summary>
        /// OnClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.ResetGrid();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reset the Grid
        /// </summary>
        /// <param name="graphics"></param>
        private void ResetGrid(Graphics graphics = null)
        {
            if (graphics == null)
            {
                graphics = this.pictureBoxGrid.CreateGraphics();
            }

            graphics.Clear(Color.White);

            for (int x = 0; x < DemoForm.gridWidth; x++)
            {
                for (int y = 0; y < DemoForm.gridHeight; y++)
                {
                    this.DrawNode(x, y, this.graph.Map[x, y].Type, graphics);
                }
            }
        }

        /// <summary>
        /// Draw a Node
        /// </summary>
        /// <param name="gridNodeX"></param>
        /// <param name="gridNodeY"></param>
        /// <param name="type"></param>
        /// <param name="g"></param>
        private void DrawNode(int gridNodeX, int gridNodeY, NodeType type, Graphics g = null)
        {
            int rectX = gridNodeX + gridNodeX * DemoForm.gridNodeSize;
            int rectY = gridNodeY + gridNodeY * DemoForm.gridNodeSize;
            Node currentNode = this.graph.Map[gridNodeX, gridNodeY];

            if (type == NodeType.Start)
            {
                this.graph.Start = currentNode;
                this.graph.Start.Type = NodeType.Start;
                this.DrawNodeRect(rectX, rectY, Brushes.Green, g);
            }
            else if (type == NodeType.End)
            {
                this.graph.End = currentNode;
                this.graph.End.Type = NodeType.End;
                this.DrawNodeRect(rectX, rectY, Brushes.Red, g);
            }
            else if (type == NodeType.Wall)
            {
                currentNode.Type = NodeType.Wall;
                this.DrawNodeRect(rectX, rectY, Brushes.Black, g);
            }
            else if (type == NodeType.Empty)
            {
                if (currentNode.Type == NodeType.Start)
                    this.graph.Start = null;
                else if (currentNode.Type == NodeType.End)
                    this.graph.End = null;

                currentNode.Type = NodeType.Empty;
                this.DrawNodeRect(rectX, rectY, Brushes.Silver, g);
            }
        }

        /// <summary>
        /// Draw a Rect
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="brush"></param>
        /// <param name="g"></param>
        private void DrawNodeRect(int x, int y, Brush brush, Graphics g = null)
        {
            if (g == null)
                g = this.pictureBoxGrid.CreateGraphics();

            g.FillRectangle(brush, x, y, DemoForm.gridNodeSize, DemoForm.gridNodeSize);
        }

        /// <summary>
        /// Draw Arrow
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="adjacent"></param>
        /// <param name="brush"></param>
        private void DrawRectArrow(Node parent, Node adjacent, Brush brush)
        {
            Pen pen = new Pen(brush);

            int parentX = GetNodeRectX(parent.X) + (DemoForm.gridNodeSize / 2);
            int parentY = GetNodeRectY(parent.Y) + (DemoForm.gridNodeSize / 2);
            int adjacentX = GetNodeRectX(adjacent.X) + (DemoForm.gridNodeSize / 2);
            int adjacentY = GetNodeRectY(adjacent.Y) + (DemoForm.gridNodeSize / 2);

            Graphics g = this.pictureBoxGrid.CreateGraphics();
            g.DrawLine(pen, parentX, parentY, adjacentX, adjacentY);
        }

        /// <summary>
        /// Get X
        /// </summary>
        /// <param name="nodeX"></param>
        /// <returns></returns>
        private static int GetNodeRectX(int nodeX)
        {
            return nodeX + nodeX * DemoForm.gridNodeSize;
        }

        /// <summary>
        /// Get Y
        /// </summary>
        /// <param name="nodeY"></param>
        /// <returns></returns>
        private static int GetNodeRectY(int nodeY)
        {
            return nodeY + nodeY * DemoForm.gridNodeSize;
        }

        #endregion
    }
}
