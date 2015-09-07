using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
	[Header("Chunk prefabs")]
	public List<GameObject> m_chunks = new List<GameObject>();
	
	//screen width in game unit
	private float m_screenWidthGameUnits;
	
	private List<GameObject> m_chunkClones = new List<GameObject>();
	
	void Awake()
	{
		this.m_screenWidthGameUnits = this.getHalfScreenWidth();
	}
	
	void Start()
	{
		//maak alle chunks aan
		//sorteer de nieuwe chunk zo dat ze het scherm volledig vullen
		for (int i = 0; i < 3; i++)
		{
			m_chunkClones.Add (getRandomChunk(Vector3.zero));
		}
		
		
		for (int j = 0; j < m_chunkClones.Count; j++)
		{
			m_chunkClones[j].transform.position = new Vector3(j * getChunkWidth(m_chunkClones[j]), 0);
		}
		
		
		m_chunkClones[0].transform.position = new Vector3(0 - m_screenWidthGameUnits, 0);
		
		Vector3 eersteChunkPos = m_chunkClones[0].transform.position;
		
		for (int k = 0; k < m_chunkClones.Count; k++)
		{
			m_chunkClones[k].transform.position = new Vector3(eersteChunkPos.x + getChunkWidth(m_chunkClones[k])*k ,0);
		}
	}
	void Update()
	{
		//check of alle chunks nog binne scherm zijn
		//delete de chunks die buiten het scherm zijn
		//beweeg alle chunks
	}
	
	/// <summary>
	/// Sorteer de chunk achter elkaar
	/// </summary>
	/// <param name="_chunks">List van chunks die gesorteerd moeten worden</param>
	private void sortChunks(List<GameObject> _chunks)
	{
		if (_chunks.Count<1)
		{
			Debug.Log("Error sort chunk! list heeft geen elementen");
			return;
		}
		// var l_offset = m_screenWidthGameUnits;
		//get first chunk position
		var l_firstChunkV3 = _chunks[0].transform.position;
		//sort chunks
		for (int i = 0; i < _chunks.Count; i++)
		{
			_chunks[i].transform.position = new Vector3(l_firstChunkV3.x + (getChunkWidth(_chunks[i]) * i), 0);
		}
	}
	
	/// <summary>
	/// Check of de chunk aan de linker kant uit het scherm is
	/// </summary>
	/// <param name="_chunk">chunk die we gaan checken</param>
	/// <returns>True = uit scherm, False = binnen scherm</returns>
	private bool checkBoundsChunk(GameObject _chunk)
	{
		if (_chunk.transform.position.x < 0 - (m_screenWidthGameUnits + getChunkWidth(_chunk) / 2))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	/// <summary>
	/// Beweeg een chunk
	/// </summary>
	/// <param name="_chunk">Chunk dat bewongen moet worden</param>
	/// <param name="_speed">Snelheid van bewegen</param>
	private void moveChunk(GameObject _chunk, float _speed)
	{
		_chunk.transform.position -= new Vector3(_speed * Time.deltaTime, 0);
	}
	
	/// <summary>
	/// Haal een random chunk op
	/// </summary>
	/// <param name="_position">positie van game object</param>
	/// <returns>chunk clone gameobject</returns>
	private GameObject getRandomChunk(Vector3 _position)
	{
		
		return spawnChunk(m_chunks[Random.Range(0, m_chunks.Count)], _position);
	}
	
	/// <summary>
	/// Spawn een chunk
	/// </summary>
	/// <param name="_chunk">chunk game object</param>
	/// <param name="_position">position van game object</param>
	/// <returns>chunk clone</returns>
	private GameObject spawnChunk(GameObject _chunk, Vector3 _position)
	{
		return (GameObject)Instantiate(_chunk, _position, Quaternion.identity);
	}
	
	/// <summary>
	/// Haal de breete op van de chunk
	/// </summary>
	/// <param name="_chunk">chunk game object</param>
	/// <returns>breete in float</returns>
	private float getChunkWidth(GameObject _chunk)
	{
		return _chunk.GetComponent<BoxCollider2D>().size.x;
	}
	
	/// <summary>
	/// Haal de helft van de schermbreete op in game units
	/// </summary>
	/// <returns>game with in game units</returns>
	private float getHalfScreenWidth()
	{
		//orthoWidth = orthographicSize / screenHeight * screenWidth;
		return (Camera.main.orthographicSize / Camera.main.pixelHeight * Camera.main.pixelWidth);
	}
}