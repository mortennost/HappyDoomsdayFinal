using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class SaveGameManagerScript : MonoBehaviour
{
	public GameObject[] prefabs;
		
	private bool hasLoaded;
	private bool isDone;
	private Stack<GameObject> theStack;
		
	public enum InstanceType
	{ 
		// Turrets
		Crossbow, Flamethrower, Freezer, Scarecrow,
		
		// UNITS - Scientist
		Krueger, Lippschultz, VonSchnauser,
		
		// UNITS - Other
		Bullfrog, Gorilla, Raptor, Scorpion,
		
		// UNITS - Soldier
		Boomer, Cannoneer, Jarhead,
		
		// BULDINGS - Harvesters
		Chicken_Coop, Field, Rain_Collector, Well,
		
		// BUILDINGS - Utility
		Workshed, Wall,
		
		LAST_ENTRY
	};
		
	public struct InstanceInfo
	{
		public InstanceType type;
		public Vector3 position;
		public int level;
		public int health;
		public int maxHealth;
	};
	Stack<InstanceInfo> instanceInfoStack;
	
	// Use this for initialization
	void Start()
	{
		theStack = new Stack<GameObject>();
		instanceInfoStack = new Stack<InstanceInfo>();
		hasLoaded = true;
		isDone = false;
		
		hasLoaded = Load();
	}
	
	void OnApplicationQuit()
	{
		Save();
	}
	
	// Update is called once per frame
	void Update()
	{
		if ( hasLoaded && !isDone )
		{
			GameObject[] gameObjects = theStack.ToArray();
			InstanceInfo[] infoObjects = instanceInfoStack.ToArray();
			
			for (int count = 0; count < gameObjects.Length; ++count)
			{
				StructureStateManager ssm = gameObjects[count].GetComponent<StructureStateManager>();
				if (ssm != null) 
				{
					ssm.ChangeState( new AIStateStructureOperational( gameObjects[count] ) );
				}
				
				gameObjects[count].GetComponent<Level>().SetLevel(infoObjects[count].level);
				gameObjects[count].GetComponent<Health>().CurHealth = infoObjects[count].health;
				gameObjects[count].GetComponent<Health>().MaxHealth = infoObjects[count].maxHealth;
				
				StructureScript structureScript = gameObjects[count].GetComponent<StructureScript>();
				
				if (structureScript != null)
				{
					Vector3 cornerPosition = gameObjects[count].transform.position;
					cornerPosition.x -= structureScript.xSize / 2.0f;
					cornerPosition.z -= structureScript.zSize / 2.0f;
					GameObject.Find("Grid").GetComponent<GridScript>().DirGraph.ToggleTraversable(cornerPosition, structureScript.xSize, structureScript.zSize, true);
				}
			}
			
			isDone = true;
		}
		if (Input.GetKeyDown(KeyCode.F6))
		{
			Save();
		}
		
		if (Input.GetKeyDown(KeyCode.F7))
		{
			hasLoaded = Load();
		}
	}
	
	void Save()
	{
		ResourceManagerScript resources = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>();
		
		int foodCurrent = resources.GetFood();
		int foodMax = resources.GetMaxFood();
		
		int waterCurrent = resources.GetWater();
		int waterMax = resources.GetMaxWater();
		
		int scrapCurrent = resources.GetScrap();
		int scrapMax = resources.GetMaxScrap();
		
		int experienceCurrent = resources.GetExperience();
		int experienceMax = resources.GetMaxExperience();
		
		int levelCurrent = resources.GetLevel();
		int levelMax = resources.GetMaxLevel();
		
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		
		XmlWriter writer = XmlWriter.Create(Application.persistentDataPath +  "/save.xml", settings);
		
		writer.WriteStartDocument();
		
			writer.WriteStartElement("savegame");
		
				writer.WriteStartElement("food");
					writer.WriteAttributeString("current", foodCurrent.ToString());
					writer.WriteAttributeString("max", foodMax.ToString());
				writer.WriteEndElement();
		
				writer.WriteStartElement("water");
					writer.WriteAttributeString("current", waterCurrent.ToString());
					writer.WriteAttributeString("max", waterMax.ToString());
				writer.WriteEndElement();
		
				writer.WriteStartElement("scrap");
					writer.WriteAttributeString("current", scrapCurrent.ToString());
					writer.WriteAttributeString("max", scrapMax.ToString());
				writer.WriteEndElement();
			
				writer.WriteStartElement("experience");
					writer.WriteAttributeString("current", experienceCurrent.ToString());
					writer.WriteAttributeString("max", experienceMax.ToString());
				writer.WriteEndElement();
		
				writer.WriteStartElement("invasiontimer");
					writer.WriteAttributeString("next", GameObject.Find("GameManager").GetComponent<GameManagerScript>().timeUntilNextInvasion.ToString());
				writer.WriteEndElement();
		
				writer.WriteStartElement("compoundlevel");
					writer.WriteAttributeString("current", levelCurrent.ToString());
					writer.WriteAttributeString("max", levelMax.ToString());
				writer.WriteEndElement();
		
				GameObject playerHouse = GameObject.FindGameObjectWithTag("Playerhouse");
				writer.WriteStartElement("playerhouse");
					writer.WriteAttributeString("level", playerHouse.GetComponent<Level>().GetLevel().ToString());
					writer.WriteAttributeString("health", playerHouse.GetComponent<Health>().getHealth().ToString());
				writer.WriteEndElement();
		
				GameObject[] structures = GameObject.FindGameObjectsWithTag("Structure");
				foreach (GameObject structure in structures)
				{
					writer.WriteStartElement("structure");
						writer.WriteAttributeString("prefabName", structure.name.ToString().Split('(')[0]);
						writer.WriteAttributeString("xPos", structure.transform.position.x.ToString());
						writer.WriteAttributeString("yPos", structure.transform.position.y.ToString());
						writer.WriteAttributeString("zPos", structure.transform.position.z.ToString());
						writer.WriteAttributeString("level", structure.GetComponent<Level>().GetLevel().ToString());
						writer.WriteAttributeString("health", structure.GetComponent<Health>().getHealth().ToString());
						writer.WriteAttributeString("maxHealth", structure.GetComponent<Health>().getMaxHealth().ToString());
					writer.WriteEndElement();
				}
		
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject enemy in enemies)
				{
					writer.WriteStartElement("unit");
						writer.WriteAttributeString("prefabName", enemy.name.ToString().Split('(')[0]);
						writer.WriteAttributeString("xPos", enemy.transform.position.x.ToString());
						writer.WriteAttributeString("yPos", enemy.transform.position.y.ToString());
						writer.WriteAttributeString("zPos", enemy.transform.position.z.ToString());
						writer.WriteAttributeString("level", enemy.GetComponent<Level>().GetLevel().ToString());
						writer.WriteAttributeString("health", enemy.GetComponent<Health>().getHealth().ToString());
						writer.WriteAttributeString("maxHealth", enemy.GetComponent<Health>().getMaxHealth().ToString());
					writer.WriteEndElement();
				}
		
			writer.WriteEndDocument();
		
		writer.Flush();
		writer.Close();
	}
	
	bool Load()
	{
		ResourceManagerScript rsm = GameObject.Find("ResourceManager").GetComponent<ResourceManagerScript>();
		
		try
		{
			XmlReader reader = XmlReader.Create(Application.persistentDataPath + "/save.xml");

			while (reader.Read())
			{			
				if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("food"))
				{
					reader.MoveToNextAttribute();
					rsm._foodCount = System.Convert.ToInt32(reader.Value);
					
					reader.MoveToNextAttribute();
					rsm._maxFoodCount = System.Convert.ToInt32(reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("water"))
				{
					reader.MoveToNextAttribute();
					rsm._waterCount = System.Convert.ToInt32(reader.Value);
	
					reader.MoveToNextAttribute();
					rsm._maxWaterCount = System.Convert.ToInt32(reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("scrap"))
				{
					reader.MoveToNextAttribute();
					rsm._scrapCount = System.Convert.ToInt32(reader.Value);
					
					reader.MoveToNextAttribute();
					rsm._maxScrapCount = System.Convert.ToInt32(reader.Value);;
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("experience"))
				{
					reader.MoveToNextAttribute();
					rsm._currentExperience = System.Convert.ToInt32(reader.Value);
	
					reader.MoveToNextAttribute();
					rsm._maxExperience = System.Convert.ToInt32(reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("invasiontimer"))
				{
					reader.MoveToNextAttribute();
					GameObject.Find("GameManager").GetComponent<GameManagerScript>().timeUntilNextInvasion = (float) System.Convert.ToDouble (reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("compoundlevel"))
				{
					reader.MoveToNextAttribute();
					rsm._currentLevel = System.Convert.ToInt32(reader.Value);
					
					reader.MoveToNextAttribute();
					rsm._maxLevel = System.Convert.ToInt32(reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("playerhouse"))
				{
					reader.MoveToNextAttribute();
					GameObject.FindGameObjectWithTag("Playerhouse").GetComponent<Level>().SetLevel(System.Convert.ToInt32(reader.Value));
					
					reader.MoveToNextAttribute();
					
					GameObject.FindGameObjectWithTag("Playerhouse").GetComponent<Health>().CurHealth = System.Convert.ToInt32(reader.Value);
				}
				
				else if (reader.NodeType == XmlNodeType.Element && (reader.Name.Equals("structure") || reader.Name.Equals ("unit")))
				{
					reader.MoveToNextAttribute();
					string prefabName = reader.Value;
					Debug.Log("Attempting to load structure: " + reader.Value);
					
					reader.MoveToNextAttribute();
					double xPos = System.Convert.ToDouble(reader.Value);
					
					reader.MoveToNextAttribute();
					double yPos = System.Convert.ToDouble(reader.Value);
					
					reader.MoveToNextAttribute();
					double zPos = System.Convert.ToDouble(reader.Value);
					
					reader.MoveToNextAttribute();
					int level = System.Convert.ToInt32(reader.Value);
					
					reader.MoveToNextAttribute();
					int health = System.Convert.ToInt32(reader.Value);
					
					reader.MoveToNextAttribute();
					int maxHealth = System.Convert.ToInt32(reader.Value);
					
					InstanceInfo instanceInfo = new InstanceInfo();
					instanceInfo.position = new Vector3((float)xPos, (float)yPos, (float)zPos);
					instanceInfo.level = level;
					instanceInfo.health = health;
					
					for (InstanceType it = InstanceType.Crossbow; it < InstanceType.LAST_ENTRY; ++it)
					{
						string itAsString = it.ToString().Replace('_', ' ');
						
						if (itAsString.Equals(prefabName))
						{
							instanceInfo.type = it;
							
							it = InstanceType.LAST_ENTRY;
							
							LoadInstance(instanceInfo);
						}
					}
				}
			}
		}
		
		catch (FileNotFoundException fnfex)
		{
			Debug.Log("Failed to load save.xml: " + fnfex.StackTrace);
		}
		
		return true;
	}
	
	public void LoadInstance(InstanceInfo instanceInfo)
	{
		instanceInfoStack.Push (instanceInfo);
		
		GameObject prefabObject = (GameObject) Instantiate(prefabs[(int)instanceInfo.type], instanceInfo.position, prefabs[(int)instanceInfo.type].transform.localRotation);
		
		if (prefabObject == null)
		{
			Debug.Log ("Failed to instantiate object: " + prefabObject.ToString());
		}
		else theStack.Push(prefabObject);
	}
}