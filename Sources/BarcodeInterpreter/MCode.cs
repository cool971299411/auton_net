﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Barcode
{
    public static class MCode
    {
        private static readonly int[] nums = 
            { 137, 138, 141, 142, 145, 146, 149, 150, 153, 154, 157, 162, 165, 166, 169, 170,
            173, 174, 177, 178, 181, 182, 185, 186, 197, 198, 201, 202, 205, 206, 209, 210,
            213, 214, 217, 218, 221, 226, 229, 230, 233, 234, 237, 238, 273, 274, 277, 278,
            281, 282, 285, 290, 293, 294, 297, 298, 301, 302, 305, 306, 309, 310, 313, 314,
            325, 326, 329, 330, 333, 334, 337, 338, 341, 342, 345, 346, 349, 354, 357, 358,
            361, 362, 365, 366, 369, 370, 373, 374, 393, 394, 397, 398, 401, 402, 405, 406,
            409, 410, 413, 418, 421, 422, 425, 426, 429, 430, 433, 434, 437, 438, 441, 442,
            453, 454, 457, 458, 461, 462, 465, 466, 469, 470, 473, 474, 477, 546, 549, 550,
            553, 554, 557, 558, 561, 562, 565, 566, 569, 570, 581, 582, 585, 586, 589, 590,
            593, 594, 597, 598, 601, 602, 605, 610, 613, 614, 617, 618, 621, 622, 625, 626,
            629, 630, 649, 650, 653, 654, 657, 658, 661, 662, 665, 666, 669, 674, 677, 678,
            681, 682, 685, 686, 689, 690, 693, 694, 697, 698, 709, 710, 713, 714, 717, 718,
            721, 722, 725, 726, 729, 730, 733, 738, 741, 742, 745, 746, 749, 750, 785, 786,
            789, 790, 793, 794, 797, 802, 805, 806, 809, 810, 813, 814, 817, 818, 821, 822,
            825, 826, 837, 838, 841, 842, 845, 846, 849, 850, 853, 854, 857, 858, 861, 866,
            869, 870, 873, 874, 877, 878, 881, 882, 885, 886, 905, 906, 909, 910, 913, 914,
            917, 918, 921, 922, 925, 930, 933, 934, 937, 938, 941, 942, 945, 946, 949, 950,
            953, 954, };

        public static int Count { get { return nums.Length; } }

        public static int Encode(int k)
        {
            try
            {
                return nums[k];
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException(String.Format("MCode encoding: encoded number {0} out of range", k));
            }
        }

        public static int Decode(int c)
        {
            int idx = Array.BinarySearch(nums, c);
            if (idx < 0)
                throw new ArgumentException(String.Format("No such MCode number as {0} exists", c));

            return idx;
        }
    }
}
