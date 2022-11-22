using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Adds the UnityEngine.UI namespace so we have access to the Text variable type.
using TMPro;
using System;
using UnityEngine.SceneManagement; // Adds the SceneManagement namespace with the using keyword, which handles all scene-related logic like creating loading scenes.

public class GameBehavior : MonoBehaviour
{
    private int _playerHP = 10; // Adds a variable to store how many lives the player has left.

    public int MaxItems = 4; // Adds a new public variable for the max number of items in the level. 

    public TMP_Text HealthText; // Adds a new text variable which we can connect in the Inspector panel.
    public TMP_Text ItemText; // Adds a new text variable which we can connect in the Inspector panel.
    public TMP_Text ProgressText; // Adds a new text variable which we can connect in the Inspector panel.

    public Button WinButton; // Adds a UI button variable to connect to our Win Condition button in the Hierachy.

    void Start() // Uses the Start method to set the initial values of our health and items text using the += operator.
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
    }

    private int _itemsCollected = 0; // Every time an item is collected, we update the text property of ItemText to show the updated items count.

    public int Items // Adds a new public variable called Items with get and set properties.
    {
        get // Uses the get property to return the value stored in _itemsCollected whenever Items are accessed from an outside class.
        {
            return _itemsCollected;
        }

        set
        {
            _itemsCollected = value;

            ItemText.text = "Items Collected:" + Items; // Uses the set property to assign _itemsCollected to the new value of Items whenever it's updated, with an added Debug.LogFormat() call to print out the modified value of _itemsCollected.

            if(_itemsCollected >= MaxItems) // We declare an if statement in the set property of _itemsCollected. If the player has gathered more than or equal to MaxItems, they've won, and ProgressText.text is updated. Otherwise, ProgressText.text shows how many items are still left to collect.
            {
                ProgressText.text = "You've found all the items!";

                WinButton.gameObject.SetActive(true); // Since we set the Win Condition button as Hidden when the game starts, we reactivate it when the game is won.

                Time.timeScale = 0f; // Sets Time.timeScale to 0 to pause the game when the win screen is displayed, which disables any input or movement.
            }

            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more to go!";
            }

            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }

    public int HP // Sets up a public variable called HP with get and set properties to complement the private _playerHP backing variable.
    {
        get
        {
            return _playerHP;
        }

        set
        {
            _playerHP = value;

            HealthText.text = "Player Health: " + HP; // Every time the player's health is damaged, which we'll cover in the next chapter, we update the text property of HealthText with the new value.
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0); // Creates a new method called RestartScene and call LoadScene() when the win screen button is clicked: LoadScene() takes in a scene index as an int parameter. Because there is only one scene in our project, we use index 0 to restart the game from the beginning.

        Time.timeScale = 1f; // Resets Time.timeScale to the default value of 1 so that when the scene restarts, all controls and behaviors will be able to execute again.
    }
}
