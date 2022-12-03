/*
 * --- Day 21: Dirac Dice ---
 * Part 1: Solved
 * Part 2: Not yet solved
 */


PartOne();
PartTwo();

void PartOne()
{
    int dDicePos = 1;
    int rolls = 0;

    int p1;
    int p1pos = 10;
    long p1score = 0;

    int p2;
    int p2pos = 2;
    long p2score = 0;

    while (p1score < 1000 && p2score < 1000)
    {
        p1 = RollNextDice(ref dDicePos, ref rolls);
        p1pos = MoveNextPos(p1, p1pos);
        p1score += p1pos;

        if (p1score >= 1000) { break; }

        p2 = RollNextDice(ref dDicePos, ref rolls);
        p2pos = MoveNextPos(p2, p2pos);
        p2score += p2pos;

        if (p2score >= 1000) { break; }
    }

    long score = rolls * ((p1score < p2score) ? p1score : p2score);
    Console.WriteLine($"Part One. Final Score: {score}");
}

void PartTwo()
{

    List<(int r1, int r2, int r3)> possibleRolls = new();
    for (int i = 1; i < 4; i++)
    {
        for (int j = 1; j < 4; j++)
        {
            for (int k = 1; k < 4; k++)
            {
                possibleRolls.Add((i, j, k));
            }
        }
    }

}


int MoveNextPos(int p, int ppos)
{
    int moves = p % 10;
    for (int i = 0; i < moves; i++)
    {
        if(ppos == 10)
        {
            ppos = 0;
        }
        ppos++;
    }
    return ppos;
}

// simulate dice rolls
int RollNextDice(ref int ddice, ref int rolls)
{
    int rollValue = 0;

    for (int i = 0; i < 3; i++)
    {
        rollValue += ddice;
        if (ddice == 100)
        {
            ddice = 0;
        }
        ddice++;
        rolls++;
    }
    return rollValue;
}