using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6.Task1
{
    class Bike : Vehicle
    {
        private double frameSize;
        public double FrameSizeInInches
        {
            get { return frameSize; }
            set { frameSize = Math.Max(0, value); }
        }

        private double wheelSize;
        public double WheelSizeInInches
        {
            get { return wheelSize; }
            set { wheelSize = Math.Max(0, value); }
        }

        public string FrameSizeString()
        {
            if (FrameSizeInInches < 14) return "XS";
            else if (FrameSizeInInches < 16) return "S";
            else if (FrameSizeInInches < 17) return "M";
            else if (FrameSizeInInches < 18) return "L";
            else if (FrameSizeInInches < 19) return "XL";
            else return "XXL";
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nBike has {frameSize} inches frame and {wheelSize} inches wheels";
        }
    }
}
