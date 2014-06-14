using Pathfinding.Core;
using Pathfinding.Finders;
using Pathfinding.Game.Common;
using Pathfinding.Game.Creeps;
using Pathfinding.Game.Towers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.Game
{
    /// <summary>
    /// Game End EventArgs
    /// </summary>
    public class GameEndEventArgs : EventArgs
    {
        /// <summary>
        /// IsGameWon
        /// </summary>
        public bool IsGameWon { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isGameWon"></param>
        public GameEndEventArgs(bool isGameWon) : base()
        {
            this.IsGameWon = isGameWon;
        }
    }

    /// <summary>
    /// Game Class
    /// </summary>
    public class Game
    {
        #region Members

        /// <summary>
        /// Pathfinding Finder
        /// </summary>
        private ISearchAlgorithm finder;

        /// <summary>
        /// CancellationTokenSource
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// CancellationToken
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        /// Task
        /// </summary>
        private Task task;

        /// <summary>
        /// Random
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// User stopped Game
        /// </summary>
        private bool userStop;

        #endregion

        #region Properties

        /// <summary>
        /// Tower List
        /// </summary>
        public List<Tower> Towers { get; private set; }

        /// <summary>
        /// Creeps List
        /// </summary>
        public List<Creep> Creeps { get; private set; }

        /// <summary>
        /// Next Creeps List
        /// </summary>
        public Queue<List<Creep>> CreepsNext { get; private set; }

        /// <summary>
        /// Lifes
        /// </summary>
        public int Lifes  { get; private set; }

        /// <summary>
        /// Cash
        /// </summary>
        public int Cash { get; private set; }

        /// <summary>
        /// Kills
        /// </summary>
        public int Kills { get; private set; }

        /// <summary>
        /// Game Running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Game Paused
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// Sync Object
        /// </summary>
        public Object SyncObject { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Delegate for own Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GameEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Delegate for GameEnd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GameEndEventHandler(object sender, GameEndEventArgs e);

        /// <summary>
        /// Game Over Event
        /// </summary>
        public event GameEndEventHandler OnGameEnded;

        /// <summary>
        /// Creep Finish Reached
        /// </summary>
        public event GameEventHandler OnCreepFinishReached;

        /// <summary>
        /// Creep Killed
        /// </summary>
        public event GameEventHandler OnCreepKilled;

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="finder"></param>
        public Game(GameForm gameForm, ISearchAlgorithm finder)
        {
            this.finder = finder;

            this.Reset();

            this.SyncObject = new Object();

            gameForm.OnTowerAdded += gameForm_OnTowerAdded;
            gameForm.OnTowerRemoved += gameForm_OnTowerRemoved;
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Tower Added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_OnTowerAdded(object sender, EventArgs e)
        {
            this.Cash -= 10;

            this.UpdateCreeps();
        }

        /// <summary>
        /// Tower Removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameForm_OnTowerRemoved(object sender, EventArgs e)
        {
            this.Cash += 5;

            this.UpdateCreeps();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Init
        /// </summary>
        private void Init()
        {
            SearchResult result = this.finder.Search();

            List<Creep> creeps1 = new List<Creep>();
            List<Creep> creeps2 = new List<Creep>();
            List<Creep> creeps3 = new List<Creep>();

            this.CreepsNext.Enqueue(creeps1);
            this.CreepsNext.Enqueue(creeps2);
            this.CreepsNext.Enqueue(creeps3);

            for (int i = 0; i < 20; i++)
            {
                creeps1.Add(new LevelOne(result.Path));
            }

            for (int i = 0; i < 20; i++)
            {
                creeps2.Add(new LevelTwo(result.Path));
            }

            for (int i = 0; i < 20; i++)
            {
                creeps3.Add(new LevelThree(result.Path));
            }
        }

        /// <summary>
        /// Update Task
        /// </summary>
        private void Update()
        {
            this.IsRunning = true;
            bool firstRun = true;

            while (true)
            {
                int tick = Environment.TickCount;

                List<Creep> creepsToRemove = new List<Creep>();

                lock (this.SyncObject)
                {
                    // No more Creeps?
                    if (this.Creeps.Count == 0)
                    {
                        // Next Creeps available?
                        if (this.CreepsNext.Count > 0)
                        {
                            this.Creeps = this.CreepsNext.Dequeue();

                            if (firstRun)
                            {
                                firstRun = false;
                            }
                            else
                            {
                                this.Cash += 100;
                            }

                            for (int i = 0; i < this.Creeps.Count; i++)
                            {
                                this.Creeps[i].NextTick = Environment.TickCount + random.Next(250, 350) * (i + 1);
                            }
                        }
                        else
                        {
                            if (this.userStop)
                                this.OnGameEnded(this, new GameEndEventArgs(false));
                            else
                                this.OnGameEnded(this, new GameEndEventArgs(true));
                            this.Stop();
                            this.Reset();
                            return;
                        }
                    }
                }

                // move creeps
                foreach (Creep creep in this.Creeps)
                {
                    if (creep.NextTickReached(tick))
                    {
                        if (creep.FinishReached)
                        {
                            creepsToRemove.Add(creep);
                            this.Lifes -= 1;
                            this.OnCreepFinishReached(this, new EventArgs());

                            if (this.Lifes == 0)
                            {
                                this.OnGameEnded(this, new GameEndEventArgs(false));
                                this.Stop();
                                this.Reset();
                                return;
                            }
                        }
                        else
                        {
                            creep.Move();
                        }
                    }
                }

                // check if creep is in range of tower
                foreach (Tower tower in this.Towers)
                {
                    if (tower.NextTickReached(tick))
                    {
                        foreach (Creep creep in this.Creeps)
                        {
                            if (Helper.CircleCollision(new PointF(tower.X, tower.Y), tower.Radius, new PointF(creep.X, creep.Y), creep.Width / 2))
                            {
                                // creep is in range so attack it
                                tower.Attack(creep);

                                if (creep.Life == 0)
                                {
                                    lock (this.SyncObject)
                                    {
                                        creepsToRemove.Add(creep);
                                        this.Cash += 5;
                                        this.Kills += 1;
                                        this.OnCreepKilled(this, new EventArgs());
                                    }
                                }

                                break;
                            }
                        }
                    }
                }

                // removed creeps that reached the end
                if (creepsToRemove.Count > 0)
                {
                    lock (this.SyncObject)
                    {
                        foreach (Creep creep in creepsToRemove)
                        {
                            this.Creeps.Remove(creep);
                        }
                    }
                }

                if (cancellationToken.IsCancellationRequested)
                    break;

                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Update Creeps Path
        /// </summary>
        private void UpdateCreeps()
        {
            SearchResult result = this.finder.Search();

            if (!result.FoundPath)
            {
                return;
            }

            lock (this.SyncObject)
            {
                foreach (Creep creep in this.Creeps)
                {
                    creep.UpdatePath(result.Path);
                }

                foreach (List<Creep> creeps in this.CreepsNext)
                {
                    foreach (Creep creep in creeps)
                    {
                        creep.UpdatePath(result.Path);
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.Init();

            this.cancellationTokenSource = new CancellationTokenSource();
            this.cancellationToken = this.cancellationTokenSource.Token;
            this.task = Task.Factory.StartNew(() => Update(), cancellationToken);
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop(bool userStop = false)
        {
            this.userStop = userStop;
            this.Pause();
            this.Reset();
        }

        /// <summary>
        /// Pause
        /// </summary>
        public void Pause()
        {
            if (!this.IsRunning)
                return;

            this.cancellationTokenSource.Cancel();

            this.IsRunning = false;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            this.Towers = new List<Tower>();
            this.Creeps = new List<Creep>();
            this.CreepsNext = new Queue<List<Creep>>();

            this.Lifes = 30;
            this.Kills = 0;
            this.Cash = 100;

            this.IsRunning = false;
            this.IsPaused = false;
        }

        #endregion
    }
}
