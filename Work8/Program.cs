
string[,] menu = {{" ","Начать игру"," "},
                  {" ","Выбор уровня"," "},
                  {" ","Выход"," "}};
string[,] menuLvl = {{" ","ВЫБЕРИТЕ УРОВЕНЬ"," "},
                     {" ","______________"," "},
                     {" ","Первый уровень"," "},
                     {" ","Второй уровень"," "},
                     {" ","Третий уровень"," "},
                     {" ","Четвертый уровень"," "}};
string[,] matrix= {{" "," "," "," "," "},
                   {" ","|"," ","|"," "},
                   {" ","|","@","|"," "},
                   {" "," "," "," "," "},
                   {" "," "," ","$"," "},
                   {" "," "," "," "," "}};
Dictionary<int, string[,]> Lvls = new Dictionary<int, string[,]> {{1,
new string[,] {{"|"," "," "," ","|"},
               {"|"," "," "," ","|"},
               {"|"," ","@"," ","|"},
               {"|"," "," "," ","|"},
               {"|"," "," ","$","|"},
               {"|"," "," "," ","|"}}},
               {2,
new string[,] {{" "," "," ","|","|"},
               {" "," "," ","|"," "},
               {" ","|","@","|"," "},
               {" "," "," "," "," "},
               {" "," "," ","$"," "},
               {"|"," "," "," "," "}}},
               {3,
new string[,] {{"|","|","|","|","|"},
               {"|"," "," "," ","|"},
               {"|"," ","@"," ","|"},
               {"|"," "," "," ","|"},
               {"|"," "," ","$","|"},
               {"|","|","|","|","|"}}},
               {4,
new string[,] {{"|"," "," "," ","|"},
               {" ","|"," "," "," "},
               {" "," ","@","|"," "},
               {"|"," "," "," "," "},
               {" "," "," ","$","|"},
               {" ","|"," "," "," "}}}};
int coins = 0;
int menuX = 0;
int menuY = 0;
int SelectLevlGame()
{
   int indexMenu = 0;
    MaxtrixWrite(menuLvl);
    ConsoleKeyInfo User_keyTab = Console.ReadKey();
    while(User_keyTab.Key != ConsoleKey.Enter)
    {
        Console.Clear();
        menuLvl[menuY,menuX] = " ";
        if(User_keyTab.Key == ConsoleKey.W && indexMenu > 1)
        {
            indexMenu--;
            menuY--;
        }
        if(User_keyTab.Key == ConsoleKey.S && indexMenu < 4)
        {
            indexMenu++;
            menuY++;
        }
        menuLvl[menuY,menuX] = "@";
        MaxtrixWrite(menuLvl);
        User_keyTab = Console.ReadKey();
    }
    return indexMenu;
}
int SelectMenuPlayer()
{
    int indexMenu = 0;
    MaxtrixWrite(menu);
    ConsoleKeyInfo User_keyTab = Console.ReadKey();
    while(User_keyTab.Key != ConsoleKey.Enter)
    {
        Console.Clear();
        menu[menuY,menuX] = " ";
        if(User_keyTab.Key == ConsoleKey.W && indexMenu > 0)
        {
            indexMenu--;
            menuY--;
        }
        if(User_keyTab.Key == ConsoleKey.S && indexMenu < 3)
        {
            indexMenu++;
            menuY++;
        }
        menu[menuY,menuX] = "@";
        MaxtrixWrite(menu);
        User_keyTab = Console.ReadKey();
    }
    return indexMenu;
}

void MaxtrixWrite(string[,] array)
{
    for (int i = 0; i < array.GetLength(0); i++)
    {
        for (int j = 0; j <array.GetLength(1); j++)
        {
            System.Console.Write(array[i,j] + " ");
        }
        System.Console.WriteLine();
    }
    System.Console.WriteLine("Кол-во очков " + coins );
}

string[,] ItemFoodMatrix(int x, int y, string[,] array)
{
    while (matrix[y,x] == "$")
    {
        matrix[y,x] = " ";
        int matX = new Random().Next(0, array.GetLength(1));
        int matY = new Random().Next(0, array.GetLength(0));
        while(matrix[matY, matX] == "|")
        {
            matX = new Random().Next(0, array.GetLength(1));
            matY = new Random().Next(0, array.GetLength(0));
        }
        array[matY,matX] = "$";
        coins++;
    }
    return array;
}

bool Barrier(int x,int y)
{
    if(matrix[y,x] == "|") return false;
    return true;
}

int x = 2;
int y = 2;

while (true)
{
    switch (SelectMenuPlayer())
    {
        case 0:
            Console.Clear();
            Game();
            break;
        case 1:
            Console.Clear();
            matrix = Lvls[SelectLevlGame()];
            Game();
            break;
        case 2:
            Console.Clear();
            break;
        default:
            break;
    }
}

void Game()
{
    while (true)
    {
        Console.Clear();
        MaxtrixWrite(matrix);
        matrix[y,x] = " ";
        ConsoleKeyInfo User_KeyTab = Console.ReadKey();
        if(User_KeyTab.Key == ConsoleKey.W) if(Barrier(x, y - 1)) y--;
        if(User_KeyTab.Key == ConsoleKey.S) if(Barrier(x, y + 1)) y++;
        if(User_KeyTab.Key == ConsoleKey.A) if(Barrier(x - 1, y)) x--;
        if(User_KeyTab.Key == ConsoleKey.D) if(Barrier(x + 1, y)) x++;

        if(y >= matrix.GetLength(0)) y = 0;
        if(y <= 0) y = matrix.GetLength(0);

        if(x >= matrix.GetLength(1)) x = 0;
        if(x <= 0) x = matrix.GetLength(1);
    
        matrix = ItemFoodMatrix(x,y,matrix);
        matrix[y,x] = "@";
    }
}