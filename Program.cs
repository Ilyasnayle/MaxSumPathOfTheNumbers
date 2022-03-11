using System.Text.RegularExpressions;

/*
 * Author: Ilyas Nayle
 * Email: ilyasnayle5@gmail.com // ilyas.nayle@tedu.edu.tr
 * https://www.linkedin.com/in/ilyasnayle/

 */

namespace MaximumSumOfTheNumbers
{
    public static class Solution 
    {

    /*
        Assignment 3: 
        Write code for below problem. The input below is just an example and you should implement independent from the input. Please paste the link to the answer shared using pastebin, dotnetfiddle, jsfiddle, or any other similar web-site.
        You will have an orthogonal triangle input from a file and you need to find the maximum sum of the numbers according to given rules below;

        1. You will start from the top and move downwards to an adjacent number as in below.
        2. You are only allowed to walk downwards and diagonally.
        3. You can only walk over NON PRIME NUMBERS.
        4. You have to reach at the end of the pyramid as much as possible.
        5. You have to treat your input as pyramid.

        According to above rules the maximum sum of the numbers from top to bottom in below example is 24.

              *1
             *8 4
            2 *6 9
           8 5 *9 3

        As you can see this has several paths that fits the rule of NOT PRIME NUMBERS; 1>8>6>9, 1>4>6>9, 1>4>9>9
        1 + 8 + 6 + 9 = 24.  As you see 1, 8, 6, 9 are all NOT PRIME NUMBERS and walking over these yields the maximum sum.

        You can implement by using any programming language except Mathlab. Please paste the link to your code.
     */
     private const string Input = @" 1
                                     8 4
                                     2 6 9
                                     8 5 9 3";
     /*
      Assignment 4:
         According to assignment in 3 that you implemented what is the maximum sum of below input? 
         It means please take this input 
         (as file or constants directly inside the code) for your implementation and solve by using it.
         
            215
            193 124
            117 237 442
            218 935 347 235
            320 804 522 417 345
            229 601 723 835 133 124
            248 202 277 433 207 263 257
            359 464 504 528 516 716 871 182
            461 441 426 656 863 560 380 171 923
            381 348 573 533 447 632 387 176 975 449
            223 711 445 645 245 543 931 532 937 541 444
            330 131 333 928 377 733 017 778 839 168 197 197
            131 171 522 137 217 224 291 413 528 520 227 229 928
            223 626 034 683 839 053 627 310 713 999 629 817 410 121
            924 622 911 233 325 139 721 218 253 223 107 233 230 124 233
            
      */
     private const string Input2 = @" 215
                                      193 124
                                      117 237 442
                                      218 935 347 235
                                      320 804 522 417 345
                                      229 601 723 835 133 124
                                      248 202 277 433 207 263 257
                                      359 464 504 528 516 716 871 182
                                      461 441 426 656 863 560 380 171 923
                                      381 348 573 533 447 632 387 176 975 449
                                      223 711 445 645 245 543 931 532 937 541 444
                                      330 131 333 928 377 733 017 778 839 168 197 197
                                      131 171 522 137 217 224 291 413 528 520 227 229 928
                                      223 626 034 683 839 053 627 310 713 999 629 817 410 121
                                      924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
     
     private const string TestInput = @" 1
                                         8 4
                                         2 6 9
                                         8 5 2 3";
     
     
     // <Main>
     public static void Main(string[] args)
    {

            int [,] twoDimensionArray = Input.Format2DArray() 
                                        ?? throw new ArgumentNullException("Input.Format2DArray()");
            int sum = twoDimensionArray.MovingTowardsDownwards();

            switch (sum) 
            {
                case 0:
                case -1:
                    Console.WriteLine("No possible path");
                    break;
                default:
                    Console.WriteLine("The Sum of Assignment 3 is: "+ sum);
                    break;
            }
            
            //Read a the triangle given from a file 
            Console.WriteLine("\nPlease, enter the file name 'e.g: input2.txt': ");

            string readFile = Console.ReadLine() 
                              ?? throw new InvalidOperationException
                              {
                                  HelpLink = null,
                                  HResult = 0,
                                  Source = null
                              };
            if (readFile == null)
                throw new ArgumentNullException(nameof(readFile));
            string data = File.ReadAllText(readFile);

            int[,] twoDimensionArray2 = data.Format2DArray();
            if (twoDimensionArray2 == null)
                throw new ArgumentNullException(nameof(twoDimensionArray2));
            
            int max = twoDimensionArray2.MovingTowardsDownwards();
            
            switch (max)
            {
                case 0:
                case -1:
                    Console.WriteLine("No possible path");
                    break;
                default:
                    Console.WriteLine("The total sum for input2 ( Assignment 4 ) is: " + max);
                    break;
            }
    }
    
       
      
    /*
    Check for prime numbers
    */
    private static  bool IsItPrime(this int num)
    {
        
        if((num &1 )==0)
        {
            if(num == 2)
            // Console.WriteLine("t");
                return true;
            else
                return false;

        }          
           // bool ans = false;
        for (var i = 3; (i * i) <= num; i +=2 )
        {
                
            if(num % i == 0)
            {   
                //ans = true;
                //break;
                return false;
            }
        }
        //Console.WriteLine(ans);
        return num != 1;
        
    }
    
        /*
        Calculate Max Value
        */
        private static int MaxValue(int val1, int input)
     {  
        // int max = int.MinValue;
        
         //for (var i = 0; i < max; i++)
        if( val1 == -1 && input == -1 || val1 == 0 && input == 0)
            return -1;
        else
            return Math.Max(val1,input);
     }

     
    /*
     replace the prime number with -1
    */
    
   private static  int [,] RemovePrimeNum(this int [,] grid)
    {
        int size = grid.GetLength(0);
        // Generate primes
        //sieve();    

        for(var  x = 0; x < size; x++)
        {
            for(var y = 0; y < size; y++)
            {
                if(grid[x,y] == 0 )
                    continue;
                
                else if ( IsItPrime( grid[x,y] ) )
                    grid[x,y] = -1; 
                
            }
        }
        //Console.WriteLine(grid);
        return grid;
    }
    
   /*
    * Create or Format a 2-D array from the given input or given file.
    */
    private static int [,] Format2DArray(this string input)
    {
        string[] array =
            input.Split(new[]
                    {Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        int[,] grid = new int[array.Length, array.Length + 1];

        for(var aa = 0; aa < array.Length; aa++)
        {
            int[] digits = Regex.Matches(array[aa], 
                    "[0-9]+").Cast<Match>()
                .Select(line => int.Parse(line.Value)).ToArray();
            
            if (digits == null) 
                throw new ArgumentNullException(nameof(digits));

            for (var bb = 0; bb < digits.Length; bb++)
                 grid[aa, bb] = digits[bb];
             
        }
        // Console.WriteLine(grid.removePrimeNum());
        return grid.RemovePrimeNum();

    }

    
    
    //Moving Downwards method
        private static int MovingTowardsDownwards(this int [,] grid)
        {
            int size = grid.GetLength(0);

          int result = -1;
          
            for (int a = 0; a < size - 2; a++)
                result = Math.Max(result, grid[0, a]);
     
            for (int b = 1; b < size; b++)
            {
                result = -1;
                
                for (int c = 0; c < size; c++)
                {
                    if (c == 0 && grid[b, c] != -1)
                    {
                        if (grid[b - 1, c] != -1)
                            grid[b, c] += grid[b - 1, c];
                        else
                            grid[b, c] = -1;
                        
                            
                    }
                    
                    else if (c > 0 && c < size - 1 && grid[b, c] != -1)
                    {
                        int tempValue = MaxValue(grid[b - 1, c],grid[b - 1, c - 1]);
                        
                        if (tempValue == -1)
                            grid[b, c] = -1;
                        else
                            grid[b, c] += tempValue;
                        
                            
                    }

                    else if (c > 0 && grid[b, c] != -1)
                    {
                        int tempValue = MaxValue(grid[b - 1, c],grid[b - 1, c - 1]);
                        
                        if (tempValue == -1)
                            grid[b, c] = -1;
                        else
                            grid[b, c] += tempValue;
                    }
                    
                    else if (c != 0 && c < size - 1 && grid[b, c] != -1)
                    {
                        int tempValue = MaxValue(grid[b - 1, c],grid[b - 1, c - 1]);
                        if (tempValue == -1)
                            grid[b, c] = -1;
                        else
                            grid[b, c] += tempValue;
                    }
                    
                    result = Math.Max(grid[b, c], result);
                    
                }
            } 
            return result;
        }
    }
}