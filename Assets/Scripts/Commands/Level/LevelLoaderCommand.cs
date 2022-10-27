using UnityEngine;

namespace Commands.Level
{
    public class LevelLoaderCommand 
    {
        public void InitializeLevel(int levelID, Transform levelHolder)
        {
            Object.Instantiate(Resources.Load<GameObject>($"LevelPrefabs/Level {levelID}"), levelHolder);
        }
    }
}   