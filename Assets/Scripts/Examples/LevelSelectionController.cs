using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Level selection controller class that is added to a prefab of a level selection button.
/// </summary>
public class LevelSelectionController : MonoBehaviour {
    [SerializeField] private Button m_button;

    // Save level index here
    private int m_sceneIndex;


    /// <summary>
    /// Call this when creating an instance from the prefab (for each instance)
    /// </summary>
    /// <param name="sceneIndex">Scene index from LevelData to determine which scene to load</param>
    public void Init(int sceneIndex) {
        m_sceneIndex = sceneIndex;
    }


    /// <summary>
    /// Gets called when button is pressed
    /// </summary>
    private void ButtonOnClick() {
        if (m_sceneIndex > 0) {
            SceneManager.LoadSceneAsync(m_sceneIndex);
        }
    }


    private void Awake() {
        m_button.onClick.AddListener(ButtonOnClick);
    }
}

