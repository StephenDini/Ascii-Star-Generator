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
            int input = 0;
            int howMany = 0;
            int counter = 0;
            List<int> sizes = new List<int>();

            //testing 
            try
            {
                //input = Convert.ToInt32(Console.ReadLine());
                //Get how many stars from user. Can not be Negative and must be an integer. 
                Console.Write("How many stars would you like to create? [10 max] ");
                do
                {
                    // for reference: int.TryParse returns True if an integer is input. false if anthing else 
                    // this protects from char from being inputed and throwing and exception
                    if(!int.TryParse(Console.ReadLine(),out howMany))
                    {
                        Console.Write("Please enter a number larger than 0 and less than 11: ");
                    }
                    else if(howMany <= 0 || howMany > 10)
                    {
                        Console.Write("Please enter a number larger than 0 and less than 11: ");
                    }
                } while (howMany <= 0 || howMany > 10 );

                Console.WriteLine();

                //Get the sizes of the stars and place them into a list of sizes
                while (counter < howMany)
                {
                    Console.Write("Please enter the size of star number " + (counter + 1) + ": [max 20] ");

                    do
                    {
                        if (!int.TryParse(Console.ReadLine(), out input))
                        {
                            Console.Write("Please enter a number larger than 0 and less than 21: ");
                        }
                        else if(input <= 0 || input > 20)
                        {
                            Console.Write("Please enter a number larger than 0 and less than 21: ");
                        }
                    } while (input <= 0 || input >20);

                    sizes.Add(input);
                    counter++;
                }

                //testing
                Console.Clear();

                Console.WriteLine("begining Test");
                Console.ReadLine();
                Console.Clear();

                string test = null;
                test = createStarScape(sizes);

                Console.WriteLine(test);
                Console.WriteLine("End Test");

                Console.ReadLine();
                Console.Clear();

                // end testing

                // pass the sizes to the star generator. 
                for (int c = 0; c < sizes.Count(); c++)
                {
                    //debugging
                    //Console.WriteLine(sizes[c]);
                    printOrder.Add(PrintStars(sizes[c]));
                }
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();

                Console.WriteLine();
                Console.Clear();
                
                //old keeping for reference
                //printOrder.Add(PrintStars(input));
                //printOrder.Add(PrintStars(input));

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
        // creating the stars in a char array. 
        static string createStarScape(List<int> sizes)
        {
            string completedScape = null;
            Random randX = new Random(100);
            Random randY = new Random(30);


            // setup for the character array 
            char[,] starScape = new char[30,100];
            for(int c0 = 0; c0 < 30; c0++)
            {
                for(int c1 = 0; c1 < 100; c1++)
                {
                    starScape[c0, c1] = ' ';

                }
            }

            // for loop to set up star creation
            for(int c = 0; c < sizes.Count(); c++)
            {
                int startLocX = randX.Next(99);
                int startLocY = randY.Next(29);
                int newLocX = startLocX;
                int newLocY = startLocY; 
                int half = 0;
                int thisSize = 0;
                int growingSpace = 1;
                bool shrink = false;
                int checkSize = 0;
                if (sizes[c] % 2 !=0)
                {
                    thisSize = (sizes[c] - 1);
                }
                else
                {
                    thisSize = sizes[c];
                }

                for (int x0 = 0; x0 < thisSize; x0++)
                {


                    try
                    {
                        starScape[startLocY, startLocX] = '*';
                    }
                    catch { }

                    while (checkSize < thisSize)
                    {
                        if (shrink)
                        {
                            newLocX += 1;
                        }
                        else
                        {
                            newLocX -= 1;
                        }

                        newLocY += 1;
                        int x1 = 1;

                        try
                        {
                            starScape[newLocY, newLocX] = '*';
                        }
                        catch (IndexOutOfRangeException)
                        { }

                        while (x1 <= growingSpace)
                        {
                            try
                            {
                                starScape[(newLocY), (newLocX + x1)] = ' ';
                            }
                            catch (IndexOutOfRangeException)
                            { }

                            x1++;
                        }
                        if (shrink)
                        {
                            if (growingSpace == 1)
                            {
                                try
                                {
                                    starScape[(newLocY), (newLocX + x1)] = '*';
                                }
                                catch (IndexOutOfRangeException)
                                { }
                                //break;
                            }
                            growingSpace -= 2;
                            
                        }
                        else
                        {
                            growingSpace += 2;
                        }

                        if (growingSpace == (thisSize + 1))
                        {
                            //debuging
                            //Console.WriteLine(growingSpace + " = " + sizes[c]);
                            shrink = true;
                            growingSpace -= 4;
                        }
                        if ((checkSize + 1 ) != thisSize)
                        {
                            try
                            {
                                starScape[(newLocY), (newLocX + x1)] = '*';
                            }
                            catch (IndexOutOfRangeException)
                            { }
                        }

                        checkSize += 1;
                    }
                    try
                    {
                        starScape[(startLocY + thisSize), startLocX] = '*';
                    }
                    catch (IndexOutOfRangeException)
                    { }


            }

            }

            /*example for above
                     *      1 star no space         = 1
                    * *     1 star 1 space 1 star   = 3
                   *   *    1 star 3 space 1 star   = 5
                  *     *   1 star 5 space 1 star   = 7
                   *   *    1 star 3 space 1 star   = 5
                    * *     1 star 1 space 1 star   = 3
                     *      1 star no space         = 1
            */

            // Turn the character array into a string
            for (int c0 = 0; c0 < 30; c0++)
            {
                for(int c1 = 0; c1 < 100; c1++)
                {
                    completedScape += starScape[c0, c1];
                    if(c1 == 99)
                    {
                        completedScape += "\n";
                    }
                }
            }



            return completedScape;
        }

        // This code is being reworked on.
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

            // padding for the leading spaces for its location
            // might have to rethink how i go about this. 
            Random random = new Random();
            int xpadding = random.Next(50);

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
