using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    //Suit and rank names to match Free_Playing_cards naming convention
    string[] cardSuits = new string[] { "Club", "Spades", "Diamond", "Heart" };
    string[] cardRanks = new string[] {"2", "3", "4", "5","6","7","8","9","10",
                                        "J", "Q", "K", "A"};

    // Make sure there is only one instance of the game running at once.
    static public MemoryGame Instance;

    //Variables to hold all cards and cards that are selected by the player
    private Card[] cards;
    private Card firstCard;
    private Card secondCard;

    // A variable to hold time, allowing the player to see the cards before they are checked for match
    private float selectTime;

    // Start is called before the first frame update
    void Start()
    {
        // Set instance to this gameobject to prevent more than one from being created
        if (Instance == null)
        {
            Instance = this;
        }

        // Reference for all cards on the GameBoard
        cards = transform.GetComponentsInChildren<Card>();

        //Generate random cards in pairs
        int t = 0;
        Shuffle(cards);

        for (int i = 0; i < cards.Length / 2; i++)
        {
            //get random suit and rank
            string suit = GetRandomFromArray(cardSuits);
            string rank = GetRandomFromArray(cardRanks);

            // assign rank and suit to 2 cards
            cards[t++].GenerateCard(suit, rank);
            cards[t++].GenerateCard(suit, rank);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Check for 2 cards flipped
        if (secondCard != null)
        {
            //Wait a second to allow player to see the card
            if (Time.time > selectTime + 1.0)
            {
                CheckMatch();
            }
        }
    }

    private void CheckMatch()
    {
        if (firstCard.isMatch(secondCard))
        {
            //Remove Cards from the board
            firstCard.Hide();
            secondCard.Hide();
        }
        else
        {
            //Flip the cards back over
            firstCard.Flip();
            secondCard.Flip();
        }

        // Clear selected cards
        firstCard = secondCard = null;
    }

    private void Shuffle<T> (T[] array)
    {
        // Store array length
        int n = array.Length;

        // loop through the array
        while (n > 1)
        {
            // Create random index for shuffling
            int i = (int)Mathf.Floor(Random.value * (n--));

            // keep track of current card in loop
            T temp = array[n];

            //Swap current card with random indexed card
            array[n] = array[i];
            array[i] = temp;   
        }
    }

    private T GetRandomFromArray<T> (T[] array)
    {
        return array[ (int)Mathf.Floor(Random.value * array.Length) ];
    }

    public void SelectCard(Card card)
    {
        // Check if 2 cards have been selected
        if (secondCard == null)
        {
            //Flip Card
            card.Flip();

            // Save the card as first or second card flipped
            if (firstCard == null)
            {
                Debug.Log("First card flipped");
                firstCard = card;
            }
            else
            {
                Debug.Log("Second card flipped");
                secondCard = card;
                selectTime = Time.time;
            }
        }
    }
}
