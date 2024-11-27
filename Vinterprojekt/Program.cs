int playerHealth = 100;
int playerArmor = 0;
int item = 0;
int chance = 100;
bool theEscape = false;
int monsterHealth = 1;

Console.WriteLine("Du är en äventyrare som har i uppgift att undersöka och töma en fängelsehåla(duengon) på skater och monster!");
Console.WriteLine("I det första rummet fins det en skat kista och en dör. vill du öppna kistan? Y/N");
string yesNo = Console.ReadLine().ToUpper();

while ((yesNo != "Y" || yesNo != "YES") && (yesNo != "N" || yesNo != "NO"))
{
    yesNo = Console.ReadLine().ToUpper();
}

if (yesNo == "Y")
{
    Console.WriteLine("Du öppna kistan och hittar några stål hanskar, dit för svar går up med 3.");
    Console.WriteLine("Du hittar även en hälsodryck, efter det går du igenom dörren");
    playerArmor += 3;
    item += 1;
}
else
{
    Console.WriteLine("Du går förbi kistan och går igenom dörren.");
}

Console.WriteLine("När du går igenom blir du bemöt av en");

while (playerHealth > 0 && monsterHealth > 0 || theEscape == false)
{

    monster(1);
    Console.WriteLine("1.attack  2.försvara  3.föremål  4.fly (40% chans)");
    string player = Console.ReadLine();
    int playerMove = 0;
    bool playerTurn = true;
    int playerchois = 100;
    bool success = false;
    while (playerTurn == true)
    {
        while (success == false && playerMove > 4 && playerMove < 1)
        {
            player = Console.ReadLine();
            success = int.TryParse(player, out playerMove);
        }

        if (playerMove == 1)
        {
            Console.WriteLine("1. 10 dmg 80% success  2. 20 dmg 50% success  3. 40 dmg 20% success  4. baka");
            while (true)
            {
                player = Console.ReadLine();
                success = int.TryParse(player, out playerchois); 
            }

            if (playerchois == 1)
            {
                chance = Random.Shared.Next(1, 11);
                Console.WriteLine("Du flyde");
                theEscape = true;
            }
            else
            {
                Console.WriteLine("Du flyde inte");
                playerTurn = false;
            }
        }

        if (playerMove == 2)
        {
            Console.WriteLine("2");
        }

        if (playerMove == 3)
        {
            Console.WriteLine("3");
        }

        if (playerMove == 4)
        {
            int escape = 0;
            yesNo = "";
            Console.WriteLine("är du säker? Y/N");
            while ((yesNo != "Y" || yesNo != "YES") && (yesNo != "N" || yesNo != "NO"))
            {
                yesNo = Console.ReadLine().ToUpper();
            }
            escape = Random.Shared.Next(1, 11);
            if (escape > 6)
            {
                Console.WriteLine("Du flyde");
                theEscape = true;
            }
            else
            {
                Console.WriteLine("Du flyde inte");
                playerTurn = false;
            }
        }

    }
}



static void monster(int level)
{
    int monsterHealth = 25 * level;
    int monsterAttack = 10 * level;
    int monsterDefens = 2 * level;
    int goldDrop = 5 * level;

    if (level == 1)
    {
        Console.WriteLine($"Slime ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine("(**)");
    }

    if (level == 2)
    {
        Console.WriteLine($"Greater Slime ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine("( *-* )");
    }
}







Console.ReadLine();