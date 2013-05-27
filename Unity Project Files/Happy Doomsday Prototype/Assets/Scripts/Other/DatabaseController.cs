using UnityEngine;
using System.Collections;
 
public class DatabaseController : MonoBehaviour
{
	
	private string gameCenterID;
    private string secretKey = "DOOOOOOOOOOOOOOOMSDAAAAAAAAAY"; // Edit this value and make sure it's the same as the one stored on the server
	
	private string addScoreURL = "http://doomsdaydevelopment.com/game/addscore.php"; //be sure to add a ? to your url
    private string highscoreURL = "http://doomsdaydevelopment.com/game/display.php";
	private string dataminingBuildingURL = "http://doomsdaydevelopment.com/game/dataminingbuilding.php";
	//public string addScoreURL = "http://localhost/Doomsday/addscore.php"; //be sure to add a ? to your url
    //public string highscoreURL = "http://localhost/Doomsday/display.php";
	
	public string GameCenterID {
		get { return gameCenterID; }
		set { gameCenterID = value; }
	}
 
    void Start()
    {
        //StartCoroutine(GetScores());
		//GameCenterID
    }
 
    // remember to use StartCoroutine when calling this function!
    public IEnumerator PostScore( int score )
    {
		
		int id = GameObject.Find( "ResourceManager" ).GetComponent<ResourceManagerScript>().ID;
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        
		string hash = Md5Sum(id.ToString() + score.ToString() + secretKey);
 
        //string post_url = addScoreURL + "?profile_id=" + WWW.EscapeURL(id) + "&score=" + score + "&hash=" + hash;
		string post_url = addScoreURL + "?profile_id=" + id + "&score=" + score + "&hash=" + hash;
		
 
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done
 
        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        } else {
			
			print( hs_post.text );
			
			int cack = System.Convert.ToInt32( hs_post.text );
			
			if ( cack != id )
			{
				GameObject.Find( "ResourceManager" ).GetComponent<ResourceManagerScript>().ID = cack;
			}
			
		}
	}
	
	public IEnumerator PostDataminingBuilding( string action, string building )
	{
		building = building.Split( '(' )[0];
		
		int id = GameObject.Find( "ResourceManager" ).GetComponent<ResourceManagerScript>().ID;
		
		string hash = Md5Sum( id.ToString() + action + building + secretKey );
		
		print( building );
		string post_url = dataminingBuildingURL + "?profile_id=" + id
						+ "&action=" + WWW.EscapeURL(action)
						+ "&building=" + WWW.EscapeURL(building)
						+ "&hash=" + hash;
		
		WWW dmb_post = new WWW( post_url );
		yield return dmb_post;
		
		if( dmb_post.error != null ) {
			print("There was an error posting the high score: " + dmb_post.error);
        } else {
			
			
			print( dmb_post.text );
			
			int cack = System.Convert.ToInt32( dmb_post.text );
			
			if ( cack != id )
			{
				GameObject.Find( "ResourceManager" ).GetComponent<ResourceManagerScript>().ID = cack;
			}
		}
	}
 
    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    public IEnumerator GetScores()
    {
        print ( "Loading Scores" );
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;
 
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            print ( hs_get.text ); // this is a GUIText that will display the scores in game.
        }
    }
	
	private string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);
	 
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
	 
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
	 
		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}
	 
		return hashString.PadLeft(32, '0');
	}
 
}