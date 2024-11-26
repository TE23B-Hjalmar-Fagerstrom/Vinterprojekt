int playerHealth = 100;
int playerArmor = 0;
int item = 0;
int escape = 0;
int monsterHealth = 1;

Console.WriteLine("Du är en äventyrare som har i uppgift att undersöka och töma en fängelsehåla(duengon) på skater och monster!");
Console.WriteLine("I det första rummet fins det en skat kista och en dör. vill du öppna kistan? Y/N");
string yesNo = Console.ReadLine().ToUpper();

while (yesNo != "Y" && yesNo != "N")
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

while (playerHealth > 0 && monsterHealth > 0 || escape == 0)
{
    fighet();
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

static void fighet()
{
    monster(1);
    Console.WriteLine("1.attack  2.försvara  3.föremål  4.fly");
    string player = Console.ReadLine();
    int playerMove = 0;
    bool success = false;
    while (success == false && playerMove > 4 && playerMove < 1)
    {
        success = int.TryParse(player, out playerMove);
        player = Console.ReadLine();
    }
    if (playerMove == 1)
    {
        Console.WriteLine("1. 10 dmg 85% success rate  2.");
    }

}

Console.ReadLine();