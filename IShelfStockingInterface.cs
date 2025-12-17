using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invintoryStackerClassesTesting;

namespace NathanielBall_testingInvitoryStacker
{
    public interface IShelfStockingInterface
    {
        box checkId(int iD, HashSet<box> boxList);
        int[] searchForIndex(string[,] array, string target);
        void stockBox(box boxDetails, string[,] array, HashSet<box> infoArray, bool display);
        bool startSearchSpace(box boxDetails, string[,] array, int i, int j, bool replacing);
        bool hasGround(string[,] array, int i, int j);
        int upSearch(box boxDetails, string[,] array, int i, int j, bool replacing, int count);
        void nextSearch(box boxDetails, string[,] array, int i, int j, bool replacing, int count);
        void removeBox(int iD, string[,] array, HashSet<box> boxList);
        bool isEmpty(string str);
        string fillX(string str, int count);
        string removeX(string str);
        void setUp(string[,] array);
        void shuffle(string[,] array, HashSet<box> boxList);
        void printAll(string[,] array);
    }
}
