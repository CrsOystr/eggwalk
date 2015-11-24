using UnityEngine;

public class ScreenshotMovie : MonoBehaviour
{
	// The folder we place all screenshots inside.
	// If the folder exists we will append numbers to create an empty folder.
	public string folder = "ScreenshotMovieOutput";
	public int frameRate = 30;
	public int sizeMultiplier = 1;
	public bool recording = false;
	
	private string realFolder = "";
	
	void Start()
	{
	
	}

	private void StartRecording()
	{
		recording = true;
		// Set the playback framerate!
		// (real time doesn't influence time anymore)
		Time.captureFramerate = frameRate;

		// Find a folder that doesn't exist yet by appending numbers!
		realFolder = folder;
		int count = 1;
		while (System.IO.Directory.Exists(realFolder))
		{
			realFolder = folder + count;
			count++;
		}
		// Create the folder
		System.IO.Directory.CreateDirectory(realFolder);
	}
	
	void Update()
	{
		if (!recording && Input.GetKeyDown("space"))
			StartRecording ();
		// name is "realFolder/shot 0005.png"
		else if(recording && Input.GetKeyDown("space"))
			recording = false;
		else if(recording){
			var name = string.Format("{0}/shot {1:D04}.png", realFolder, Time.frameCount);
		
			// Capture the screenshot
			Application.CaptureScreenshot(name, sizeMultiplier);
		}
	}
}