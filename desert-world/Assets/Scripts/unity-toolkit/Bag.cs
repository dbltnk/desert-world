using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour {

    public bool IsTestActive;

    private void Start () {
        if (IsTestActive) {
            Setup(5, 5);
            print("--------- Bag Test ---------");
            for (int i = 0; i <= 10; i++) Draw();
            print("----------------------------");
        }
    } 

    // This bag returns a set of white or black pieces and shuffles itself when all pieces have been returned.

    List<string> bag = new List<string>();
    int amount1;
    int amount2;
    bool setup = false;

    public void Setup (int amount1, int amount2) {
        if (setup == true) Debug.LogError("This bag has already been set up.");
        this.amount1 = amount1;
        this.amount2 = amount2;
        setup = true;
    }

    void Fill () {

        int piecesToAdd1 = amount1;
        int piecesToAdd2 = amount2;

        while (piecesToAdd1 > 0 || piecesToAdd2 > 0) {
            string p = null;
            if (Random.Range(0f, 1f) <= 0.5f) {
                if (piecesToAdd1 <= 0) continue;
                p = "black";
                piecesToAdd1--;
            } else {
                if (piecesToAdd2 <= 0) continue;
                p = "white";
                piecesToAdd2--;
            } 

            bool addToBeginning = (Random.Range(0f, 1f) <= 0.5f) ? true : false;

            if (addToBeginning) {
                bag.Insert(0, p);
            }
            else {
                bag.Insert(bag.Count, p);
            }
        }
    }

    string DrawWithoutShuffle () {
        string p;
        p = bag[0];
        bag.RemoveAt(0);
        if (IsTestActive) print(p);
        return p;
    }

    public string Draw () {
        if (setup == false) Debug.LogError("Please call setup first.");
        if (bag.Count == 0) {
            Fill();
        }
        return DrawWithoutShuffle();
    }

}
