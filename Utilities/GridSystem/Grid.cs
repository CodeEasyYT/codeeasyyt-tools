using System;
using UnityEngine;
using CodeEasyYT.Utils;

namespace CodeEasyYT.Utilities.GridSystem
{
    /// <summary>
    /// A advenced grid system for games. This includes a debug text feature built in.
    /// </summary>
    /// <typeparam name="TGridObject">Stored objects type</typeparam>
    public class Grid<TGridObject>
    {
        //Events
        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        private int width;
        private int height;
        private float cellSize;
        private TGridObject[,] gridArray;
        private Vector2 originPosition;
        private bool showDebug;

        Func<Grid<TGridObject>, int, int, TGridObject> createGridObject;

        private TextMesh[,] debugTextArray;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">Width of the grid</param>
        /// <param name="height">Height of the grid</param>
        /// <param name="cellSize">Each cells size on world space</param>
        /// <param name="originPosition">Where the grid should be placed</param>
        /// <param name="createGridObject">How should I create this value?</param>
        /// <param name="showDebug">Enable built-in debug text?</param>
        /// <param name="debugColor">Color of the built-in debug text</param>
        /// <param name="fontSize">Size of the debug text (which uses ToString() of the class)</param>
        public Grid(int width, int height, float cellSize, Vector2 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = false, Color? debugColor = null, int fontSize = 20)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.showDebug = showDebug;

            this.createGridObject = createGridObject;

            gridArray = new TGridObject[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            if (showDebug)
            {
                debugTextArray = new TextMesh[width, height];

                if (debugColor == null) debugColor = Color.black;

                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridArray.GetLength(1); y++)
                    {
                        debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector2(cellSize, cellSize) * 0.5f, fontSize, debugColor, textAnchor: TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), (Color)debugColor, float.MaxValue);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), (Color)debugColor, float.MaxValue);
                    }
                }

                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), (Color)debugColor, float.MaxValue);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), (Color)debugColor, float.MaxValue);

                OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
                {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };
            }
        }

        //Helpers
        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * cellSize + originPosition;
        }
        public Vector2Int GetXY(Vector2 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

            return new Vector2Int(Mathf.FloorToInt((worldPosition - originPosition).x / cellSize), Mathf.FloorToInt((worldPosition - originPosition).y / cellSize));
        }

        //Helpers to Outside
        public int GetWidth() => width;
        public int GetHeight() => height;
        public float GetCellSize() => cellSize;

        public void CloneGridObjectToPoint(int x, int y, int targetX, int targetY)
        {
            SetGridObject(targetX, targetY, GetGridObject(x, y));
        }
        public void MoveGridObjectToPoint(int x, int y, int targetX, int targetY)
        {
            CloneGridObjectToPoint(x, y, targetX, targetY);
            RemoveGridObject(x, y);
        }
        public void RemoveGridObject(int x, int y)
        {
            SetGridObject(x, y, createGridObject(this, x, y));
        }

        //Helpers to Inside
        public void TriggerGridObjectChanged(int x, int y)
        {
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        #region Set Value
        /// <summary>
        /// Use this to set the wanted object. WARNING: OLD VALUES WILL BE ERASED!
        /// </summary>
        /// <param name="x">The wanted objects X coordinate.</param>
        /// <param name="y">The wanted objects Y coordinate.</param>
        /// <param name="value">The wanted objects new value.</param>
        public void SetGridObject(int x, int y, TGridObject value)
        {
            if(x >= 0 && y >= 0 && x < width && y < height)
            {
                TGridObject oldValue = gridArray[x, y];
                gridArray[x, y] = value;

                if(OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
            }
        }
        /// <summary>
        /// Use this to set the wanted object with position on world space. WARNING: OLD VALUES WILL BE ERASED!
        /// </summary>
        /// <param name="worldPosition">The wanted objects position on world space</param>
        /// <param name="value">The wanted objects new value.</param>
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }
        #endregion

        #region Get Value
        /// <summary>
        /// Use this to get your object with it's coordinates.
        /// </summary>
        /// <param name="x">The wanted objects X coordinate.</param>
        /// <param name="y">The wanted objects Y coordinate.></param>
        /// <returns>Returns the class that you used.</returns>
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }

            return default(TGridObject);
        }
        /// <summary>
        /// Use this to get your object with position on world space.
        /// </summary>
        /// <param name="worldPosition">The wanted objects position on world space</param>
        /// <returns>Returns the class that you used.</returns>
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
        #endregion
    }
}