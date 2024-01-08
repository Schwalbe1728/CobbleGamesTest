using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.Map.Abstraction
{
    public class MapAbstraction
    {
        private MapData _mapData;
        private MapGridConfiguration _gridConfiguration;

        protected bool[][] _gridTraversalData;

        private Vector3 _minRealCoordinate = Vector3.zero;
        private Vector3 _maxRealCoordinate = Vector3.zero;

        private static readonly int[] _gridNeighboursX = { 0, 1, 1, 1, 0, -1, -1, -1};
        private static readonly int[] _gridNeighboursY = { 1, 1, 0, -1, -1, -1, 0, 1};

        public void Initialize(MapData mapData, MapGridConfiguration gridConfig)
        {
            _mapData = mapData;
            _gridConfiguration = gridConfig;

            if (mapData != null)
            {
                _minRealCoordinate =
                  new Vector3(
                      x: _mapData.CenterCoord.x - _mapData.Width / 2,
                      y: _mapData.CenterCoord.y,
                      z: _mapData.CenterCoord.z - _mapData.Height / 2);

                _maxRealCoordinate =
                    new Vector3(
                        x: _mapData.CenterCoord.x + _mapData.Width / 2,
                        y: _mapData.CenterCoord.y,
                        z: _mapData.CenterCoord.z + _mapData.Height / 2);
            }
            else
            {
                Debug.LogWarning("MapAbstraction: MapData is null!");
            }

            GenerateGridData(mapData, gridConfig);            
        }

        public Vector2Int MapCoordsToGridCoords(Vector3 realPoint)
        {
            Vector3 positionVector = realPoint - _minRealCoordinate;
            float normX = positionVector.x / _mapData.Width;
            float normZ = positionVector.y / _mapData.Height;

            return
                new Vector2Int(
                        Mathf.FloorToInt(normX * _gridConfiguration.GridWidth),
                        Mathf.FloorToInt(normZ * _gridConfiguration.GridHeight)
                    );
        }

        public Vector3 GridCoordsToMapCoords(Vector2Int gridPoint)
        {
            float normX = (gridPoint.x + 0.5f) / _gridConfiguration.GridWidth;
            float normZ = (gridPoint.y + 0.5f) / _gridConfiguration.GridHeight;

            return
                new Vector3
                (
                    normX * _mapData.Width + _minRealCoordinate.x,
                    _minRealCoordinate.y,
                    normZ * _mapData.Height + _minRealCoordinate.z
                    );
        }

        public bool GridCoordTraversible(Vector2Int gridPoint)
        {
            return
                GridCoordTraversible(gridPoint.x, gridPoint.y);
        }

        public bool GridCoordTraversible(int x, int y)
        {
            return
                IsInBoundsGrid(x, y) &&
                _gridTraversalData[x][y];
        }

        public Vector2Int[] GetTraversibleNeighboursOnGrid(Vector2Int gridPoint)
        {
            return GetTraversibleNeighboursOnGrid(gridPoint.x, gridPoint.y);
        }

        public Vector2Int[] GetTraversibleNeighboursOnGrid(int x, int y)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            for(int i = 0; i < _gridNeighboursX.Length; i++)
            {
                if(
                    GridCoordTraversible (x + _gridNeighboursX[i], y + _gridNeighboursY[i]) &&
                    IsDiagonalObstructedByAdjacentNeighbours(x, y, x + _gridNeighboursX[i], y + _gridNeighboursY[i]) == false
                    )
                {
                    result.Add
                        (
                            new Vector2Int(x + _gridNeighboursX[i], y + _gridNeighboursY[i])
                        );
                }
            }

            return
                result.Count > 0 ?
                    result.ToArray() : null;
        }

        public GameObject[] GetObstacleObjects()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void GenerateGridData(MapData mapData, MapGridConfiguration gridConfig)
        {
            _gridTraversalData = new bool[gridConfig.GridWidth][];

            for (int x = 0; x < gridConfig.GridWidth; x++)
            {
                _gridTraversalData[x] = new bool[gridConfig.GridHeight];

                for(int y = 0; y < gridConfig.GridHeight; y++)
                {
                    _gridTraversalData[x][y] = true;
                }
            }

            //  TODO: Finish
            //  1. Get obstacle objects
            //  2. For each obstacle obtain rasterization data (collection of grid coordinates obstructed by the obstacle)

            throw new System.NotImplementedException();
        }

        private void InsertObstacle(GameObject obstacle)
        {
            throw new System.NotImplementedException();
        }

        private Vector2Int[] RasterizeGameObject(GameObject obstacle)
        {
            //  TODO:
            //  https://api.repo.agh.edu.pl/server/api/core/bitstreams/cb63c41a-1b90-46a2-83c7-88cdfba33496/content
            //  https://www.ncbi.nlm.nih.gov/pmc/articles/PMC8707769/

            throw new System.NotImplementedException();
        }

        private bool IsInBoundsGrid(int x, int y)
        {
            return
                x >= 0 && x < _gridConfiguration.GridWidth &&
                y >= 0 && y < _gridConfiguration.GridHeight;
        }

        /// <summary>
        /// returns true if two cells are diagonal to each other and despite being traversible, their common adjacent neighbours obstruct the diagonal path
        /// returns false if from and to not diagonal neighbours
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private bool IsDiagonalObstructedByAdjacentNeighbours(Vector2Int from, Vector2Int to)
        {
            return 
                IsDiagonalObstructedByAdjacentNeighbours(from.x, from.y, to.x, to.y);
        }

        private bool IsDiagonalObstructedByAdjacentNeighbours(int x0, int y0, int x1, int y1)
        {
            bool diagonal =
                Mathf.Abs(x0 - x1) == 1 &&
                Mathf.Abs(y0 - y1) == 1;

            if (diagonal)
            {
                diagonal =
                    (_gridTraversalData[x0][y1] && _gridTraversalData[x1][y0]) == false;
            }

            return diagonal;
        }
    }
}