using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invintoryStackerClassesTesting;
using testingInvitoryStacker;

namespace NathanielBall_testingInvitoryStacker
{
    internal class MainAppService:IShelfStockingInterface
    {
        public box checkId(int iD, HashSet<box> boxList)
        {
            foreach (box box in boxList)
            {
                if (iD == box.iD)
                {
                    return box;
                }
            }
            throw new Exception("box ID search unsecsefful");
        }
           

        public int[] searchForIndex(string[,] array, string target)
        {
            target = fillX(target, 5);
            int rows = array.GetLength(0); // Number of rows
            int cols = array.GetLength(1); // Number of columns

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j] == target)
                    {
                        return new int[] { i, j }; // Return the row and column indices
                    }
                }
            }
            return null; // Element not found
        }
           
        public void stockBox(box boxDetails, string[,] array, HashSet<box> infoArray, bool display)
        {
            // no dupes can be added
            if (display)
            {
                Console.WriteLine("NEW BOX BEING ADDED");
                Console.WriteLine("new box iD: " + fillX(boxDetails.iD.ToString(), 3));
            }
            foreach (box item in infoArray)
            {
                //Console.WriteLine("box id from HashSet: " + fillX(item.iD.ToString(), 3));
                if (fillX(boxDetails.iD.ToString(), 3) == fillX(item.iD.ToString(), 3))
                {
                    Console.WriteLine("box already in array");
                    return;
                }
            }
            int rows = array.GetLength(0); // Number of rows
            int cols = array.GetLength(1); // Number of columns

            for (int j = 0; j < cols; j++)
            {
                //Console.WriteLine("column number: " + j);
                for (int i = rows - 1; i >= 0; i--)
                {
                    boxDetails.canFit = true; // reset after a fail
                    //boxDetails.canFit = isEmpty(array[i, j]);
                    if (isEmpty(array[i, j]))
                    {
                        //Console.WriteLine();
                        //Console.WriteLine("starting process");
                        boxDetails.canFit = startSearchSpace(boxDetails, array, i, j, false); // this is the real start of everything, program will only continue if this results in true
                        //Console.WriteLine("after isEmpty was true, canFit is: " + boxDetails.canFit);
                        if (boxDetails.canFit)
                        {
                            startSearchSpace(boxDetails, array, i, j, true);
                            //Console.WriteLine("madeIt.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            infoArray.Add(boxDetails); // to remove, reshuffle, or avoid dupes later
                            return;// end adding, it has worked
                        }
                        break; // if is empty is correct, but startSearch fails to be a valid space for a box:
                               // there is no point in checking the indexes above as they will all fail the ground check anyway
                    }
                }
            }
            Console.WriteLine("box was unable to be added");
        }    
        public bool startSearchSpace(box boxDetails, string[,] array, int i, int j, bool replacing)
        {
            //int jStart = j;
            boxDetails.canFit = hasGround(array, i, j); //easiest way to start function as a fail avoids getting sent to the loop, even if this index (i-1,j) gets checked twice
            //Console.WriteLine(boxDetails.canFit);
            //bool unfinshed = true;
            for (int x = 0; x < boxDetails.width && boxDetails.canFit == true; x++)
            {
                //Console.WriteLine(j+x);
                nextSearch(boxDetails, array, i, j + x, replacing, i); // doesnt actualy print during the replace phase, is still required to check ground for check phase
                if (boxDetails.canFit)
                {
                    upSearch(boxDetails, array, i, j + x, replacing, boxDetails.hight); // the only one that prints
                }
            }

            return boxDetails.canFit;
        }    
        public bool hasGround(string[,] array, int i, int j)
        {
            if (i == array.GetLength(0) - 1)
            {
                return true;
            }
            else if (removeX(array[i + 1, j]) != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }   
        public int upSearch(box boxDetails, string[,] array, int i, int j, bool replacing, int count)
        {
            if (boxDetails.canFit == true && count > 0) // if failed, then something else failed first
            {
                try
                {
                    boxDetails.canFit = isEmpty(array[i, j]);
                    if (boxDetails.canFit)
                    {
                        //Console.WriteLine("upSearch count is: " + count);
                        count = upSearch(boxDetails, array, i - 1, j, replacing, count - 1);
                    }
                    else
                    {
                        throw new Exception("something made going up invalid");
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.ToString()); this happens A LOT LOL, but its expected and ok
                    boxDetails.canFit = false;
                }

                // ending, and the only one that actualy incerts the box into the shelf array
                if (replacing)
                {
                    array[i, j] = fillX(boxDetails.iD.ToString(), 3);
                }
            }
            return count;
        }

        public void nextSearch(box boxDetails, string[,] array, int i, int j, bool replacing, int count)// NEED TO ADD STOPER // make start of upSearch
        {
            if (boxDetails.canFit == true) // if failed, then something else failed first
            {
                boxDetails.canFit = hasGround(array, i, j);
                if (boxDetails.canFit)
                {
                    boxDetails.canFit = isEmpty(array[i, j]);
                }

                //ending
                //if (replacing)
                // {
                //     array[i, j] = fillX(boxDetails.iD.ToString(), 3);
                // }
            }
            //return;
        }


        public void removeBox(int iD, string[,] array, HashSet<box> boxList)
        {
            Console.WriteLine("Removing box: " + iD);
            try
            {
                box targetBox = checkId(iD, boxList);
                //if (targetBox != null) { }
                boxList.Remove(targetBox);
                shuffle(array, boxList);
            }
            catch (Exception e)
            {
                Console.WriteLine("box not found");
            }
        }
        public  bool isEmpty(string str)
        {
            string current = removeX(str);
            //current = removeX(str);
            //Console.WriteLine("checking " + i + " " + j + ": " + current);
            if (current == "")
            {
                //Console.WriteLine("wow its empty");
                //return new int[] { i, j }; // Return the row and column indices
                return true;
            }
            return false;
        }

        public  string fillX(string str, int count) // the reason for this is so that the whole shelf can keep its shape no matter what
        {
            if (str.Length < count)
            {
                for (; str.Length < count;)
                {
                    str = "x" + str;
                }
            }
            return str;
        }
        public  string removeX(string str) // clean up the shape holders
        {
            //Console.WriteLine(str);
            try
            {
                while (str.Substring(0, 1) == "x")
                {
                    str = str.Substring(1);
                }
            }
            catch { return str; }
            //Console.WriteLine(str);
            return str;
        }

        public void setUp(string[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                //Console.WriteLine($"Layer {i}:"); // setUp goes from (left->right)->(top->bottom) top being 0
                                                    // other functions do (bottom->top)->(left->right) 
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    /*
                    int x = (i*array.GetLength(1)) + (j+1);
                    string xString = x.ToString();
                    xString = fillX(xString, 3);
                    
                    array[i, j] = xString;// */
                    array[i, j] = "xxx";
                    //Console.Write(array[i, j] + " ");
                }
                //Console.WriteLine();
            }
        }
        public void shuffle(string[,] array, HashSet<box> boxList) // aside from it being good to have, its nessary to avoid floating boxes after a removal
        {
            //use a hasSet with box iDs
            //HashSet<string> set = new HashSet<string>();
            //boxList.Comparer
            List<box> sortedBoxList = boxList.ToList();
            sortedBoxList.Sort();
            sortedBoxList.Reverse();
            boxList.Clear();
            setUp(array);//to reset the shelf
            foreach (box box in sortedBoxList)
            {
                Console.WriteLine(box.ToString());
                //boxList.Add(box);
            }
            foreach (box box in sortedBoxList)
            {
                stockBox(box, array, boxList, false);
            }
            //printAll(array);
            Console.WriteLine("Shuffle complete");
        }
        public void printAll(string[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.WriteLine($"Layer {i}:");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
       
       
    }
}
