using UnityEngine;

namespace GamePlay
{
    public class BotSettings : MonoBehaviour
    {
        public void OnBotNbrValueChange(int value)
        {
            GameSettings.BotNumber = value;
        }

        public void OnBotDifficultyValueChange(int value)
        {
            switch (value)
            {
                case 0:
                    GameSettings.Diffulty = BotDifficulties.Normal; break;
                case 1:
                    GameSettings.Diffulty = BotDifficulties.Hard; break;
            }
        }
    }
}