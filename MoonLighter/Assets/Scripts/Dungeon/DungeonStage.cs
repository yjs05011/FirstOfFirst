using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStage : MonoBehaviour
{
    public List<DungeonBoard> mBoards = new List<DungeonBoard>();

    // Start is called before the first frame update
    void Start()
    {
        int directions = DungeonBoard.DIRECTION_TOP | DungeonBoard.DIRECTION_BOTTOM;

        List<DungeonBoard> boards = new List<DungeonBoard>();
        GetFilteredBoards(directions, ref boards);

        // ¹» »Ì¾Ò°Ô?
        for(int idx=0; idx<boards.Count; ++idx)
        {
            Debug.Log(boards[idx].gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GetFilteredBoards(int directions, ref List<DungeonBoard> output)
    {
        int count = mBoards.Count;
        for(int idx=0; idx<count; ++idx)
        {
            DungeonBoard board = mBoards[idx];

            if(board.IsMovableDirection(directions))
            {
                if(!output.Contains(board))
                {
                    output.Add(board);
                }
            }
        }
    }


}
