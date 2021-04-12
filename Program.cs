using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //assign variable angle to get the angle value from user input.
            double angle;
            //The speed when launching the ball (The force puted behind the swing).
            double velocity;

            //Generating a random value to assign it for the Cup distance location variable.
            Random rand = new Random();
            double CupLocation = rand.Next(20, 501);
            Console.WriteLine("Golf Game V.2021");
            

            //Create Counter for swings.
            int SwingCount = 0;
            
           
            //Create variable for calculating distance to Cup.
            double DistanceToCup;

            
            //Creating String list To display the records when the goal achieved.
            var result = new List<string>();

            //Create Too Far point value to break the loop and end the game and give an exception.
            double TooFar = CupLocation + 100;



            int loop = 0;
            while (loop <= 12)
            {
                Console.WriteLine("\nThe Cup is far from your location: {0} meters.", CupLocation);

                Console.WriteLine("Enter the angle: ");
                double.TryParse(Console.ReadLine(), out angle);

                Console.WriteLine("Enter the force behind your swing: ");
                double.TryParse(Console.ReadLine(), out velocity);
                SwingCount += 1;



                //Algorithms to Calculating the distance for the ball.
                double AngleInRadian = (Math.PI / 180) * angle;

                double GRAVITY = 9.8;
                double BallDistance = Math.Pow(velocity, 2) / GRAVITY * Math.Sin(2 * AngleInRadian);
                //Converting the value for the ball distance from decimal to nearst integer.
                BallDistance = Math.Round(BallDistance);
                //If the ball travels beyond the cup, the new distance should still be positive.
                DistanceToCup = Math.Abs(CupLocation - BallDistance);

                Console.WriteLine($"Swing count: {SwingCount}\n" +
                    $"Landing ball on: {BallDistance} Meters.\n" +
                    $"Distance To Cup is: {DistanceToCup}");

                //Each swing should move the starting location for each arc.
                Random rand2 = new Random();
                double startPoint = rand.Next(1, 10);
                CupLocation -= startPoint;

                //Creating List of Class Objects.
                var Report = new List<Golf>()
                    {
                     new Golf(){ swing = SwingCount, distance = BallDistance }
                    };

                //Using Linq Lambda.
                Report.ForEach(log => result.Add(Convert.ToString($"Swing {log.swing} : {log.distance} Meters.")));

                /*When the ball has reached the goal, the game should end,
                displaying all swings taken, and how far the ball travelled each time.*/
                if (BallDistance == CupLocation || DistanceToCup == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations You Reach The Goal!!");
                    Console.ResetColor();

                    Console.WriteLine("Your Report: ");

                    foreach (var win in result)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(win);
                        Console.ResetColor();

                    }
                    break;
                }
                


                try
                {
                    //If the ball moves too far away from the cup,
                    //the game should generate an exception that takes you out of the game loop,
                    //with a failure message.
                    if (BallDistance > TooFar)
                    {
                        throw new GolfExceptions("Foul! You hit the ball too far, GAME OVER!");

                    }


                    //If too many swings have been taken, the game should end, with a different failure message.
                    else if (SwingCount >= 10)
                    {
                        throw new GolfExceptions("Too many swings have been taken, GAME OVER");
                    }
                }
                catch (GolfExceptions ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    break;
                }





                loop += 1;
            }




            Console.WriteLine("\nPlease press enter to exit... ");
            Console.ReadKey();
           
        }
    }
}
