using System.Collections;
using System.Collections.Generic;
using System.Text;
using Test.Pathfinding;
using Test.Pathfinding.AStar;
using UnityEngine;

namespace Test.Map.Abstraction
{    

    public class CreatePathfindingTestForAStarScript : MonoBehaviour
    {
        private class MapAbstractionForTest : MapAbstraction
        {
            private static readonly bool[,] testTraveralArea =
            {
                { true, true, true, false, true, true, true, true },
                { false, true, true, false, false, false, true, true },
                { true, true, true, false, true, true, true, true },
                { true, false, true, true, true, true, false, false },
                { true, true, true, true, false, true, false, true },
                { true, false, false, true, true, true, true, true },
                { true, false, false, true, true, false, true, false },
                { true, true, false, true, true, true, false, true },
                { true, false, true, true, false, true, true, true }
            };


            protected override void GenerateGridData(MapData mapData, MapGridConfiguration gridConfig)
            {
                _gridTraversalData = new bool[gridConfig.GridWidth][];

                for (int x = 0; x < gridConfig.GridWidth; x++)
                {
                    _gridTraversalData[x] = new bool[gridConfig.GridHeight];

                    for (int y = 0; y < gridConfig.GridHeight; y++)
                    {
                        _gridTraversalData[x][y] =
                            y < testTraveralArea.GetLength(0) && x < testTraveralArea.GetLength(1) ?
                                testTraveralArea[y, x] : true;
                    }
                }
            }
        }

        [SerializeField]
        private MapGridConfiguration gridConfig = null;

        [SerializeField]
        private HeuristicConfigurationInt heuristicConfig = null;

        [SerializeField]
        private Vector2Int _startCoord = Vector2Int.zero;

        [SerializeField]
        private Vector2Int _targetCoord = Vector2Int.zero;


        // Start is called before the first frame update
        void Start()
        {
            TestAStarPathing();
        }

        private void TestAStarPathing()
        {
            MapAbstractionForTest abstraction = new MapAbstractionForTest();
            abstraction.Initialize(null, gridConfig);

            HeuristicModule heuristic = new HeuristicModule(heuristicConfig);

            AStarAlgorithm aStarAlgorithm = new AStarAlgorithm();
            Vector2Int[] path = aStarAlgorithm.CalculatePath(_startCoord, _targetCoord, heuristic, abstraction);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Calculated path: ");

            if (path != null && path.Length > 0)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    builder.AppendLine(path[i].ToString());
                }
            }

            builder.AppendLine();

            if (path != null && path.Length > 0 && path[path.Length - 1].Equals(_targetCoord))
            {
                builder.AppendLine("Target reached!");
            }
            else
            {
                builder.AppendLine("Path to target not found!");
            }

            Debug.Log(builder.ToString());
        }
    }
}