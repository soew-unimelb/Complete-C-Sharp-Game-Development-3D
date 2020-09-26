using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game configuration data
    string[] level1Passwords = { "books", "aisle", "self", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };

    // Game state
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string password;

    void Start() {
        ShowMainMenu();
    }

    void Update() { }

    void ShowMainMenu() {

        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");
    }
     
    void OnUserInput(string input) {

        if (input == "menu") {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu) {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password) {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input) {

        bool isValidLevel = (input == "1" || input == "2");
        if (isValidLevel) {
            level = int.Parse(input);
            StartGame();
        }
        else if (input == "007") {
            Terminal.WriteLine("Please select a level, Mr.Bond.");
        }
        else {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    void StartGame() {

        currentScreen = Screen.Password;
        switch (level) {
            case 1:
                password = level1Passwords[3];
                break;
            case 2:
                password = level2Passwords[2];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        Terminal.ClearScreen();
        Terminal.WriteLine("Please enter your password: ");
    }

    void CheckPassword(string input) {

        if (input == password) {
            Terminal.WriteLine("Congradulation!");
        }
        else {
            Terminal.WriteLine("Try again.");
        }
    }
}