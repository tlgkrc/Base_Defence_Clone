using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class GridSystem
    {
        #region Self Variables

        #region Private Variables
        
        /*if BaseAxis is equal to XY ,_grid1 is X axis ,_grid2 is Y axis
         if BaseAxis is equal to XZ ,_grid1 is X axis ,_grid2 is Z axis
         if BaseAxis is equal to YZ ,_grid1 is Y axis ,_grid2 is Z axis
         */
        private readonly int _grid1;
        private readonly int _grid2;
        private readonly BaseAxis _baseAxis;
        private readonly int _maxStackCount;
        private readonly Vector3 _offset;

        private readonly Transform _initializeTransform;
        private Vector3 _nextPoint;
        private readonly List<Vector3> gridPoints = new List<Vector3>();

        #endregion
        
        #endregion

        public GridSystem(Transform initializeTransform,int maxStackCount,Vector3 offset,int grid_1,int grid_2,BaseAxis baseAxis)
        {
            _initializeTransform = initializeTransform;
            _maxStackCount = maxStackCount;
            _offset = offset;
            _grid1 = grid_1;
            _grid2 = grid_2;
            _baseAxis = baseAxis;
            
            GridAlignment();
        }

        public Vector3 NextPoint(int stackCount)
        {
            return gridPoints[stackCount-1];
        }

        private void GridAlignment()
        {
            Vector3 pos;
            int grid3;
            if (_maxStackCount % (_grid1 * _grid2) == 0)
            {
                grid3 = _maxStackCount / (_grid1 * _grid2);
            }
            else
            {
                grid3 = _maxStackCount / (_grid1 * _grid2) + 1;
            }

            if (_baseAxis == BaseAxis.XZ)
            {
                var startPositionZ = _initializeTransform.localPosition.z - _offset.z / 2 * (_grid2 - 1);
                for (int i = 0; i < grid3 ; i++)
                {
                    pos.y = _initializeTransform.localPosition.y + i * _offset.y;
                    for (int j = 0; j < _grid2; j++)
                    {
                        pos.z = startPositionZ + j * _offset.z;

                        for (int k = 0; k < _grid1; k++)
                        {
                            var startPositionX = _initializeTransform.localPosition.x -_offset.x / 2 * (_grid1 - 1);
                            pos.x = startPositionX + k * _offset.x;

                            gridPoints.Add(pos);
                        }
                    }
                }
            }
            else if (_baseAxis == BaseAxis.XY)
            {
                var startPositionY = _initializeTransform.localPosition.y - _offset.y / 2 * (_grid2 - 1);
                for (int i = 0; i < grid3 ; i++)
                {
                    pos.z = _initializeTransform.localPosition.z + i * _offset.z;
                    for (int j = 0; j < _grid2; j++)
                    {
                        pos.y = startPositionY + j * _offset.y;

                        for (int k = 0; k < _grid1; k++)
                        {
                            var startPositionX = _initializeTransform.localPosition.x -_offset.x / 2 * (_grid1 - 1);
                            pos.x = startPositionX + k * _offset.x;

                            gridPoints.Add(pos);
                        }
                    }
                }
            }
            else if (_baseAxis == BaseAxis.YZ) 
            {
                var startPositionZ = _initializeTransform.localPosition.z - _offset.z / 2 * (_grid2 - 1);
                for (int i = 0; i < grid3 ; i++)
                {
                    pos.x = _initializeTransform.localPosition.x + i * _offset.x;
                    for (int j = 0; j < _grid2; j++)
                    {
                        pos.z = startPositionZ + j * _offset.z;

                        for (int k = 0; k < _grid1; k++)
                        {
                            var startPositionY = _initializeTransform.localPosition.y -_offset.y / 2 * (_grid1 - 1);
                            pos.y = startPositionY + k * _offset.y;

                            gridPoints.Add(pos);
                        }
                    }
                }
            }
        }
    }

    public enum BaseAxis
    {
        XZ,
        XY,
        YZ
    }
}