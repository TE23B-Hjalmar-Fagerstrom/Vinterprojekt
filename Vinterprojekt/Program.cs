// allt som handlar om spelaren
int playerMove = 0;
int playerArmor = 1;
int playerDefens = 0;
int playerAttack = 10;
List<int> playerDamage = [playerAttack, playerAttack + 10, playerAttack + 30];
List<int> damageChance = [2, 5, 8];
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
List<string> attcakHit = ["Slime spottar en svag syra på dig", "Greater Slime hoppar emot dig i en hög fart", "Slime kungen beordrar en grupp slime att svärma dig"];
List<string> attcakMiss = ["Slimet hoppar mot dig och du undvek precis i tid", "Greater Slime gör en stor syr boll och kastar den emot dig men du lyckades ta skydd i tid", "Slime kungen på börjar en magisk attack men du påminner den att den inte kan magi"];
int level = 1;

int chance = 0; // påverkar bådas chans att träffa

bool playAgain = true; // om spelaren vill spela igen

// gör det mer intreserand att läsa början igen plus ger bakgrund till hur du fick alla saker
List<string> story = ["Du är en äventyrare som har i uppgift att undersöka och töma en fängelsehåla(duengon) på skater och monster!", "Din far var en äventyrare som skulle undersöka en fängelsehåla men han återvände aldrig. En dag kom en annan äventyrare med alla din fars ägodelar och du bästemer dig för att avsluta det din far hade startat.", "Senda du var ung så hade du hör att din farfar hade ofrat sitt liv för din fars bästa och en dag begav sig din far att göra det samma och när du fick din fars ägodelar så viste du redan vad som behövde ske.",];
int storyProgres = 0;

while (playAgain == true)
{
    // gör så att spelaren inte startar död eller startar mot ett svårare monster än menat
    playerHealth = 100;
    level = 1;

    spel();
    if (playerHealth <= 0)
    {
        storyProgres++;

        Console.WriteLine("Du dog. Vill du försöka igen? Y/N");
        string yesNo = Console.ReadLine().ToUpper();

        while (yesNo != "Y" && yesNo != "N")
        {
            Console.WriteLine("skriv Y för ja eller N för nej");
            yesNo = Console.ReadLine().ToUpper();
        }

        if (yesNo == "Y")
        {
            playAgain = true;
            Console.WriteLine();
            Console.WriteLine($"Du har {gold} guld, {playerArmor} skydd(armor) och {item} hälsodrycker");
        }
        else
        {
            playAgain = false;
        }

        yesNo = "";
        Console.WriteLine();
    }

    if (playerHealth > 0)
    {
        // gör så att man startar om totalt från början
        playerArmor = 1;
        playerAttack = 10;
        item = 0;
        gold = 0;
        storyProgres = 0;

        Console.WriteLine("Du besegrade Slime kungen. Vill du slå han igen? Y/N");
        string yesNo = Console.ReadLine().ToUpper();

        while (yesNo != "Y" && yesNo != "N")
        {
            Console.WriteLine("skriv Y för ja eller N för nej");
            yesNo = Console.ReadLine().ToUpper();
        }

        if (yesNo == "Y")
        {
            playAgain = true;
        }
        else
        {
            playAgain = false;
        }
        yesNo = "";
        Console.WriteLine();
    }
}

void spel()
{
    Console.WriteLine(story[storyProgres]);
    Console.WriteLine("I det första rummet finns det en skat kista och en dör. vill du öppna kistan? Y/N");
    string yesNo = Console.ReadLine().ToUpper();

    while (yesNo != "Y" && yesNo != "N")
    {
        Console.WriteLine("skriv Y för ja eller N för nej");
        yesNo = Console.ReadLine().ToUpper();
    }
    Console.WriteLine();

    if (yesNo == "Y")
    {
        Console.WriteLine("Du öppna kistan och hittar några stål hanskar, ditt försvar går up med 3.");
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

    if (playerHealth > 0)
    {
        Console.WriteLine();
        Console.WriteLine("I det nya rummet så finns två dörrar. En av dörrarna är i metall och har ett rött x på sig.");
        Console.WriteLine("Den andra dörren är i trä och har en skylt med något som liknar ett stånd");
        Console.WriteLine("Vilken dörr går du igenom? (1 för metall 2 för trä)");

        while (playerChois != 1 && playerChois != 2)
        {
            player = Console.ReadLine();
            int.TryParse(player, out playerChois);
            Console.WriteLine();
        }

        if (playerChois == 1)
        {
            Console.WriteLine("Du går mott metal dörren och hör något bankande på dörren, när du öppna den så kom");
            Console.WriteLine("en stor Slime i hög far rakt emot dig och misade dig med ett hårstrå och framför dig stog en");
            fight();
            if (playerHealth > 0)
            {
                shop();
            }
        }
        else
        {
            shop();
        }

    }

    if (playerHealth > 0)
    {
        Console.WriteLine();
        Console.WriteLine("Efter du hndlade det du fan viktigt så fortaste du till nästa rum.");
        Console.WriteLine("När du kom in i rummet så märkte du att rummet var väldigt öppen nästan som ett tron rum för en kung");
        Console.WriteLine("det är då du la märke till att en stor skugga satt på en torn i längst back i rummet och du mötte ");

        fight();
    }

}


void fight()
{
    monster(level); // ger monstret hur mycket HP, DEF och ATK som det ska ha
    playerChois = 0; // gör så att tidigare Chois inte påverkar striden

    while (playerHealth > 0 && monsterHealth > 0 && theEscape == false)
    {
        bool success = false;
        while (playerTurn == true && playerHealth > 0 && monsterHealth > 0 && theEscape == false)
        {
            for (int i = 0; i < 3; i++) // ger ett mellan rum från texten innan striden startade och för texten som säger att du missa eller träffa
            {
                Console.WriteLine();
            }

            monsterAppearance(level); // visar monstrets nuvarande HP, DEF och ATK 
            Console.WriteLine($"Ditt liv {playerHealth}, armor {playerArmor}"); // visar spelarens nuvarande HP och Armor
            Console.WriteLine("1. attack  2. försvara  3. föremål  4. fly (40% chans)");

            while (success == false || playerMove > 4 || playerMove < 1)
            {
                player = Console.ReadLine();
                success = int.TryParse(player, out playerMove);
                Console.WriteLine();
            }

            if (playerMove == 1) // om spelaren väljer attack
            {
                Console.WriteLine("1. 10 dmg 80% success  2. 20 dmg 50% success  3. 40 dmg 20% success  4. backa");
                while (playerChois > 4 || playerChois < 1) // sålänge spelaren inte väljer något av ovan stående
                {
                    player = Console.ReadLine();
                    int.TryParse(player, out playerChois);
                    Console.WriteLine();
                }
                chance = Random.Shared.Next(1, 11);

                if (playerChois < 4)
                {
                    if (chance > damageChance[playerChois - 1])
                    {
                        Console.WriteLine("Du träfade");
                        Console.WriteLine();
                        monsterHealth -= playerDamage[playerChois - 1] - monsterDefens;
                        playerTurn = false;
                    }
                    else
                    {
                        Console.WriteLine("Du missade");
                        Console.WriteLine();
                        playerTurn = false;
                    }
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
                Console.WriteLine($"1. skyda dig (tar {playerDefens + playerArmor} minder dmg)  2. backa");
                while (playerChois > 2 || playerChois < 1)
                {
                    player = Console.ReadLine();
                    int.TryParse(player, out playerChois);
                    Console.WriteLine();
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
                    Console.WriteLine();
                }
                if (playerChois == 1 && item > 0)
                {
                    playerHealth += 25;
                    item--;
                    playerMove = 0;
                    playerChois = 0;
                }
                if (playerChois == 1 && item == 0)
                {
                    Console.WriteLine("Du har inga flera hälsodrycker");
                    playerMove = 0;
                    playerChois = 0;
                }
                else
                {
                    playerMove = 0;
                    playerChois = 0;
                }
            }

            if (playerMove == 4 && playerTurn == true) // om splaren väljer fly
            {
                string yesNo = "";
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
                chance = Random.Shared.Next(1, 11);

                if (chance > 3 && monsterHealth > 0)
                {
                    Console.Write(attcakHit[level - 1]);
                    if (monsterAttack - (playerArmor + playerDefens) < 0) // gör så att monstret inte kan heala spelaren 
                    {
                        monsterAttack = playerArmor + playerDefens;
                        Console.WriteLine($" och gör {monsterAttack - (playerArmor + playerDefens)} skada");
                        playerHealth -= monsterAttack - (playerArmor + playerDefens);
                        monsterAttack = 15 * level;
                    }
                    else
                    {
                        Console.WriteLine($" och gör {monsterAttack - (playerArmor + playerDefens)} skada");
                        playerHealth -= monsterAttack - (playerArmor + playerDefens);
                    }
                }
                else
                {
                    Console.WriteLine(attcakMiss[level - 1]);
                }
                playerTurn = true;
            }
        }
    }

    if (monsterHealth < 0 && theEscape == false)
    {
        gold = 5 * level;
        Console.WriteLine($"Du bäsegrade slimet och fick {gold} guld och fortsätt till nästa rum");
    }

    if (theEscape == true)
    {
        Console.WriteLine("Du flydde in till nästa rum");
        theEscape = false;
    }
    level++;
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
        Console.WriteLine("Slime (**)");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }

    if (level == 2)
    {
        Console.WriteLine();
        Console.WriteLine("Greater Slime ( *-* )");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }

    if (level == 3)
    {
        Console.WriteLine();
        Console.WriteLine("           ^^^^^^^   ");
        Console.WriteLine("Slime KING( *___* )");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }
}

void shop()
{
    Console.WriteLine();
    Console.WriteLine("Du rör dig mott trä dörren och hör en röst mumlande på andra sidan.");
    Console.WriteLine("När du öppnar dörren blir du bemöt av en eldre man som verkar driva en affär här och säljer.");
    List<string> shopItems = ["1. hälsodrycker", "2. skydd (+3 armor)", "3. slipsten (+10 dmg)"];
    List<int> itemPrices = [3, 7, 10];

    while (playerChois != 4)
    {
        Console.WriteLine();
        Console.WriteLine($"du har {gold} guld");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(shopItems[i]);
            Console.Write(" för ");
            Console.Write(itemPrices[i]);
            Console.WriteLine(" guld");
        }
            Console.WriteLine("4. lämna affär");

        bool success = false;
        while (success == false || playerChois > 4 || playerChois < 0)
        {
            player = Console.ReadLine();
            success = int.TryParse(player, out playerChois);
        }

        if (playerChois == 1 && gold > 2)
        {
            Console.WriteLine("Du köpte en hälsodryk för 3 guld.");
            item ++;
            gold -= 3;
        }
        else if (playerChois == 1 && gold < 2)
        {
            Console.WriteLine("Du har inte råd");
        }
        
        if (playerChois == 2 && gold > 5)
        {
            Console.WriteLine("Du köpte skydd för 7 guld.");
            playerArmor += 3;
            gold -= 7;
        }
        else if (playerChois == 2 && gold < 5)
        {
            Console.WriteLine("Du har inte råd");
        }

        if (playerChois == 3 && gold > 10)
        {
            Console.WriteLine("Du köpte en slipsten för 10 guld.");
            playerAttack += 10;
            gold -= 10;
        }
        else if (playerChois == 3 && gold < 10)
        {
            Console.WriteLine("Du har inte råd");
        }
    }

}