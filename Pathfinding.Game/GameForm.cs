using Pathfinding.Core;
using Pathfinding.Finders;
using Pathfinding.Game.Creeps;
using Pathfinding.Game.Towers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Pathfinding.Game
{
    /// <summary>
    /// GameForm Class
    /// </summary>
    public partial class GameForm : Form
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
        private static int gridNodeSize = Properties.Settings.Default.GridNodeSize;

        #endregion

        #region Members

        /// <summary>
        /// Random Class
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Buffer for Double Buffering
        /// </summary>
        private Bitmap bitmapBuffer;

        /// <summary>
        /// Buffer for Double Buffering
        /// </summary>
        private Graphics graphicsBuffer;

        /// <summary>
        /// Font for Debug Mode
        /// </summary>
        private Font font = new Font("Arial", 10);

        /// <summary>
        /// Next Tick to Write Buffer to Screen
        /// </summary>
        private int nextTick;

        /// <summary>
        /// Current Frames
        /// </summary>
        private int frames;

        /// <summary>
        /// Current FPS
        /// </summary>
        private int fps;

        /// <summary>
        /// Graph for the Game
        /// </summary>
        private Graph graph;

        /// <summary>
        /// Game Class
        /// </summary>
        private Game game;

        /// <summary>
        /// Pathfinding Class
        /// </summary>
        private ISearchAlgorithm finder;

        /// <summary>
        /// Debug Mode
        /// </summary>
        private bool debugMode;
        
        /// <summary>
        /// Draw Life of Creeps
        /// </summary>
        private bool drawLife;

        #endregion

        #region Events

        /// <summary>
        /// Delegate for own Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GameEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Tower Added Event
        /// </summary>
        public event GameEventHandler OnTowerAdded;

        /// <summary>
        /// Tower Removed Event
        /// </summary>
        public event GameEventHandler OnTowerRemoved;

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameForm()
        {
            InitializeComponent();

            bitmapBuffer = new Bitmap(pictureBoxGrid.Width, pictureBoxGrid.Height);
            graphicsBuffer = Graphics.FromImage(bitmapBuffer);

            // http://www.codeproject.com/Articles/409988/Beginners-Starting-a-2D-Game-with-GDIplus
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, false);

            this.graph = new Graph(GameForm.gridWidth, GameForm.gridHeight);
            this.graph[0, 0].Type = NodeType.Start;
            this.graph.Start = this.graph[0, 0];
            this.graph[gridWidth - 1, gridHeight - 1].Type = NodeType.End;
            this.graph.End = this.graph[gridWidth - 1, gridHeight - 1];

            this.finder = new Astar(graph);
            this.game = new Game(this, this.finder);
            this.game.OnCreepKilled += game_OnCreepKilled;
            this.game.OnCreepFinishReached += game_OnCreepFinishReached;
            this.game.OnGameEnded += game_OnGameEnded;

            // Draw every second
            this.nextTick = Environment.TickCount + 1000;
            this.frames = 0;
            this.fps = 0;

            this.debugMode = false;
            this.drawLife = false;

            // Redraw Picture Box
            this.pictureBoxGrid.Invalidate();
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Creep Killed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_OnCreepKilled(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Creep Finish Reached Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_OnCreepFinishReached(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Game Ended Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_OnGameEnded(object sender, GameEndEventArgs e)
        {
            string msg = e.IsGameWon ? "Game Won" : "Game Over";

            if (MessageBox.Show(msg, "Pathfinding", MessageBoxButtons.OK, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                this.game.Stop();
                this.Reset();
                this.ResetGrid();
                buttonStart.Text = "Start";
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Render the Game
        /// </summary>
        /// <param name="g"></param>
        private void Render(Graphics g)
        {
this.ResetGrid(graphicsBuffer);

if (debugMode)
{
    frames++;

    if (Environment.TickCount >= nextTick)
    {
        Debug.WriteLine("FPS: {0}", frames);
        fps = frames;
        frames = 0;
        nextTick = Environment.TickCount + 1000;
    }

    if (fps > 0)
    {
        graphicsBuffer.DrawString(string.Format("FPS: {0}", fps), font, Brushes.Yellow, 2.0f, 2.0f);
    }
}

Tower selectedTower = null;

foreach (Tower tower in game.Towers)
{
    if (tower.IsSelected)
    {
        selectedTower = tower;
    }
    else
    {
        tower.Draw(graphicsBuffer);
    }
}

lock (this.game.SyncObject)
{
    foreach (Creep creep in game.Creeps)
    {
        creep.Draw(graphicsBuffer);

        if (this.drawLife)
        {
            creep.DrawLife(graphicsBuffer);
        }
    }
}

if (selectedTower != null)
{
    selectedTower.Draw(graphicsBuffer);
}

g.DrawImage(bitmapBuffer, 0, 0);

this.labelLifesValue.Text = this.game.Lifes.ToString();
this.labelKilledValue.Text = this.game.Kills.ToString();
this.labelCashValue.Text = this.game.Cash.ToString();
        }

        /// <summary>
        /// Reset the Graph
        /// </summary>
        private void Reset()
        {
            this.graph.Reset();

            this.graph[0, 0].Type = NodeType.Start;
            this.graph.Start = this.graph[0, 0];
            this.graph[gridWidth - 1, gridHeight - 1].Type = NodeType.End;
            this.graph.End = this.graph[gridWidth - 1, gridHeight - 1];
        }

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

            for (int x = 0; x < GameForm.gridWidth; x++)
            {
                for (int y = 0; y < GameForm.gridHeight; y++)
                {
                    int rectX = x + x * GameForm.gridNodeSize;
                    int rectY = y + y * GameForm.gridNodeSize;
                    
                    Node node = this.graph.Map[x, y];

                    if (node.Type == NodeType.Empty || node.Type == NodeType.Wall)
                        graphics.FillRectangle(Brushes.Silver, rectX, rectY, GameForm.gridNodeSize, GameForm.gridNodeSize);
                    else if (node.Type == NodeType.Start)
                        graphics.FillRectangle(Brushes.Green, rectX, rectY, GameForm.gridNodeSize, GameForm.gridNodeSize);
                    else if (node.Type == NodeType.End)
                        graphics.FillRectangle(Brushes.Red, rectX, rectY, GameForm.gridNodeSize, GameForm.gridNodeSize);
                }
            }
        }

        /// <summary>
        /// Draw a Node
        /// </summary>
        /// <param name="gridNodeX"></param>
        /// <param name="gridNodeY"></param>
        /// <param name="type"></param>
        private void DrawNode(int gridNodeX, int gridNodeY, NodeType type)
        {
            int rectX = gridNodeX + gridNodeX * GameForm.gridNodeSize;
            int rectY = gridNodeY + gridNodeY * GameForm.gridNodeSize;

            if (type == NodeType.Wall)
            {
                if (this.graph.Map[gridNodeX, gridNodeY].Type == NodeType.Empty)
                {
                    this.graph.Map[gridNodeX, gridNodeY].Type = NodeType.Wall;

                    SearchResult result = this.finder.Search();

                    if (!result.FoundPath)
                    {
                        lock (this.game.SyncObject)
                        {
                            this.graph.Map[gridNodeX, gridNodeY].Type = NodeType.Empty;
                            this.game.Towers.RemoveAt(this.game.Towers.Count - 1);
                        }

                        return;
                    }

                    if (this.OnTowerAdded != null)
                        this.OnTowerAdded(this, new EventArgs());

                    this.pictureBoxGrid.Invalidate();
                }
            }
            else if (type == NodeType.Empty)
            {
                if (this.graph.Map[gridNodeX, gridNodeY].Type != NodeType.Empty)
                {
                    this.graph.Map[gridNodeX, gridNodeY].Type = NodeType.Empty;

                    if (this.OnTowerRemoved != null)
                        this.OnTowerRemoved(this, new EventArgs());

                    this.pictureBoxGrid.Invalidate();
                }
            }
        }

        /// <summary>
        /// OnPaint Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGrid_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            this.Render(g);

            this.pictureBoxGrid.Invalidate();
        }

        /// <summary>
        /// Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (game.IsRunning)
            {
                game.Stop(true);
                buttonStart.Text = "Start";
            }
            else
            {
                game.Start();
                buttonStart.Text = "Stop";
            }
        }

        /// <summary>
        /// MouseClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X % GameForm.gridNodeSize + 1 == 0 || e.Y % GameForm.gridNodeSize + 1 == 0)
                return;

            int gridNodeX = e.X / (GameForm.gridNodeSize + 1);
            int gridNodeY = e.Y / (GameForm.gridNodeSize + 1);
            NodeType gridNodeType = NodeType.Empty;

            if (e.Button == MouseButtons.Left)
            {
                Tower selectedTower = game.Towers.Find(t => t.IsSelected);

                if (selectedTower != null)
                    selectedTower.IsSelected = false;


                if (this.graph[gridNodeX, gridNodeY].Type == NodeType.Wall)
                {
                    Tower tower = game.Towers.Find(t => t.GridX == gridNodeX && t.GridY == gridNodeY);

                    if (tower != null)
                        tower.IsSelected = true;

                    return;
                }
                else
                {
                    if (this.game.Cash == 0)
                    {
                        return;
                    }

                    if (this.radioButtonLaserTower.Checked)
                    {
                        game.Towers.Add(new LaserTower(gridNodeX, gridNodeY));
                    }
                    else if (this.radioButtonRocketTower.Checked)
                    {
                        game.Towers.Add(new RocketTower(gridNodeX, gridNodeY));
                    }

                    gridNodeType = NodeType.Wall;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (this.graph[gridNodeX, gridNodeY].Type == NodeType.Wall)
                {
                    Tower tower = game.Towers.Find(t => t.GridX == gridNodeX && t.GridY == gridNodeY);

                    if (tower != null)
                    {
                        game.Towers.Remove(tower);
                    }
                }
                
                gridNodeType = NodeType.Empty;
            }

            this.DrawNode(gridNodeX, gridNodeY, gridNodeType);
        }

        /// <summary>
        /// KeyPress Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'd' || e.KeyChar == 'D')
            {
                this.debugMode = !this.debugMode;
            }
            else if (e.KeyChar == 'l' || e.KeyChar == 'L')
            {
                this.drawLife = !this.drawLife;
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                Tower selectedTower = game.Towers.Find(t => t.IsSelected);

                if (selectedTower != null)
                    selectedTower.IsSelected = false;
            }
        }

        /// <summary>
        /// Exit OnClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.game.Stop();
            this.Close();
        }

        #endregion
    }
}
