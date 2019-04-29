using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class ShowScript : MonoBehaviour
{
    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
}
