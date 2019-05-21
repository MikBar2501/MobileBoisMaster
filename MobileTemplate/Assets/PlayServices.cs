using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

//**import for save/load
 using GooglePlayGames;
 using GooglePlayGames.BasicApi;
 using GooglePlayGames.BasicApi.SavedGame;
 using UnityEngine.SocialPlatforms;
 //**end
 
public class PlayServices : MonoBehaviour
{
    public static PlayServices Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
		.EnableSavedGames()
		.Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        SignIn();
    }

    void SignIn()
    {
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("The user has been authenticated correctly.");
            }
            else
            {
                Debug.LogError("An error while authenticating.");
            }
        });
    }

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public static void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion /Leaderboards

	//true = wczytywanie, false = zapisywanie
	private bool isSaving;
	
	//otworzenie UI
	#region SaveGamesUI
	void ShowSelectUI() {
    uint maxNumToDisplay = 5;
    bool allowCreateNew = false;
    bool allowDelete = true;

    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    savedGameClient.ShowSelectSavedGameUI(
        "Select saved game",
        maxNumToDisplay,
        allowCreateNew,
        allowDelete,
        OnSavedGameSelected // callback
    );
}


	public void OnSavedGameSelected (SelectUIStatus status, ISavedGameMetadata game) {
		if (status == SelectUIStatus.SavedGameSelected) {
        // handle selected game save
		// OpenSavedGame(?); jaki filename
		} else {
        // handle cancel or error
		// ustawienie nowej gry od zera?
		}
	}
	#endregion SaveGamesUI
	
	//wczytywanie migawki
	#region LoadGameSnap
	void OpenSavedGame(string filename) {
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    savedGameClient.OpenWithAutomaticConflictResolution(
        filename,
        DataSource.ReadCacheOrNetwork,
        ConflictResolutionStrategy.UseLongestPlaytime, // strategia konfliktów
        OnSavedGameOpened // callback
    );
}

	public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
        // handle reading or writing of saved game.
			if(isSaving)
			{
				//zapisywanie danych
				//SaveGame(?)
			}else{
				//wczytywanie danych
				//LoadGameData(?)
			}
		} else {
        // handle error
		if(isSaving)
			{
				//brak zapisu?
			}else{
				//ustawienie nowej gry od zera?
			}
		}
	}
	#endregion LoadGameSnap
	
	//zapis migawki
	#region SaveGameSnap
	void SaveGame (ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime) {
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
    builder = builder
        .WithUpdatedPlayedTime(totalPlaytime) // metadane
        .WithUpdatedDescription("Saved game at " + DateTime.Now()); // metadane
    if (savedImage != null) {
        byte[] pngData = savedImage.EncodeToPNG();
        builder = builder.WithUpdatedPngCoverImage(pngData);  // metadane
    }
    SavedGameMetadataUpdate updatedMetadata = builder.Build();
    savedGameClient.CommitUpdate(
        game,
        updatedMetadata,
        savedData,
        OnSavedGameWritten // callback
    );
	}

	public void OnSavedGameWritten (SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
        // handle reading or writing of saved game.
		// zapis 
		} else {
        // handle error
		}
	}

	public Texture2D getScreenshot() { // zapis zdjęcia gry
    Texture2D screenShot = new Texture2D(1024, 700);
    screenShot.ReadPixels(
        new Rect(0, 0, Screen.width, (Screen.width/1024)*700), 0, 0);
    return screenShot;
}
	#endregion SaveGameSnap
	
	//wczytywanie stanu gry z migawki
	#region LoadGameState
	void LoadGameData (ISavedGameMetadata game) {
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    savedGameClient.ReadBinaryData(
        game,
        OnSavedGameDataRead // callback
    );
}

	public void OnSavedGameDataRead (SavedGameRequestStatus status, byte[] data) {
		if (status == SavedGameRequestStatus.Success) {
        // handle processing the byte array data
		// wczytanie danych do tablicy i przekazanie do gry, uruchomienie
		} else {
        // handle error
		// uruchomienie ze stanem zerowym?
		}
	}
	#endregion LoadGameState
	
	#region DeleteGameSnap
	void DeleteGameData (string filename) {
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    savedGameClient.OpenWithAutomaticConflictResolution(
        filename,
        DataSource.ReadCacheOrNetwork,
        ConflictResolutionStrategy.UseLongestPlaytime,
        DeleteSavedGame // callback
    );
	}

	public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game) {
		if (status == SavedGameRequestStatus.Success) {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.Delete(game);
		} else {
        // handle error
		}
	}
	#endregion DeleteGameSnap
}
