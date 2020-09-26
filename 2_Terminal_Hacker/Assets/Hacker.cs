using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

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

        if (input == "1") {
            level = 1;
            password = "books";
            StartGame();
        }
        else if (input == "2") {
            level = 2;
            password = "prisoner";
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
        Terminal.WriteLine("You have chosen level " + level);
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