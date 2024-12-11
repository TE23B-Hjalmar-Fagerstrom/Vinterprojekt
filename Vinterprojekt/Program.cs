// allt som handlar om spelaren
int playerMove = 0;
int playerArmor = 1;
int playerDefens = 0;
int playerChois = 100;
int playerHealth = 100;
string player = "";
bool playerTurn = true;
bool theEscape = false;

// saker som spelaren kommer att kunna använda
int item = 0;
int gold = 0;

// allt som handlar om monster
int monsterHealth = 0;
int monsterAttack = 0;
int monsterDefens = 0;
int level = 1;

int chance = 0;

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
    item++;
}
else
{
    Console.WriteLine("Du går förbi kistan och går igenom dörren.");
}

Console.WriteLine("När du går igenom blir du bemöt av en");

fight();  // allt som har med striden att göra 

if (monsterHealth < 0 && theEscape == false)
{
    gold += 5 * level;
    Console.WriteLine($"Du bäsegrade slimet och fick {gold} guld och fortsätt till nästa rum");
}

if (theEscape == true)
{
    Console.WriteLine("Du flydde in till nästa rum");
    theEscape = false;
}

level ++;
Console.WriteLine("");




void fight()
{
    monster(level); // ger monstret hur mycket HP, DEF och ATK som det ska ha
    while (playerHealth > 0 && monsterHealth > 0 && theEscape == false)
    {
        bool success = false;
        while (playerTurn == true && playerHealth > 0 && monsterHealth > 0 && theEscape == false)
        {
            monsterAppearance(level); // visar monstrets nuvarande HP, DEF och ATK 
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
                while (playerChois > 4 || playerChois < 1) // sålänge spelaren inte väljer något ovan stående
                {
                    player = Console.ReadLine();
                    int.TryParse(player, out playerChois);
                }
                chance = Random.Shared.Next(1, 11);

                if (playerChois == 1 && chance > 2)
                {
                    Console.WriteLine("Du träfade");
                    monsterHealth -= 10 - monsterDefens;
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
                    monsterHealth -= 20 - monsterDefens;
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
                    monsterHealth -= 20 - monsterDefens;
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
                playerDefens = 2 * playerArmor;
                Console.WriteLine($"1. skyda dig ({playerDefens} dmg)  2. backa");
                while (playerChois > 2 || playerChois < 1)
                {
                    player = Console.ReadLine();
                    int.TryParse(player, out playerChois);
                }

                if (playerChois == 1)
                {
                    playerTurn = false;
                }
                else
                {
                    playerMove = 0;
                    playerChois = 0;
                }

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
                    item--;
                    playerTurn = false;
                }
                else
                {
                    playerMove = 0;
                    playerChois = 0;
                }
            }

            if (playerMove == 4 && playerTurn == true) // om splaren väljer fly
            {
                yesNo = "";
                Console.WriteLine("är du säker? Y/N");
                while (yesNo != "Y" && yesNo != "N")
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

            while (playerTurn != true && theEscape == false)
            {
                playerMove = 0;
                playerChois = 0;
                chance = Random.Shared.Next(1,11);
                if (chance > 3)
                {
                    Console.WriteLine($"slime spottar en svag syra på dig och gör {monsterAttack - (playerArmor + playerDefens)} skada");
                    playerHealth -= monsterAttack - (playerArmor + playerDefens);
                }
                else
                {
                    Console.WriteLine("slimet hoppar mot dig och du undvek precis i tid");
                }
                playerTurn = true;
            }
        }
    }
}

void monster(int level)
{
    monsterHealth = 25 * level;
    monsterAttack = 15 * level;
    monsterDefens = 0 + level;
}

void monsterAppearance(int level)
{
    if (level == 1)
    {
        Console.WriteLine();
        Console.WriteLine($"Slime (**)");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }

    if (level == 2)
    {
        Console.WriteLine();
        Console.WriteLine($"Greater Slime ( *-* )");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }
}




Console.ReadLine();