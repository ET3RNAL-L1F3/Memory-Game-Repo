using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Variables to hold card information
    private string suit;
    private string rank;
    private bool faceUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCard (string m_suit, string m_rank)
    {
        // Assing suit and rank to the card
        suit = m_suit;
        rank = m_rank;

        // Get the card graphic to match the suit and rank
        string path = "Free_Playing_Cards/PlayingCards_" + rank + suit;
        GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>(path);

        // Add collision to check for mouse clicks
        gameObject.AddComponent<MeshCollider>();    
    }

    public bool isMatch(Card otherCard)
    {
        return (rank == otherCard.rank) && (suit == otherCard.suit);
    }

    public void Flip()
    {
        faceUp = !faceUp;
        transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!faceUp)
        {
            MemoryGame.Instance.SelectCard(this);
        }
    }
}
