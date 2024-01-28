using System;

class RoomGame
{
    static int playerHealth = 100;
    static int wrongAnswersCount = 0;

    static void DisplayOptions(string[] options)
    {
        foreach (var option in options)
        {
            Console.WriteLine(option);
        }
    }

    static string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine().ToUpper();
    }

    static void LookAround()
    {
        Console.WriteLine("\nAs you look around, you find a dusty bookshelf.");
        Console.WriteLine("The books seem old and worn. Some titles catch your eye.");

        Console.WriteLine("A. Take a closer look at the books");
        Console.WriteLine("B. Go back");

        string lookAroundChoice = GetUserInput("Enter the letter of your choice: ");

        if (lookAroundChoice == "A")
        {
            ExploreBookshelf();
        }
        else
        {
            Console.WriteLine("You decide to leave the bookshelf.");
        }
    }

    static void ExploreBookshelf()
    {
        Console.WriteLine("\nYou examine the bookshelf and discover a book with a strange symbol on its cover.");

        Console.WriteLine("A. Take the book");
        Console.WriteLine("B. Leave the bookshelf");

        string takeBookChoice = GetUserInput("Enter the letter of your choice: ");

        if (takeBookChoice == "A")
        {
            TakeBook();
        }
        else
        {
            Console.WriteLine("You decide not to take the book. It might be important, though.");
        }
    }

    static void TakeBook()
    {
        Console.WriteLine("\nYou take the book with the strange symbol. It might be important.");
        Console.WriteLine("As you open the book, you find a strange symbol and a riddle inside.");

        Console.WriteLine("A. Read the riddle");
        Console.WriteLine("B. Leave the book");

        string readRiddleChoice = GetUserInput("Enter the letter of your choice: ");

        if (readRiddleChoice == "A")
        {
            PlayRiddleGame();
        }
        else
        {
            Console.WriteLine("You decide to leave the book. The secrets of the room remain hidden.");
        }
    }

    static void PlayRiddleGame()
    {
        Console.WriteLine("\nYou decide to read the mysterious riddle:");
        Console.WriteLine("I fly without wings, I cry without eyes. Wherever I go, darkness follows me. What am I?");
        Console.WriteLine("(Contains letter 'B')");
        Console.WriteLine("A) Cloud");
        Console.WriteLine("B) Bat");
        Console.WriteLine("C) Moon");
        Console.WriteLine("D) Night");
        Console.WriteLine("E) Ghost");

        string playerAnswer = GetUserInput("Enter the letter of your choice: ");

        if (playerAnswer == "B")
        {
            Console.WriteLine("\nCongratulations! You gave the correct answer.");
            Console.WriteLine("You have WON the game!");

            Console.WriteLine("\nYou sense another riddle awaits you...");
            PlayNextRiddle();
        }
        else
        {
            HandleIncorrectAnswer();
        }
    }

    static void PlayNextRiddle()
    {
        Console.WriteLine("\nYou found an old parchment with another riddle inscribed on it:");
        Console.WriteLine("What has keys but can't open locks?");
        Console.WriteLine("(Contains letter 'P')");
        Console.WriteLine("A) Piano");
        Console.WriteLine("B) Treasure chest");
        Console.WriteLine("C) Calculator");
        Console.WriteLine("D) Map");
        Console.WriteLine("E) Secret");

        string playerAnswer = GetUserInput("Enter the letter of your choice: ");

        if (playerAnswer == "A")
        {
            Console.WriteLine("\nCongratulations! You solved the second riddle.");
            Console.WriteLine("You are a master of riddles!");

            Console.WriteLine("\nThe game ends here.");
            return;
        }
        else
        {
            HandleIncorrectAnswer();
        }
    }

    static void HandleIncorrectAnswer()
    {
        Console.WriteLine("Incorrect answer. An NPC in the room attacks you!");

        playerHealth -= 50;
        wrongAnswersCount++;

        if (playerHealth <= 0 || wrongAnswersCount >= 2)
        {
            Console.WriteLine("You have been killed. Game over.");
        }
        else
        {
            Console.WriteLine($"You have {playerHealth} health remaining.");
            PlayRiddleGame();
        }
    }

    static void Main()
    {
        Console.WriteLine("You find yourself inside a mysterious room.");
        Console.WriteLine("This room seems to hold many secrets...");

        bool playAgain = true;

        while (playAgain)
        {
            playerHealth = 100;
            wrongAnswersCount = 0;

            while (true)
            {
                Console.WriteLine("\nChoose an action:");
                string[] mainOptions = { "1. Look around", "2. Take item", "3. Read riddle", "4. Quit" };
                DisplayOptions(mainOptions);

                string userChoice = GetUserInput("\nEnter the number of your choice: ");

                switch (userChoice)
                {
                    case "1":
                        LookAround();
                        break;

                    case "2":
                        Console.WriteLine("\nYou take a closer look at the mysterious object in the room.");
                        Console.WriteLine("It's a small, intricately designed key. You decide to take it.");
                        break;

                    case "3":
                        PlayRiddleGame();
                        break;

                    case "4":
                        Console.WriteLine("You decide to quit. The game ends.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                if (playerHealth <= 0 || userChoice == "4")
                    break;
            }

            string playAgainChoice;
            do
            {
                playAgainChoice = GetUserInput("Do you want to play again? (Y/N): ");
            } while (playAgainChoice != "Y" && playAgainChoice != "N");

            playAgain = (playAgainChoice == "Y");
        }

        Console.WriteLine("Game over. You have been killed.");
    }
}
