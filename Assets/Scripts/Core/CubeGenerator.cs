using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Brain.Core
{
    [System.Serializable]
    class CubeWrapper
    {
        public Difficulty difficulty;
        public GameObject cubePrefab;
    }

    public class CubeGenerator : MonoBehaviour
    {
        [SerializeField] private List<CubeWrapper> cubePrefabs = null;

        /// <summary>
        /// 한 변의 길이
        /// </summary>
        public int SideLength => sideLength;

        /// <summary>
        /// 활성화된 큐브들의 위치
        /// </summary>
        public List<Vector3Int> OccupiedPositions => occupiedPositions;

        private Dictionary<Difficulty, GameObject> cubePrefabDic = new Dictionary<Difficulty, GameObject>();

        private InputReceiver inputReceiver = null;

        private int difficulty;

        private GameObject rootCube = null;

        private int cubeLimit; // 총 큐브 개수
        private int cubeCount; // 생성할 큐브 개수
        private int sideLength; // 한 변 길이
        private int sideArea; // 한 면 넓이

        private List<Vector3Int> occupiedPositions = new List<Vector3Int>();
        private bool[,,] visitedPosition = new bool[5, 5, 5];
        private List<Vector3Int> possiblePositions = new List<Vector3Int>();

        private int[] deltaX = { 1, 0, 0, -1, 0, 0 };
        private int[] deltaY = { 0, 1, 0, 0, -1, 0 };
        private int[] deltaZ = { 0, 0, 1, 0, 0, -1 };

        public GameObject Init(InputReceiver inputReceiver, Difficulty difficulty)
        {
            this.inputReceiver = inputReceiver;

            cubePrefabDic = cubePrefabs.ToDictionary(x => x.difficulty, x => x.cubePrefab);

            rootCube = Instantiate(cubePrefabDic[difficulty]);

            sideLength = difficulty switch
            {
                Difficulty.Easy => 3,
                Difficulty.Normal => 4,
                Difficulty.Hard => 5,
                _ => 3
            };

            sideArea = sideLength * sideLength;
            cubeLimit = sideLength * sideArea;

            ClearCube();

            return rootCube;
        }

        /// <summary>
        /// 큐브 생성
        /// </summary>
        public void GenerateCube()
        {
            cubeCount = Random.Range(sideLength, cubeLimit - 1);

            int startCubeIndex = Random.Range(0, cubeLimit); // 첫 큐브의 인덱스
            int yzIndex = startCubeIndex % sideArea; // 첫 큐브의 평면상 인덱스 (이동방향인 x축에 수직인 평면)
            Vector3Int firstCubePosition = new Vector3Int(startCubeIndex / sideArea, yzIndex / sideLength, yzIndex % sideLength);
            UpdatePosition(firstCubePosition);

            rootCube.transform.GetChild(startCubeIndex).gameObject.SetActive(true);

            int nextCubeIndex = 0;
            int nextCube = 0;

            while (cubeCount-- != 0)
            {
                nextCubeIndex = Random.Range(0, possiblePositions.Count);
                UpdatePosition(possiblePositions[nextCubeIndex]);

                nextCube = possiblePositions[nextCubeIndex].x * sideArea + possiblePositions[nextCubeIndex].y * sideLength + possiblePositions[nextCubeIndex].z;

                rootCube.transform.GetChild(nextCube).gameObject.SetActive(true);

                possiblePositions.RemoveAt(nextCubeIndex);
            }
        }

        private void UpdatePosition(Vector3Int position)
        {
            occupiedPositions.Add(position);
            visitedPosition[position.x, position.y, position.z] = true;

            int newX = 0, newY = 0, newZ = 0;
            Vector3Int newPos;

            for (int i = 0; i < 6; ++i)
            {
                newX = position.x + deltaX[i];
                newY = position.y + deltaY[i];
                newZ = position.z + deltaZ[i];

                if (newX < 0 || newY < 0 || newZ < 0 || newX >= sideLength || newY >= sideLength || newZ >= sideLength) continue;

                newPos = new Vector3Int(newX, newY, newZ);

                if (visitedPosition[newPos.x, newPos.y, newPos.z]) continue;

                possiblePositions.Add(newPos);
            }
        }

        /// <summary>
        /// 큐브 제거
        /// </summary>
        public void ClearCube()
        {
            foreach (Transform child in rootCube.transform)
            {
                child.gameObject.SetActive(false);
            }

            occupiedPositions.Clear();
            possiblePositions.Clear();
            System.Array.Clear(visitedPosition, 0, visitedPosition.Length);
        }
    }
}
