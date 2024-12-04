// allt som handlar om spelaren
int playerMove = 0;
int playerArmor = 0;
int playerChois = 100;
int playerHealth = 100;
string player = "";
bool playerTurn = true;

int item = 0;
int gold = 0;

bool theEscape = false;
int monsterHealth = 1;

Console.WriteLine("Du är en äventyrare som har i uppgift att undersöka och töma en fängelsehåla(duengon) på skater och monster!");
Console.WriteLine("I det första rummet fins det en skat kista och en dör. vill du öppna kistan? Y/N");
string yesNo = Console.ReadLine().ToUpper();

while (yesNo != "Y"  && yesNo != "N" )
{
    yesNo = Console.ReadLine().ToUpper();
}

if (yesNo == "Y")
{
    Console.WriteLine("Du öppna kistan och hittar några stål hanskar, dit för svar går up med 3.");
    Console.WriteLine("Du hittar även en hälsodryck, efter det går du igenom dörren");
    playerArmor += 3;
    item ++;
}
else
{
    Console.WriteLine("Du går förbi kistan och går igenom dörren."); 
}

Console.WriteLine("När du går igenom blir du bemöt av en");

while (playerHealth > 0 && monsterHealth > 0 && theEscape == false)
{

    monster(1);
    bool success = false;
    while (playerTurn == true)
    {
        Console.WriteLine($"ditt liv {playerHealth}, armor {playerArmor}");
        Console.WriteLine("1. attack  2. försvara  3. föremål  4. fly (40% chans)");
        while (success == false && playerMove > 4 || playerMove < 1)
        {
            player = Console.ReadLine();
            success = int.TryParse(player, out playerMove);
        }

        if (playerMove == 1) // om spelaren väljer attack
        {
            Console.WriteLine("1. 10 dmg 80% success  2. 20 dmg 50% success  3. 40 dmg 20% success  4. backa");
            while (playerChois > 4 || playerChois < 1)
            {
                player = Console.ReadLine();
                int.TryParse(player, out playerChois); 
            }
                int chance = Random.Shared.Next(1, 11);

            if (playerChois == 1 && chance > 2)
            {
                Console.WriteLine("Du träfade");
                monsterHealth -= 10;
                playerTurn = false;
            }
            else if (playerChois == 1 && chance < 2)
            {
                Console.WriteLine("Du missade");
                playerTurn = false;
            }
            if (playerChois == 2 && chance <= 5)
            {
                Console.WriteLine("Du träfade");
                monsterHealth -= 20;
                playerTurn = false;
            }
            else if (playerChois == 2 && chance > 5)
            {
                Console.WriteLine("Du missade");
                playerTurn = false;
            }
            if (playerChois == 3 && chance <= 2)
            {
                Console.WriteLine("Du träfade");
                monsterHealth -= 20;
                playerTurn = false;
            }
            else if (playerChois == 3 && chance > 2)
            {
                Console.WriteLine("Du missade");
                playerTurn = false;
            }
            if (playerChois == 4)
            {
                playerMove = 0;
                playerChois = 0;
            }
        }

        if (playerMove == 2) // om spelaren väljer försvar
        {
            
            
        }

        if (playerMove == 3) // om spelaren väljer föremål
        {
            Console.WriteLine($"1. hälsodryck({item})  2. backa");
            while (playerChois > 2 || playerChois < 1)
            {
                player = Console.ReadLine();
                int.TryParse(player, out playerChois); 
            }
            if (playerChois == 1)
            {
                playerHealth += 25;
                item --;
                playerTurn = false;
            }
            if (playerChois == 2)
            {
                playerMove = 0;
                playerChois = 0;
            }
        }

        if (playerMove == 4 && playerTurn == true) // om splaren väljer fly
        {
            yesNo = "";
            Console.WriteLine("är du säker? Y/N");
            while (yesNo != "Y"  && yesNo != "N" )
            {
                yesNo = Console.ReadLine().ToUpper();
            }
            int escape = Random.Shared.Next(1, 11);

            if (escape > 6)
            {
                Console.WriteLine("Du flyde");
                theEscape = true;
                playerTurn = false;
            }
            else
            {
                Console.WriteLine("Du flyde inte");
                playerTurn = false;
            }
        }

        while (playerTurn != true)
        {
            
        }
    }
}

if (monsterHealth <= 0 && theEscape == false)
{
    gold += 5;
}


static void monster(int level)
{
    int monsterHealth = 25 * level;
    int monsterAttack = 10 * level;
    int monsterDefens = 0 + level;
    int goldDrop = 5 * level;

    if (level == 1)
    {
        Console.WriteLine();
        Console.WriteLine($"Slime ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine("(**)");
        Console.WriteLine();
    }

    if (level == 2)
    {
        Console.WriteLine();
        Console.WriteLine($"Greater Slime ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine("( *-* )");
        Console.WriteLine();
    }
}





Console.ReadLine();