//assignment module 6

//Metoderna MainMenu(), Login() och OptionMenu()
//hade kanske kunnat göras snyggare som while-loops, men tycker de fungerar bra ändå

//Hårdkodade värden för en användare i en fiktiv databas
string username = "petter"; //hårdkodat värde för användare
string password = "abc123!"; //hårdkodat värde för lösenord till ovanstående användare
float saldo = 10000F; //mängden pengar som Petter börjar med på saldot

void MainMenu()
{
    Console.WriteLine("1. Logga in \n" +
                      "2. Avsluta");

    char choice = ChoseOption();
    if (choice == '1')
    {
        Login();
    }
    else if (choice == '2')
    {
        LogOff();
    }
    else
        Console.WriteLine("Ogiltigt val, var vänlig försök igen");
        MainMenu();
}

void Login()
{
    Console.WriteLine("Ange ditt användarnamn");
    String loginUsername = Console.ReadLine();
    Console.WriteLine("Ange ditt lösenord");
    String loginPassword = Console.ReadLine();

    //kontroll av att inskrivet användarnamn och lösenord stämmer överens med de hårdkodade värdena i filen
    if(loginUsername == username && loginPassword == password)
    {
        OptionMenu(loginUsername, loginPassword);
    }
    else
    {
        Console.WriteLine("Felaktiga uppgifter. Var vänlig försök igen\n");
        MainMenu();
    }
}

void LogOff(String? username = null) //optional strängargument så att metoden kan kallas på både från inloggat och utloggat läge
{
    Console.WriteLine($"Välkommen åter {username}\n");
    MainMenu();
}

void OptionMenu(String username, String password) //tar emot de inskrivna värdena från användaren från Login()-menyn
{
    
    Console.WriteLine($"\nVälkommen, {username}! \nDitt saldo: {saldo} kr");
    Console.WriteLine("1. Ta ut pengar\n" +
                      "2. Sätta in pengar\n" +
                      "3. Logga ut\n");

    char choice = ChoseOption();
    switch (choice)
    {
        case '1':
            {
                saldo -= WithdrawMoney();
                break;
            }
        case '2':
            {
                saldo += DepositMoney();
                break;
            }
        case '3':
            {
                LogOff(username);
                break;
            }
        default: //vid fel så hoppar programmet tillbaka till början av den här metoden med samma användarnamn och lösenord
            {
                Console.WriteLine("Ogiltigt val, var vänlig försök igen\n");
                OptionMenu(username, password);
                break;
            }
    }
}

char ChoseOption() //metod för att kontrollera att det användaren har skrivit in faktiskt är en character
{
    while (true)
    {
        char chosenOption;
        if (!char.TryParse(Console.ReadLine(), out chosenOption))
        {
            Console.WriteLine("Ogiltigt val, försök igen.\n");
        }
        else
        {
            return chosenOption;
        }
    } 
}

float DepositMoney()
{
    while (true) //while loop som pågår tills användaren har skrivit in ett giltigt värde för insättning
    {
        float amountToDeposit = 0F;
        Console.WriteLine("Hur mycket pengar vill du sätta in?");
        if (float.TryParse(Console.ReadLine(), out amountToDeposit)) //kontrollerar att inskrivet värde är en float
        {
            if(amountToDeposit > 0) //kontrollerar att det inskrivna värdet är positivt för att undvika "uttag" vid insättning
            {
                return amountToDeposit;
            }
            else
            {
                Console.WriteLine("Du kan bara sätta in en positiv summa.");
            }
        }
        else
        {
            Console.WriteLine("Error, du har skrivit in något annat än en positiv summa pengar.");
        }
    }
}

float WithdrawMoney()
{
    while (true) //while loop som pågår tills användaren har skrivit in ett giltigt värde för uttag
    {
        float amountToWithdraw = 0F;
        Console.WriteLine("Hur mycket pengar vill du ta ut?");
        if (float.TryParse(Console.ReadLine(), out amountToWithdraw))
        {
            //kontrollerar att det inskrivna värdet är positivt för att undvika "insättning" vid uttag.
            //Och kontroll för maxuttag
            if (amountToWithdraw > 0 && amountToWithdraw <= 50000F) 
            {
                return amountToWithdraw;
            }
            else
            {
                Console.WriteLine("Giltigt uttag är mellan 1 och 50k kronor.");
            }
        }
        else
        {
            Console.WriteLine("Error, du har skrivit in något annat än en positiv summa pengar.");
        }
    }
}

MainMenu();

