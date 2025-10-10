using UnityEngine;
namespace Tanchiki.GameManagers
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 0)]
    public class Level : ScriptableObject
    {
        public int LevelBuildIndex;
        public int DisplayLevelIndex;
        public string LevelName;
        public Sprite LevelIcon;
    }
}