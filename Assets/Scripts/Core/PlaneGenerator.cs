using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brain.Core
{
    public class PlaneGenerator : MonoBehaviour
    {
        /// <summary>
        /// 판 생성
        /// </summary>

        [SerializeField]
        private GameObject cubePrefab;
        [SerializeField]
        private GameObject planePrefab;

        private GameObject plane;
        private int[] row = { -1, 0, 1 };
        private int[] col = { 1, 0, -1 };
        private bool[] isHit = new bool[9];

        public void Start()
        {
            //GeneratePlane();
        }

        public void GeneratePlane()
        {
            // 판 생성

            plane = Instantiate(planePrefab);

            ChooseFace();

            MakePlane();

            RotatePlane();

            plane.transform.position = new Vector3(-10.0f, 0, 0);

        }

        private void ChooseFace()
        {
            int face = Random.Range(0, 3);

            switch(face)
            {
                case 0: //x face -x -> +x
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(-2, row[i], col[j]), Vector3.right)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 1: //y face -y -> +y
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(row[i], -2, col[j]), Vector3.up)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
                case 2: //z face -z -> +z
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Physics.Raycast(new Vector3(col[j], row[i], -2), Vector3.forward)) isHit[i * 3 + j] = true;
                            else isHit[i * 3 + j] = false;
                        }
                    }
                    break;
            }
        }

        private void MakePlane()
        {
            for (int i = 0; i < 9; i++)
            {
                if (isHit[i])
                {
                    plane.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        private void RotatePlane()
        {
            int angle = Random.Range(0, 4);

            switch(angle)
            {
                case 0: //degree 0
                    break;
                case 1: //degree 90
                    plane.transform.Rotate(new Vector3(90.0f, 0, 0));
                    break;
                case 2: //degree 180
                    plane.transform.Rotate(new Vector3(180.0f, 0, 0));
                    break;
                case 3: //degree 270
                    plane.transform.Rotate(new Vector3(270.0f, 0, 0));
                    break;
            }    
        }

        public void ClearPlane()
        {
            plane.SetActive(false);
        }
    }
}
