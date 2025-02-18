// allt som handlar om spelaren
int playerMove = 100;
int playerArmor = 1;
int playerDefens = 0;
int playerAttack = 10;
List<int> playerDamage = [0, 10, 30];
List<int> damageChance = [2, 5, 8];
int playerChois = 100;
int playerHealth;
string player;
string yesNo;
bool playerTurn = true;
bool theEscape = false;

// saker som spelaren kommer att kunna använda
int item = 0;
int gold = 0;

// allt som handlar om monster (ändra styrka i void monster)
int monsterHealth = 0;
int monsterAttack = 0;
int monsterDefens = 0;
List<string> attcakHit = ["Slime spottar en svag syra på dig", "Greater Slime hoppar emot dig i en hög fart", "Slime kungen beordrar en grupp slime att svärma dig"];
List<string> attcakMiss = ["Slimet hoppar mot dig och du undvek precis i tid", "Greater Slime gör en stor syr boll och kastar den emot dig men du lyckades ta skydd i tid", "Slime kungen på börjar en magisk attack men du påminner den att den inte kan magi"];
int level = 1;

// värden och text som har med shop att göra
List<string> shopItems = ["hälsodryck", "skydd (+3 armor)", "slipsten (+10 dmg)"];
List<int> itemPrices = [2, 5, 10];

int chance; // påverkar bådas chans att träffa

bool playAgain = true; // om spelaren vill spela igen

// gör det mer intreserand att läsa början igen plus ger bakgrund till hur du fick alla saker
List<string> story = ["Du är en äventyrare som har i uppgift att undersöka och töma en fängelsehåla(duengon) på skater och monster!", 
    "Din far var en äventyrare som skulle undersöka en fängelsehåla men han återvände aldrig. En dag kom en annan äventyrare med alla din fars ägodelar och du bästemer dig för att avsluta det din far hade startat.", 
    "Senda du var ung så hade du hör att din farfar hade ofrat sitt liv för din fars bästa och en dag begav sig din far att göra det samma och när du fick din fars ägodelar så viste du redan vad som behövde ske.", 
    "Du vet vad som måste göras."
    ];
int storyProgres = 0;

while (playAgain == true)
{
    // gör så att spelaren inte startar död eller startar mot ett svårare monster än menat
    playerHealth = 100;
    level = 1;

    Game();
    if (playerHealth <= 0)
    {
        if (storyProgres < 3) // gör så att spelet inte kraschar om man dör väldigt mycket
        {
            storyProgres++;
        }

        Console.WriteLine("Du dog. Vill du försöka igen? Y/N");
        YesOrNo();

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
        Console.WriteLine();
    }

    if (playerHealth > 0)
    {
        // gör så att man startar om totalt från början om man van
        playerArmor = 1;
        playerAttack = 10;
        item = 0;
        gold = 0;
        storyProgres = 0;

        Console.WriteLine("Du besegrade Slime kungen. Vill du spela igen? Y/N");
        YesOrNo();

        if (yesNo == "Y")
        {
            playAgain = true;
        }
        else
        {
            playAgain = false;
        }
        Console.WriteLine();
    }
}

void Game()
{
    Console.WriteLine(story[storyProgres]);
    Console.WriteLine("");
    Console.WriteLine("I det första rummet finns det en skat kista och en dörr. vill du öppna kistan? skriv Y eller N ");
    YesOrNo();

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

    Fight();  // allt som har med striden att göra 

    if (playerHealth > 0) // om spelaren lever
    {
        Console.WriteLine();
        Console.WriteLine("I det nya rummet så finns två dörrar. En av dörrarna är i metall och har ett rött x på sig.");
        Console.WriteLine("Den andra dörren är i trä och har en skylt med något som liknar en butik");
        Console.WriteLine("Vilken dörr går du igenom? (skriv 1 för metall och 2 för trä)");

        TryParseChoic(2, 1);

        if (playerChois == 1)
        {
            Console.WriteLine("Du går mott metal dörren och hör något bankande på dörren, när du öppna den så kom");
            Console.WriteLine("en stor Slime i hög far rakt emot dig och misade dig med ett hårstrå och framför dig stog en");
            Fight();
            if (playerHealth > 0) // om spelaren lever
            {
                Shop();
            }
        }
        else
        {
            Shop();
            level++;
        }
    }

    if (playerHealth > 0) // om spelaren lever
    {
        Console.WriteLine();
        Console.WriteLine("Efter du handlade det du fan viktigt så fortaste du till nästa rum.");
        Console.WriteLine("När du kom in i rummet så märkte du att rummet var väldigt öppen nästan som ett tron rum för en kung");
        Console.WriteLine("det är då du la märke till att en stor skugga satt på en torn i längst back i rummet och du mötte ");

        Fight();
    }
}

void TryParseChoic(int maxNumber, int minNumber)
{
    while (playerChois > maxNumber || playerChois < minNumber) // så länge spelaren inte väljer något givet tal
    {
        player = Console.ReadLine();
        int.TryParse(player, out playerChois);
        Console.WriteLine();
        if (playerChois > maxNumber || playerChois < minNumber)
        {
            Console.WriteLine($"skriv ett tall från {minNumber} och {maxNumber}");
        }
    }
}

void Fight()
{
    Monster(level); // ger monstret hur mycket HP, DEF och ATK som det ska ha
    playerChois = 0; // gör så att tidigare Chois inte påverkar striden

    // så länge spelaren och monstret lever eller spelaren inte har flyt
    while (playerHealth > 0 && monsterHealth > 0 && theEscape == false)
    {
        bool success = false;
        while (playerTurn == true && playerHealth > 0 && monsterHealth > 0 && theEscape == false)
        {
            // ger ett mellan rum från texten innan striden startade och för texten som säger att du missa eller träffa
            for (int i = 0; i < 3; i++) 
            {
                Console.WriteLine();
            }

            MonsterAppearance(level); // visar monstrets nuvarande HP, DEF och ATK 
            Console.WriteLine($"Ditt liv {playerHealth}, armor {playerArmor}"); // visar spelarens nuvarande HP och Armor
            Console.WriteLine("1. attack  2. försvara  3. föremål  4. fly (30% chans)");
            if (playerMove == 100) // visas första gången spelaren går in i strid
            {
                // förklarar i mer ditalj vad det är man ska göra
                Console.WriteLine("");
                Console.WriteLine("skriv numret till vänster av handlingen du vill göra");
            }

            while (success == false || playerMove > 4 || playerMove < 1)
            {
                player = Console.ReadLine();
                success = int.TryParse(player, out playerMove);
                Console.WriteLine();
                if (playerMove > 4 || playerMove < 1)
                {
                    Console.WriteLine("skriv ett tall från 1 och 4");
                }
            }

            if (playerMove == 1) // om spelaren väljer attack
            {
                Console.WriteLine($"1. {playerAttack} dmg 80% success  2. {playerAttack + playerDamage[1]} dmg 50% success  3. {playerAttack + playerDamage[2]} dmg 20% success  4. backa");
                TryParseChoic(4, 1);

                chance = Random.Shared.Next(1, 11);

                if (playerChois < 4)
                {
                    if (chance > damageChance[playerChois - 1])
                    {
                        Console.WriteLine("Du träfade");
                        Console.WriteLine();
                        monsterHealth -= playerAttack + playerDamage[playerChois - 1] - monsterDefens;
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
                TryParseChoic(2, 1);

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

                TryParseChoic(2, 1);

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
                Console.WriteLine("är du säker? Y/N");
                YesOrNo();

                int escape = Random.Shared.Next(1, 11);

                if (escape > 7)
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
                        monsterAttack = 15 * level; // setter det tillbaka till orginel värdet 
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
        gold += 5 * level;
        Console.WriteLine($"Du bäsegrade slimet och fick {gold} guld och fortsätt till nästa rum");
    }

    if (theEscape == true)
    {
        Console.WriteLine("Du flydde in till nästa rum");
        theEscape = false;
    }
    level++;
}

void Monster(int level) // säger hur "starkt" ett monster kommer vara beroende på level
{
    monsterHealth = 25 * level;
    monsterAttack = 15 * level;
    monsterDefens = level * level;
}

void MonsterAppearance(int level) // skriver ut vilket monster det är och alla monstrets variabler
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
        Console.WriteLine("            ^^^^^^^   ");
        Console.WriteLine("Slime KING ( *___* )");
        Console.WriteLine($"ATK {monsterAttack} HP {monsterHealth} DEF {monsterDefens}");
        Console.WriteLine();
    }
}


void Shop()
{
    Console.WriteLine();
    Console.WriteLine("Du rör dig mott trä dörren och hör en röst mumlande på andra sidan.");
    Console.WriteLine("När du öppnar dörren blir du bemöt av en eldre man som verkar driva en affär här och säljer.");

    while (playerChois != 4) // fortsätter tills spelaren lämnar 
    {
        playerChois = 100;
        Console.WriteLine();
        for (int i = 0; i < 3; i++) // skriver ut alla val spelaren kan välja mellan samt vad det kostar
        {
            Console.WriteLine($"{i + 1}. {shopItems[i]} för {itemPrices[i]} guld"); // skriver upp vad spelaren kan köpa och vad det kostar 
        }
        Console.WriteLine("4. lämna affär");
        Console.WriteLine($"du har {gold} guld");

        TryParseChoic(4, 1);

        ItemChois(4, 1);
    }
}

// en metod för att kolla vilket föremål spelaren valde och vad det kostar
// samt kollar om spelaren har råd 
void ItemChois(int maxNumber, int minNumber) 
{
    if (playerChois != maxNumber && playerChois > minNumber)
    {
        if (gold >= itemPrices[playerChois - 1]) // kollar om spelaren har råd med det dem valde
        {
            Console.WriteLine($"Du köpte en {shopItems[playerChois - 1]} för {itemPrices[playerChois - 1]} guld.");
            if (playerChois == 1)
            {
                item++;
            }
            else if (playerChois == 2)
            {
                playerArmor += 3;
            }
            else
            {
                playerAttack += 10;
            }
            gold -= itemPrices[playerChois - 1];
        }
        else
        {
            Console.WriteLine("Du har inte råd");
        }
    }
}

void YesOrNo() // en metod för när spelaren ska göra ett val mellan ja och nej
{
    yesNo = "";
    while (yesNo != "Y" && yesNo != "N") // så länge spelaren inte skriver Y eller N
    {
        yesNo = Console.ReadLine().ToUpper();
        if (yesNo != "Y" && yesNo != "N")
        {
            Console.WriteLine("skrive Y för ja och N för nej"); // säger till spelaren vad de behöver göra igen
        }
    }
}