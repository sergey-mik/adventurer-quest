using System;
using System.Collections.Generic;
using System.Linq;

// Every class in the program is defined within the "Quest" namespace
// Classes within the same namespace refer to one another without a "using" statement
namespace Quest
{
    class Program
    {
        static void Main(string[] args)
        {

            bool repeatChallenges = true;
            while (repeatChallenges)
            {
                // Create a few challenges for our Adventurer's quest
                // The "Challenge" Constructor takes three arguments
                //   the text of the challenge
                //   a correct answer
                //   a number of awesome points to gain or lose depending on the success of the challenge
                Challenge twoPlusTwo = new Challenge("2 + 2?", 4, 10);
                Challenge theAnswer = new Challenge(
                    "What's the answer to life, the universe and everything?", 42, 25);
                Challenge whatSecond = new Challenge(
                    "What is the current second?", DateTime.Now.Second, 50);

                int randomNumber = new Random().Next() % 10;
                Challenge guessRandom = new Challenge("What number am I thinking of?", randomNumber, 25);

                Challenge favoriteBeatle = new Challenge(
                    @"Who's your favorite Beatle?
    1) John
    2) Paul
    3) George
    4) Ringo
",
                    4, 20
                );

                // Challenge 1: Guess the number
                Challenge guessTheNumber = new Challenge("I'm thinking of a number between 1-10. What is it?", 7, 4);

                // Challenge 2: Favorite color
                Challenge favoriteColor = new Challenge("What is my favorite color? (Enter the corresponding number: 1. Red, 2. Blue, 3. Green, 4. Yellow)", 2, 4);

                // Challenge 3: Find Longest Word
                Challenge findLongestWord = new Challenge("Find the longest word in a given string.", 3, 9);

                // Create a new Robe
                Robe myRobe = new Robe
                {
                    Colors = new List<string> { "Pink", "Yellow", "Red" },
                    Length = 55
                };

                // Create a new Hat
                Hat hat = new Hat();
                hat.ShininessLevel = 7;

                Prize prize = new Prize("You won a prize!");

                // "Awesomeness" is like our Adventurer's current "score"
                // A higher Awesomeness is better

                // Here we set some reasonable min and max values.
                //  If an Adventurer has an Awesomeness greater than the max, they are truly awesome
                //  If an Adventurer has an Awesomeness less than the min, they are terrible
                int minAwesomeness = 0;
                int maxAwesomeness = 100;

                // Make a new "Adventurer" object using the "Adventurer" class
                Console.WriteLine("Please enter your name:");
                string name = Console.ReadLine();

                Adventurer theAdventurer = new Adventurer(name, myRobe, hat);
                Console.WriteLine(theAdventurer.GetDescription());

                // A list of challenges for the Adventurer to complete
                // Note we can use the List class here because have the line "using System.Collections.Generic;" at the top of the file.
                List<Challenge> challenges = new List<Challenge>()
            {
                twoPlusTwo,
                theAnswer,
                whatSecond,
                guessRandom,
                favoriteBeatle,
                guessTheNumber,
                favoriteColor,
                findLongestWord
            };

                // Keep track of the number of successful challenges
                int successfulChallenges = 0;

                // Keep track of the adventurer's initial awesomeness for each quest
                int initialAwesomeness = theAdventurer.Awesomeness;

                // Randomly select 5 challenges without repeating
                Random random = new Random();
                List<Challenge> selectedChallenges = challenges.OrderBy(x => random.Next()).Take(5).ToList();

                // Loop through the selected challenges and subject the Adventurer to them
                foreach (Challenge challenge in selectedChallenges)
                {
                    bool success = challenge.RunChallenge(theAdventurer);
                    if (success)
                    {
                        successfulChallenges++;
                    }
                }

                // This code examines how Awesome the Adventurer is after completing the challenges
                // And praises or humiliates them accordingly
                if (theAdventurer.Awesomeness >= maxAwesomeness)
                {
                    Console.WriteLine("YOU DID IT! You are truly awesome!");
                }
                else if (theAdventurer.Awesomeness <= minAwesomeness)
                {
                    Console.WriteLine("Get out of my sight. Your lack of awesomeness offends me!");
                }
                else
                {
                    prize.ShowPrize(theAdventurer);
                    Console.WriteLine("I guess you did...ok? ...sorta. Still, you should get out of my sight.");
                }

                // Display the number of successful challenges
                Console.WriteLine($"You successfully completed {successfulChallenges} out of {selectedChallenges.Count} challenges.");

                // Ask the user if they want to repeat the challenges
                Console.WriteLine("Do you want to repeat the challenges? (Y/N)");
                string repeatInput = Console.ReadLine().ToUpper();
                if (repeatInput != "Y")
                {
                    repeatChallenges = false;
                }
                else
                {
                    // Update the adventurer's initial awesomeness for the next quest
                    initialAwesomeness += successfulChallenges * 10;
                    theAdventurer.Awesomeness = initialAwesomeness;
                }
            }
        }
    }
}