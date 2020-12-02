using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_amilator
{

    

    class Program
    {

        static EOperations Operation = 0;

        string[] lines;

        char emptychar = '.';
        char[] hbits;

        int yl = 0;
        int xl = 0;

        int ycenterindex;
        int xcenterindex = 0;

        static int[] defmultiplier = new int[] { 1, 1, 0, 0 };

    bool doDrawGraph = true;

        string[] positiveY; //index 0 -> ycenter
        string zeroLine; //index == ycenter
        string[] negativeY; //index ycenter -> lines.length

        Program()
        {
            //Console.WriteLine("Please Choose your Operation of Choice(NONE) for no operations:");
            Console.Write("use Template?: (Y/N)");
            string yesno = Console.ReadLine();
            switch (yesno)
            {
                case "Y":
                    yl = 10;
                    xl = 20;
                    break;
                default:
                    Console.WriteLine("Please Enter the length of y-Axis and x-Axis as integer beetwin 20 and 100");
                    Console.Write("y:"); yl = int.Parse(Console.ReadLine());
                    Console.Write("x:"); xl = int.Parse(Console.ReadLine());
                    break;
            }
            ycenterindex = yl - 1;
            yl = (yl * 2) - 1;


            lines = new string[yl];

            for (int i = 0; i < lines.Length; i++)
            {
                string holder = "";
                for (int j = 0; j < xl; j++)
                {

                    //if you reach here it means your room wasn't an answer to calculation
                    //otherwise the loop would continue to next id
                    if (i == ycenterindex && j == 0)
                    {
                        holder += 'O';
                    }
                    else if (i == ycenterindex)
                    {
                        holder += j % 10;
                    }
                    else if (j == 0)
                    {
                        //Draw the Axis Labels with numbers HERE
                        if (i > ycenterindex)
                        {//-Y
                            holder += ((i - ycenterindex) % 10);
                        }
                        if (i < ycenterindex)
                        {//+Y
                            holder += -((i) - ycenterindex) % 10;
                        }
                    }
                    else
                    {
                        holder += emptychar;
                    }
                }
                lines[i] = holder;
            }

            // this -> charArray -> index = X
            // this -> positiveY,X -> index = Y

            positiveY = new string[((yl - 1) / 2)];
            negativeY = new string[((yl - 1) / 2)];
            zeroLine = lines[((yl - 1) / 2)];
            for (int i = 0; i < ((yl - 1) / 2); i++)
            {
                positiveY[i] = lines[i];
            }
            for (int i = ((yl - 1) / 2) + 1; i < lines.Length; i++)
            {
                int j = i - (((yl - 1) / 2) + 1);
                negativeY[j] = lines[i];
            }
            Array.Reverse(positiveY);

            int[] answersInt = new int[xl];
            double[] answersDouble = new double[xl];
            for (int i = 0; i < xl; i++)
            {
                double answer;
                answer = Func(i, new int[] { 1, 1, 0, 0 }, 2);
                answer = Func(i);
                ReplaceNodeWithChar(i, (int)answer,'*');
                ReplaceNodeWithChar(i, (int)Math.Floor(answer) - 1, '+');


                //Tabel Related
                answersInt[i] = (int)answer;
                answersDouble[i] = answer;
            }
            

            //Drawing the graph
            for (int i = positiveY.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(positiveY[i]);
            }
            Console.WriteLine(zeroLine);
            for (int i = 0; i < positiveY.Length; i++)
            {
                Console.WriteLine(negativeY[i]);
            }
            Console.WriteLine(" ");
            Console.WriteLine("----------------------------------------------");

            //Draw the Answer Table
            Console.Write("\n -------Table of Answers--------\n");
            Console.Write(" X   |   Y \n");
            for (int i = 0; i < xl; i++)
            {
                Console.Write($" {i}   |   {answersDouble[i]} \n");
            }
            Console.Write("\n -NOTE that Answers on graph are integer so for floating point is bitween + and * sign-\n");

            Console.ReadKey();
        }

        private void ReplaceNodeWithChar(int xIn,int yIn,char cIn = ' ')
        {
            if (yIn > (yl - 1) / 2) return;
            xIn = Math.Abs(xIn);
            if(yIn == 0)
            {
                var cs = zeroLine.ToCharArray();
                cs[xIn] = cIn;
                zeroLine = new string(cs);
                return;
            } else if (yIn > 0)
            {
                var cap = positiveY[yIn - 1].ToCharArray();
                cap[xIn] = cIn;
                positiveY[yIn - 1] = new string(cap); // id is value - 1 // forexample if i give 3, it change 4
                return;
            } else if (yIn < 0)
            {
                var can = negativeY[Math.Abs(yIn) - 1].ToCharArray();
                can[xIn] = cIn;
                negativeY[Math.Abs(yIn) - 1] = new string(can); // id is value - 1 // forexample if i give 3, it change 4
                return;
            }
            return;
            switch (yIn/Math.Abs(yIn))
            {
                case +1:
                    var cap = positiveY[yIn - 1].ToCharArray();
                    cap[xIn] = cIn;
                    positiveY[yIn - 1] = new string(cap); // id is value - 1 // forexample if i give 3, it change 4
                    break;
                case -1:
                    var can = negativeY[Math.Abs(yIn) - 1].ToCharArray();
                    can[xIn] = cIn;
                    negativeY[Math.Abs(yIn) - 1] = new string(can); // id is value - 1 // forexample if i give 3, it change 4
                    break;
            }
            
        }

        static double Func(double x,int[] multiplier, double powX = 1D,double powY = 1D)
        {
            double x1 = Math.Pow(x, powX);
            double x2 = SpecialOperator(x1);
            double x3 = Math.Pow(x2, powY);

            //double y = (( multiplier[0] + multiplier[2] ) ^ powY) * multiplier[1] + multiplier[3];
            return x3;
        }
        static double Func(double x)
        {
            //double y = (( multiplier[0] + multiplier[2] ) ^ powY) * multiplier[1] + multiplier[3];
            double x1 = x - 5;
            return (Math.Abs(x1-1)) + (Math.Abs(x1 - 10));
        }
        static double SpecialOperator(double In,EOperations operation = EOperations.NONE)
        {
            double Out;
            double InPi = In * Math.PI * 1/2;
            switch (operation)
            {
                case EOperations.NONE:
                    Out = In;
                    break;
                case EOperations.Abs:
                    Out = Math.Abs(In);
                    break;
                case EOperations.Log:
                    Out = Math.Log10(In);
                    break;
                case EOperations.Sin:
                    Out = 20*Math.Sin(InPi);
                    break;
                case EOperations.Cos:
                    Out = Math.Cos(InPi);
                    break;
                case EOperations.Tan:
                    Out = Math.Tan(InPi);
                    break;
                case EOperations.Cot:
                    Out = (Math.Pow(Math.Tan(InPi),-1D));
                    break;
                case EOperations.Sec:
                    //NOT/IMPLEMENTED
                    Out = Math.Sin(InPi);
                    break;
                case EOperations.Csc:
                    //NOT/IMPLEMENTED
                    Out = Math.Sin(InPi);
                    break;
                default:
                    Out = In;
                    break;
            }
            
            return Out;
        }

        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            new Program();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
