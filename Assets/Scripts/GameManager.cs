using UnityEngine;
using System.Collections;
using System.Text;

public class GameManager : MonoBehaviour {


	float input;
	bool axisInUse = false;

	private static GameManager instance;
	public static GameManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 25;
		ActivateGPGS();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}

	void GetInput(){
		if (Input.GetAxisRaw("Vertical") == 0)
			axisInUse = false;
		
		if (Input.GetAxisRaw("Vertical") == 1) {
			if (axisInUse == false) {
				axisInUse = true;
				UIManager.Instance.UpdateScrollRect(true);
			}
		} else if (Input.GetAxisRaw("Vertical") == -1) {
			if (axisInUse == false) {
				axisInUse = true;
				UIManager.Instance.UpdateScrollRect(false);
			}
		}



//		input = Input.GetAxis("Vertical");
//		if(input > 0f){
//			UIManager.Instance.UpdateScrollRect(true);
//		}
//
//		if(input < 0f){
//			UIManager.Instance.UpdateScrollRect(false);
//		}
		
	}


	public static bool saveLocally = false;
	public static bool loadFinished = false;

	public static string saveName = "mixit";

	//SAVING DATA
//	private static DateTime gameStart;
//	private static ISavedGameMetadata gameMetaData;
//
	public static void ActivateGPGS(){
//		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//			// enables saving game progress.
//			.EnableSavedGames()
//			// registers a callback to handle game invitations received while the game is not running.
//			//			.WithInvitationDelegate(<callback method>)
//			// registers a callback for turn based match notifications received while the
//			// game is not running.
//			//			.WithMatchDelegate(<callback method>)
//			// require access to a player's Google+ social graph to sign in
//			.RequireGooglePlus()
//			.Build();
//
//		PlayGamesPlatform.InitializeInstance(config);
//		// recommended for debugging:
//		PlayGamesPlatform.DebugLogEnabled = true;
//		// Activate the Google Play Games platform
//		PlayGamesPlatform.Activate();
//
		SignIn();
//
		if(saveLocally)
			LoadLocally();
//
	}

	static void SetGameStart(){
//		gameStart = DateTime.Now;
	}

	static void SignIn(){
		SetGameStart();

		// authenticate user:
		Social.localUser.Authenticate((bool success) => {
			if(success){

				OpenSavedGame(saveName);
			} else {
				UIManager.Notify("Could not sign in. The game will be saved locally.");
				saveLocally = true;
			}
		});

	}

	// Two main method for saving and loading

	public static void Save(){

		UIManager.Notify("saving");
		string inventory = "";

//		foreach(Recipe recipe in BarManager.Instance.recipes){
//			inventory += "@" + recipe.cocktailName + "!" + recipe.success.ToString("0000") + "?" + recipe.tried.ToString("0000");
//		}
//
//		//		foreach(GameObject parcelGO in GameManager.Instance.garden){
//		//			inventory += "@" + parcelGO.name.Substring(parcelGO.name.Length-1, 1);
//		//			inventory += "ph:" + parcelGO.GetComponent<Parcel>().pH.ToString("0.0");
//		//			PlantPrefab pp = parcelGO.GetComponentInChildren<PlantPrefab>();
//		//			if(pp != null)
//		//				inventory+= "pt:" + pp.plant.plantType.ToString();
//		//		}
//
//		//		UIManager.Notify(inventory);
//
//		if(saveLocally){
//			SaveLocally(inventory);
//
//		} else {
//			OpenSavedGame(saveName);
//			byte[] savedData = StringToBytes(inventory);
//			//			UIManager.Notify("\n\n" + gameMetaData.TotalTimePlayed.ToString());
//			//			UIManager.Notify("\n Difference: \n" + DateTime.Now.ToString() + "vs" + gameStart.ToString());
//			TimeSpan totalPlaytime = (DateTime.Now - gameStart) + gameMetaData.TotalTimePlayed;
//			SetGameStart();
//			//			UIManager.Notify("updated played time\n" + totalPlaytime.ToString());
//			SaveGame(gameMetaData, savedData, totalPlaytime);
//		}

	}

	static void LoadData(string data){
		UIManager.Notify("Loading");
		string[] ListRecipes;
		ListRecipes = data.Split("@"[0]);

		//		string coin = listParcel[0].Substring(1);

		for(int s = 0; s < ListRecipes.Length; s++){

			if(ListRecipes[s] != null && ListRecipes[s] != ""){

				string recipe = ListRecipes[s];
				string recipeName = recipe.Substring(0, recipe.Length - 10);
				//				Debug.Log(recipeName);
				int suc = int.Parse(recipe.Substring(recipe.Length - 9, 4));
				//				Debug.Log(suc);
				int tri = int.Parse(recipe.Substring(recipe.Length - 4, 4));
				//				Debug.Log(tri);

				BarManager.Instance.SetStat(recipeName, suc, tri);
				//				if(s-1 >= GameManager.Instance.garden.Count){
				//					GameManager.Instance.gardenSize++;
				//					GameManager.Instance.CreateParcel(GameManager.Instance.gardenSize);
				//				}
				//
				//
				//				GameManager.Instance.garden[s-1].GetComponent<Parcel>().SetpH(float.Parse(ph));
				//
				//				if(listParcel[s].Contains("pt:")){
				//
				//					GameManager.Instance.currentParcelGO = GameManager.Instance.garden[s-1];
				//					GameManager.Instance.currentParcelGO.GetComponent<Parcel>().SetPlant(GardenManager.Instance.PlantFromString(listParcel[s].Substring(10)));
				//				}
			}

		}
		loadFinished = true;
		TutorialManager.Instance.ShowNextTip();
	}

	// Helpers for loading and saving on GPGS

	public static void ShowSelectUI() {
		uint maxNumToDisplay = 5;
		bool allowCreateNew = false;
		bool allowDelete = true;

//		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
//		savedGameClient.ShowSelectSavedGameUI("Select saved game",
//			maxNumToDisplay,
//			allowCreateNew,
//			allowDelete,
//			OnSavedGameSelected);
	}

//	public static void OnSavedGameSelected (SelectUIStatus status, ISavedGameMetadata game) {
//		if (status == SelectUIStatus.SavedGameSelected) {
//			// handle selected game save
//			//			gameMetaData = game;
//			//			OpenSavedGame("garden");
//		} else {
//			// handle cancel or error
//		}
//	}

	static void OpenSavedGame(string filename) {
		UIManager.Notify("opening saved game");
//		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
//		savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
//			ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
	}

//	public static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game) {
//		if (status == SavedGameRequestStatus.Success) {
//			gameMetaData = game;
//			UIManager.Notify("Save was opened successfully");
//
//			if(!loadFinished)
//				LoadGameData(gameMetaData);
//		} else {
//			UIManager.Notify("Save couldn't be opened");
//		}
//	}
//
//	static void LoadGameData (ISavedGameMetadata game) {
//
//		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
//		savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
//
//	}
//
//	public static void OnSavedGameDataRead (SavedGameRequestStatus status, byte[] data) {
//
//		if (status == SavedGameRequestStatus.Success) {
//			// handle processing the byte array data
//			LoadData(ByteToString(data));
//		} else {
//			// handle error
//			UIManager.Notify("Could not read saved data");
//		}
//	}
//
//
//
//	static void SaveGame (ISavedGameMetadata gameMetaData, byte[] savedData, TimeSpan totalPlaytime) {
//		ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
//
//		Texture2D savedImage = getScreenshot();
//
//		SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
//		builder = builder
//			.WithUpdatedPlayedTime(totalPlaytime)
//			.WithUpdatedDescription("Saved game at " + DateTime.Now);
//		if (savedImage != null) {
//			// This assumes that savedImage is an instance of Texture2D
//			// and that you have already called a function equivalent to
//			// getScreenshot() to set savedImage
//			// NOTE: see sample definition of getScreenshot() method below
//			byte[] pngData = savedImage.EncodeToPNG();
//			builder = builder.WithUpdatedPngCoverImage(pngData);
//		}
//		SavedGameMetadataUpdate updatedMetadata = builder.Build();
//		savedGameClient.CommitUpdate(gameMetaData, updatedMetadata, savedData, OnSavedGameWritten);
//	}
//
//	public static void OnSavedGameWritten (SavedGameRequestStatus status, ISavedGameMetadata game) {
//		if (status == SavedGameRequestStatus.Success) {
//			//			gameMetaData = game;
//			UIManager.Notify("Cloud saved");
//		} else {
//			//			UIManager.Notify(status.ToString());
//		}
//	}

	public static Texture2D getScreenshot() {
		// Create a 2D texture that is 1024x700 pixels from which the PNG will be
		// extracted
		Texture2D screenShot = new Texture2D(1024, 700);

		// Takes the screenshot from top left hand corner of screen and maps to top
		// left hand corner of screenShot texture
		screenShot.ReadPixels(
			new Rect(0, 0, Screen.width, (Screen.width/1024)*700), 0, 0);
		return screenShot;
	}

	static byte[] StringToBytes(string text){
		return Encoding.UTF8.GetBytes(text);
	}

	static string ByteToString(byte[] bytes){
		return Encoding.UTF8.GetString(bytes);
	}


	// PlayerPrefs used for saving

	static void SaveLocally(string inventory){
		UIManager.Notify("saving locally");
		PlayerPrefs.SetString("inventory", inventory);
		PlayerPrefs.Save();
	}

	public static void LoadLocally(){
		if(PlayerPrefs.GetString("inventory") != null){
			LoadData(PlayerPrefs.GetString("inventory"));
		}
	}

	public static void DeletePlayerPrefs(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		Debug.Log("PlayerPrefs DELETED");
	}


	static void UnlockAchivement(string achievement, float completion = 100f){
		Social.ReportProgress(achievement, completion, (bool success) => {
			if(success){
				UIManager.Notify("Achievement unlocked: " + achievement);
			}
		});
	}
}
