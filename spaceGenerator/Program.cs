using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace spaceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> printOrder = new List<string>();
            int input = -1;
            bool numberOnly = false;

            //testing 
            try
            {
                //input = Convert.ToInt32(Console.ReadLine());
                //Get input from user. Can not be Negative and must be an integer. 
                Console.Write("Enter the size of the star: ");
                do
                {
                    if(!int.TryParse(Console.ReadLine(),out input))
                    {
                        Console.WriteLine("Please enter a positive number: ");
                    }
                    if(input<0)
                    {
                            Console.WriteLine("Please enter a number larger than 0: ");
                        if (!int.TryParse(Console.ReadLine(), out input))
                        {
                            Console.WriteLine("Please enter a number larger than 0: ");
                        }
                    }
                } while (input <= 0);

                Console.WriteLine();

                printOrder.Add(PrintStars(input));
                printOrder.Add(PrintStars(input));

                for(int c = 0;c < printOrder.Count; c++)
                {
                    Console.WriteLine(printOrder[c]);
                }

                //Console.WriteLine(PrintStars()); 
            }
            finally
            {
                Console.WriteLine("end");
                Console.Read();
            }
            
            
        }
        // This will ask for a size of star and save it inside of a string
        static string PrintStars(int input)
        {

            int x = 0;
            int space = 0;
            int split = 0;
            int spaceR = 0;
            int leadingSpace = 0;
            int difference = 1;
            int growingSpace = 1;
            string savedStar = null;


            split = input / 2;
            leadingSpace = split;
            spaceR = split - difference;
            
            // odd inputs (n) produced too many stars but the same size of the n-1. 
            if (input % 2 != 0)
                input -= 1;

            while (x <= input)
            {
                //Console.WriteLine(x);
                //Console.WriteLine(input);
                //Console.ReadLine();

                space = x;
                // if x is the start and end only have one star. 
                if (x == 0 || x == input)
                {
                    if (leadingSpace <= 0)
                        leadingSpace = 1;

                    for (int c = 0; c < leadingSpace; c++)
                    {
                        //Console.Write(" ");
                        savedStar += " ";
                    }
                    //Console.WriteLine("*");
                    savedStar += "*\n";
                    leadingSpace--;
                }
                else// 2 stars with spaces in between but shrinking amount of spaces. 
                {

                    if (x > split)
                    {
                        if (leadingSpace < 0)
                            leadingSpace = 1;

                        for (int c = 0; c < leadingSpace; c++)
                        {
                            //Console.Write(" ");
                            savedStar += " ";
                        }
                        leadingSpace++;

                        //Console.Write("*");
                        savedStar += "*";

                        // shrink the spaces down.
                        while (spaceR != 0)
                        {
                            //Console.Write(" ");
                            savedStar += " ";
                            spaceR -= 1;
                        }

                        if (x != input)
                        {
                            difference++;
                            spaceR = input / 2 - difference;

                            for (int c = 0; c < growingSpace; c++)
                            {
                                //Console.Write(" ");
                                savedStar += " ";
                            }
                            growingSpace--;
                        }
                        //Console.WriteLine("*");
                        savedStar += "*\n";
                    }// start of the spaces inserted
                    else // 2 stars with spaces growing in between 
                    {
                        for (int c = 0; c < leadingSpace; c++)
                        {
                            //Console.Write(" ");
                            savedStar += " ";
                        }
                        //Console.Write("*");
                        savedStar += "*";

                        while (space != 0)
                        {
                            //Console.Write(" ");
                            savedStar += " ";
                            space -= 1;
                        }
                        if (x > 1)
                        {
                            for (int c = 0; c < growingSpace; c++)
                            {
                                //Console.Write(" ");
                                savedStar += " ";
                            }
                            growingSpace++;
                        }
                        //Console.WriteLine("*");
                        savedStar += "*\n";
                        leadingSpace--;
                    }
                }
                //if x is the same as half reduce the growing spaces by 2
                if (x == split)
                    growingSpace -= 2;

                x++;

            }

            return savedStar;
        }
    }
}
