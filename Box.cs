// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System;

namespace invintoryStackerClassesTesting
{


    public class box: IComparable<box>
    {
        [Required]
        public int hight;
        [Required]
        public int width;
        [Required]
        public int iD;
        [Required]
        protected string filledWith;
        protected int totalSize;
        protected int totalSizeSort;
        public bool canFit;

        public int Hight { get; set; }
        public int Width { get; set; }
        public int ID { get; set; }
        public string FilledWith { get; set; }
        public int TotalSize { get; set; }
        public int TotalSizeSort { get; set; }
        public bool CanFit { get; set; }

        public box(int hight, int width, int iD, string filledWith)
        {
            this.hight = hight;
            this.width = width;
            this.iD = iD;
            this.filledWith = filledWith;
            this.totalSize = hight * width; //this is for other posable math
            this.totalSizeSort = int.Parse(hight.ToString() + width.ToString()); //this is for the sort function, this make hight worth 10*x number of digits in width
            this.canFit = true;
        }

        public int CompareTo(box other)
        {
            // Sort by Score in descending order (or other custom logic)
            //int total = hight * width + width;
            return this.totalSizeSort.CompareTo(other.totalSizeSort);
        }

        public override bool Equals(object obj)
        {
            return obj is box other && iD == other.iD;
        }

        public override int GetHashCode()
        {
            return iD.GetHashCode();
        }

        public override string ToString()
        {
            return "hight: " + this.hight + ", width" + this.width + ", ID: "+ this.iD;
        }
    }




}