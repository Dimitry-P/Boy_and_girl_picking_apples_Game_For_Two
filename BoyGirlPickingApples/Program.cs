using System;
using SFML.Learning;
using SFML.Window;
using SFML.Graphics;

class Program : Game
{
    static string backGroundTexture = LoadTexture("bg.jpg");
    static string playerOneTexture = LoadTexture("boy.png");
    static string playerTwoTexture = LoadTexture("girl.png");
    static string appleTexture = LoadTexture("apple.png");
    static int playerOneDirection = 1;
    static int playerTwoDirection = 1;
    static float playerOneX = 0;
    static float playerOneY = 200;
    static int playerOneSpeed = 100;
    static int playerTwoSpeed = 100;
    static float playerTwoX = 700;
    static float playerTwoY = 200;
    static float appleX;
    static float appleY;
    static float appleX2;
    static float appleY2;
    static int appleSizeX = 51;
    static int appleSizeY = 62;
    static int playerOneSizeX = 51;
    static int playerOneSizeY = 119;
    static int playerTwoSizeX = 83;
    static int playerTwoSizeY = 102;
    static int scoreOne = 0;
    static int scoreTwo = 0;
    static int bestScore = 0;
    static string numberPLayer;
    static string soundOne = LoadSound("1sound.wav");
    static string soundTwo = LoadSound("2sound.wav");
    
    static void DrawplayerOne()
    {
        if(playerOneDirection == 1 || playerOneDirection == 3 ) DrawSprite(playerOneTexture, playerOneX, playerOneY, 5, 4, playerOneSizeX, playerOneSizeY);
        if(playerOneDirection == 2 || playerOneDirection == 4) DrawSprite(playerOneTexture, playerOneX, playerOneY, 70, 4, playerOneSizeX, playerOneSizeY);
    }
    static void DrawplayerTwo()
    {
        if(playerTwoDirection == 1 || playerTwoDirection == 3) DrawSprite(playerTwoTexture, playerTwoX, playerTwoY, 86, 0, playerTwoSizeX, playerTwoSizeY);
        if (playerTwoDirection == 2 || playerTwoDirection == 4) DrawSprite(playerTwoTexture, playerTwoX, playerTwoY, 0, 0, playerTwoSizeX, playerTwoSizeY);
    }
    static void Drawapple()
    {
        DrawSprite(appleTexture, appleX, appleY);
    }
    static void Drawapple2()
    {
        DrawSprite(appleTexture, appleX2, appleY2);
    }

    static void PlayerOneMove()
    {
        if (GetKey(Keyboard.Key.D)) playerOneDirection = 1;
        if (GetKey(Keyboard.Key.A)) playerOneDirection = 2;
        if (GetKey(Keyboard.Key.W)) playerOneDirection = 3;
        if (GetKey(Keyboard.Key.S)) playerOneDirection = 4;

        if (playerOneDirection == 1) playerOneX += playerOneSpeed * DeltaTime;
        if (playerOneDirection == 2) playerOneX -= playerOneSpeed * DeltaTime;
        if (playerOneDirection == 3) playerOneY -= playerOneSpeed * DeltaTime;
        if (playerOneDirection == 4) playerOneY += playerOneSpeed * DeltaTime;
    }

    static void PlayerTwoMove()
    {
        if (GetKey(Keyboard.Key.L)) playerTwoDirection = 1;
        if (GetKey(Keyboard.Key.J)) playerTwoDirection = 2;
        if (GetKey(Keyboard.Key.I)) playerTwoDirection = 3;
        if (GetKey(Keyboard.Key.K)) playerTwoDirection = 4;

        if (playerTwoDirection == 1) playerTwoX += playerTwoSpeed * DeltaTime;
        if (playerTwoDirection == 2) playerTwoX -= playerTwoSpeed * DeltaTime;
        if (playerTwoDirection == 3) playerTwoY -= playerTwoSpeed * DeltaTime;
        if (playerTwoDirection == 4) playerTwoY += playerTwoSpeed * DeltaTime;
    }

    static void Main(string[] args)
    {
        InitWindow(1400, 900, "BoyAndGirl");

        SetFont("comic.ttf");
        Random rnd = new Random();
        appleX = rnd.Next(0, 700 - appleSizeX);
        appleY = rnd.Next(200, 900 - appleSizeY);
        appleX2 = rnd.Next(700, 1400 - appleSizeX);
        appleY2 = rnd.Next(200, 900 - appleSizeY);
        bool isLose = false;
        bool isLose2 = false;
        bool endOfGame = false;
       
        while (true)
        {
            if(endOfGame == false)
            {
                if (isLose == false)
                {
                    PlayerOneMove();

                    if (playerOneX + playerOneSizeX > appleX && playerOneX < appleX + appleSizeX &&
                   playerOneY + playerOneSizeY > appleY && playerOneY < appleY + appleSizeY
                   )
                    {
                        appleX = rnd.Next(0, 700 - appleSizeX);
                        appleY = rnd.Next(200, 900 - appleSizeY);
                        scoreOne++;
                        playerOneSpeed += 10;
                        PlaySound(soundOne);
                    }
                    if (playerOneX + playerOneSizeX > 700 || playerOneX < 0 || playerOneY < 100 || playerOneY + playerOneSizeY > 900)
                    {
                        Console.WriteLine("You lose!");
                        isLose = true;
                    }
                }

                if (isLose2 == false)
                {
                    PlayerTwoMove();
                    if (playerTwoX + playerTwoSizeX > appleX2 && playerTwoX < appleX2 + appleSizeX &&
                        playerTwoY + playerTwoSizeY > appleY2 && playerTwoY < appleY2 + appleSizeY
                        )
                    {
                        appleX2 = rnd.Next(700, 1400 - appleSizeX);
                        appleY2 = rnd.Next(200, 900 - appleSizeY);
                        scoreTwo++;
                        playerTwoSpeed += 10;
                        PlaySound(soundTwo);
                    }
                    if (playerTwoX + playerTwoSizeX > 1400 || playerTwoX < 700 || playerTwoY < 100 || playerTwoY + playerTwoSizeY > 900)
                    {
                        isLose2 = true;
                    }
                }
            }

            if (isLose == true)
            {
                endOfGame = true;
                numberPLayer = "второй";
                bestScore = scoreTwo;
            }
            if (isLose2 == true)
            {
                endOfGame = true;
                numberPLayer = "первый";
                bestScore = scoreOne;
            }
            if(scoreTwo == 10 || scoreOne == 10)
            {
                endOfGame = true;
                if(scoreOne > scoreTwo)
                {
                    bestScore = scoreOne;
                    numberPLayer = "первый";
                }
                if (scoreOne < scoreTwo)
                {
                    bestScore = scoreTwo;
                    numberPLayer = "второй";
                }
            }
          
            // 1. Расчет
            DispatchEvents();
            
            // Игровая логика

            // 2. Очистка буфера и окна
            ClearWindow();

            // 3. Отрисовка буфера на окне
            DrawSprite(backGroundTexture, 0, 0);
           
            SetFillColor(Color.Magenta);
            DrawLine(700, 0, 700, 900);

            if (endOfGame)
            {
                SetFillColor(70, 70, 70);
                DrawText(400, 400, "Победил " + numberPLayer + " игрок со счётом " + bestScore, 40);
            }
           
            // Вызов методов отрисовки объектов
            DrawplayerOne();
            DrawplayerTwo();
            Drawapple();
            Drawapple2();

            SetFillColor(70, 70, 70);
            DrawText(10, 10, "Очки первого игрока: " + scoreOne.ToString(), 20);
            SetFillColor(70, 70, 70);
            DrawText(710, 10, "Очки второго игрока: " + scoreTwo.ToString(), 20);
            
            DisplayWindow();
            Delay(1);
        }
    }
}