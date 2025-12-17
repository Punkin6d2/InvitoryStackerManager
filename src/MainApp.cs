using invintoryStackerClassesTesting;
using NathanielBall_testingInvitoryStacker;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using testingInvitoryStacker;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace testingInvitoryStacker
{
    internal class MainApp
    {
        
        public static void Main()
        {
            MainAppService service = new MainAppService();
            string[,] shelfArrayString = new string[3, 15];
            service.setUp(shelfArrayString);
            HashSet<box> boxList = new HashSet<box>();
            
 
            Console.WriteLine("***************Original: Empty shelves**************************************************************************");
            service.printAll(shelfArrayString);
            Console.WriteLine("****************************************************************************************************************");


            box box110 = new box(1, 1, 110, "fruit");
            box box111 = new box(1, 1, 111, "fruit");
            box box112 = new box(1, 1, 112, "fruit");
            box box113 = new box(1, 1, 113, "fruit");
            box box120 = new box(1, 2, 120, "fruit");
            box box121 = new box(1, 2, 121, "fruit");
            box box122 = new box(1, 2, 122, "fruit");
            box box210 = new box(2, 1, 210, "fruit");
            box box211 = new box(2, 1, 211, "fruit");
            box box212 = new box(2, 1, 212, "fruit");
            box box220 = new box(2, 2, 220, "fruit");
            box box221 = new box(2, 2, 221, "fruit");
            box box310 = new box(3, 1, 310, "fruit");
            box box311 = new box(3, 1, 311, "fruit");
            box box320 = new box(3, 2, 320, "fruit");
            box box321 = new box(3, 2, 321, "fruit");

            service.stockBox(box310, shelfArrayString, boxList, true);

            Console.WriteLine("***************Adding the first box*****************************************************************************");
            service.printAll(shelfArrayString);
            Console.WriteLine("****************************************************************************************************************");

            service.stockBox(box320, shelfArrayString, boxList, true);
            service.stockBox(box210, shelfArrayString, boxList, true);
            service.stockBox(box120, shelfArrayString, boxList, true);
            service.stockBox(box220, shelfArrayString, boxList, true);
            service.stockBox(box311, shelfArrayString, boxList, true);
            service.stockBox(box110, shelfArrayString, boxList, true);

            Console.WriteLine("***************Original Unoptimized Set up of shelves************************************************************");
            service.printAll(shelfArrayString);
            Console.WriteLine("*****************************************************************************************************************");

            service.shuffle(shelfArrayString, boxList);
            Console.WriteLine("***************Reshuffle called : optimized placement of boxes on shelves****************************************");

            service.printAll(shelfArrayString);
            Console.WriteLine("*****************************************************************************************************************");

            service.removeBox(320, shelfArrayString, boxList);
            service.removeBox(120, shelfArrayString, boxList);
            Console.WriteLine("***************Remove box and reshuffle called on 320, 120 : optimized placement of boxes on shelves*************");

            service.printAll(shelfArrayString);
            Console.WriteLine("*****************************************************************************************************************");


        }
    }
}
