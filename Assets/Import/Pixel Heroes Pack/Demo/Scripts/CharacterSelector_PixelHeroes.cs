using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector_PixelHeroes : MonoBehaviour {

    // Create an array of arrays
    [System.Serializable]
    public class ColorsCharacters
    {
        public GameObject[] color;
        public GameObject instructionText;
    }

    [SerializeField] public ColorsCharacters[] characters;

    private int m_index = 0;
    private int m_maxIndex;
    private GameObject[] m_currentCharacters;
    private Vector3[] spawnPositions;

    // Use this for initialization
    void Start() {
        m_maxIndex = characters.Length;
        int MAX_CHARACTERS = 3;
        spawnPositions = new Vector3[MAX_CHARACTERS];
        spawnPositions[0] = new Vector3(0.0f, 0.0f, 0.0f);
        spawnPositions[1] = new Vector3(-2.0f, 0.0f, 0.0f);
        spawnPositions[2] = new Vector3(2.0f, 0.0f, 0.0f);
        m_currentCharacters = new GameObject[MAX_CHARACTERS];

        for (int i = 0; i < characters[0].color.Length; i++)
        {
            m_currentCharacters[i] = Instantiate(characters[0].color[i]);
            m_currentCharacters[i].transform.parent = transform;
            m_currentCharacters[i].transform.localPosition = spawnPositions[i];
        }
    }

    // Update is called once per frame
    void Update() {
        // Change character
        if (Input.GetKeyDown("1"))
            changeCharacter(-1);

        if (Input.GetKeyDown("2"))
            changeCharacter(1);
    }

    private void changeCharacter(int changeValue) {
        // Destroy previous character
        for (int i = 0; i < characters[m_index].color.Length; i++)
        {
            Destroy(m_currentCharacters[i]);
        }

        // Hide previous special instruction text
        if (characters[m_index].instructionText)
            characters[m_index].instructionText.SetActive(false);

        // Increase index
        m_index += changeValue;
        // Check boundaries
        m_index = m_index < m_maxIndex ? m_index : 0;
        m_index = m_index < 0 ? m_maxIndex-1 : m_index;

        // Spawn next character
        for (int i = 0; i < characters[m_index].color.Length; i++)
        {
            m_currentCharacters[i] = Instantiate(characters[m_index].color[i]);
            m_currentCharacters[i].transform.parent = transform;
            m_currentCharacters[i].transform.localPosition = spawnPositions[i];
        }

        // Show next special instruction text
        if (characters[m_index].instructionText)
            characters[m_index].instructionText.SetActive(true);
    }
}
