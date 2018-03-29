using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        // I am using 0 and 1  - instead of 0 and X, below is for consideration
        // 0 -- O 
        //1 --  X
        static void Main(string[] args)
        {
             
            try
            {
                Random rnd = new Random();

                if (args.Length == 0)
                {
                    Console.WriteLine("No argument is passed ,Please Pass argument !!");
                    return;
                }
                if (args.Length > 1)
                {
                    Console.WriteLine("More than 1 argument passed, Please pass a single argument");
                    return;
                }

                //N - board size
                int N = Convert.ToInt32(args[0]);


                //string[] track = new string[20]; // array to keep track of the moves
                //p1 and p2 are the co-ordinates used to place the choice in the array.
                int p1 = -1, p2 = -1;

                //first -- the one who gets a chance to play first based on random choice
                //second - the one who plays second
                int first, second;

                //Count - variable to iterate and give chances to each alternatively.
                //flag -  which gets the return value from the function and decided to continue or stop the game
                int count = 0, flag = -1;

                //checking for the constraint and throwing custom error.
                if (N < 2 || N > 10)
                {
                    throw new Exception("Invalid entry"); // Custom Error
                }

                //Declaring the TicTacToe(TTT) in a integer format from the provided boardsize.
                int[,] TTT = new int[N,N];

                //Intialising all the board to -1.
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        TTT[i,j] = -1;
                    }
                }
                
                
                // 0 ---O
                //1 --X
                // Deciding Randomly whom is going to play first.
                first = rnd.Next(2);

                second = first == 0 ? 1 : 0;

                if(first == 0)
                    Console.WriteLine("O goes first");
                else
                    Console.WriteLine("X goes first");
           
                
                
                while (true)
                {
                    
                    if (count % 2 == 0)
                    {
                        //first try to fill the board

                        //again  while loop to find the free spot and enter.
                        while (true)
                        {
                            p1 = rnd.Next(N);
                            p2 = rnd.Next(N);
                            if (TTT[p1, p2] == -1)
                            {// free spot found
                                TTT[p1, p2] = first;
                               // track[count] = Convert.ToString(p1) + Convert.ToString(p2); // Commented because used to track down the spots
                                count++;
                                break;
                            }

                        }
                        flag = isGameDone_Continue(N, TTT);
                        if (flag != -1)
                            break;
                    }
                    else
                    {
                        //second fills the spot
                        //again  while loop to find the free spot and enter.
                        while (true)
                        {
                            p1 = rnd.Next(N);
                            p2 = rnd.Next(N);
                            if (TTT[p1, p2] == -1)
                            {// free spot found
                                TTT[p1, p2] = second;
                                //  track[count] = Convert.ToString(p1) + Convert.ToString(p2); // Commented because used to track down the spots
                                count++;
                                break;
                            }

                        }
                        flag = isGameDone_Continue(N, TTT);
                    }

                   
                    
                    if(flag != -1)
                    break;
                    if (!isBoardfull(TTT))
                        continue;
                    else
                        break;
                }

               

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (TTT[i, j] == 0)
                            Console.Write("O");
                        else if (TTT[i, j] == 1)
                            Console.Write("X");
                        else
                            Console.Write("#");
                       
                    }
                    Console.WriteLine();
                }
                //-this checkpoint if the board is full and comes here , we will check here.
                flag = isGameDone_Continue(N, TTT);
                if (flag == first)
                {
                    if (first == 0)
                        Console.WriteLine("O won");
                    else
                        Console.WriteLine("X won");
                }
                else if (flag == second)
                {
                    if (second == 0)
                        Console.WriteLine("O won");
                    else
                        Console.WriteLine("X won");
                }
                else
                    Console.WriteLine("It was a Draw");


                //below commented line is to display the track 
                //- I have used to check who the spots are getting filled
                //for(int i = 0; i <count;i++)
                //{
                //    Console.WriteLine(track[i]);
                //}


            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Expection occured !!! please Close and Open it again");
               
                
            }
            
        }

        //checking board is full or not 
        // returns false if it find a value '-1' in the array.
        private static bool isBoardfull( int[,] tTT)
        {
            foreach (var item in tTT)
            {
                if (item == -1)
                   return  false;
            }
            return true;
        }

        // Returns -1 for Continue
        // Retuns 0 for  Letter O wins
        // Returns 1 for Letter X wins
        private static int isGameDone_Continue(int N, int[,] tTT)
        {
            //rowsum -adding all rows if 0 as value 9
            //columnsum - adding all columns as value 10
            //adding diagonal elements  - if 0 as 9 if it is 1 as 10
            //adding all minor diagonal elements (right Top corneer to bottom left corrnor) - if 0 as 9 if it is 1 as 10 
            int rowsum = 0, columnsum = 0,diagSum =0 ,mindiag =0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (tTT[i, j] == 0)
                    {
                        
                        rowsum = rowsum + 9;
                        if((i + j) == (N-1))
                        { mindiag = mindiag + 9; }
                        if (i == j)
                            diagSum = diagSum + 9;
                    }
                    if (tTT[i, j] == 1)
                    {

                        rowsum = rowsum + 10;
                        if ((i + j) == (N - 1))
                        {
                            mindiag = mindiag + 10;
                        }
                            if (i == j)
                            diagSum = diagSum + 10;
                    }
                    if (tTT[j,i] == 1)
                    {

                   
                        columnsum = columnsum + 10;
                        
                    }
                    if (tTT[j,i] == 0)
                    {

                   
                        columnsum = columnsum + 9;
                      
                    }
                }
                if (rowsum == (9*N) || columnsum == (9*N))
                {
                    return 0;
                }
                if (rowsum == (10*N) || columnsum == (10*N))
                {
                    return 1;
                }
                else
                {
                    rowsum = 0;
                    columnsum = 0;
                }
            }
            if (diagSum == 9*N)
                return 0;
            if (diagSum == 10*N)
                return 1;
            if (mindiag == 9 * N)
                return 0;
            if (mindiag == 10 *N)
                return 1;

            return -1;
        }

       
    }
}
