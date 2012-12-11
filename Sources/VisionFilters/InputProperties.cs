﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VisionFilters
{
    internal class InputProperties
    {
        public int width, height;
        public PointF[] src;
        public PointF[] dst;

        public InputProperties(int width_ = 640, int height_ = 480)
        {
            width  = width_;
            height = height_;

            // construct perspective transformation
            //src = new PointF[] { 
            //        new PointF(116,      108), 
            //        new PointF(116 + 88, 108),                                 
            //        new PointF(320,     217), 
            //        new PointF(0,       217), 
            //    };
                
            src = new PointF[]{
                new PointF(303,       106),
                new PointF(303 + 160, 106),
                new PointF(178 + 361, 255),
                new PointF(178,       255)
            };

            int offset = width / 4 + 64;
            dst = new PointF[] { 
                    new PointF(offset,       50), 
                    new PointF(width - offset, 50), 
                    new PointF(width - offset, height - 30),
                    new PointF(offset, height - 30)
                };
            //dst = new PointF[] { 
            //        new PointF(offset,       0), 
            //        new PointF(320 - offset, 0), 
            //        new PointF(src[2].X - offset, src[2].Y + 33), 
            //        new PointF(src[3].X + offset, src[3].Y + 33) 
            //    };

            System.Console.Out.WriteLine("Input properties initialized.");
        }
    }

    public class CamModel
    {
        static InputProperties prop = new InputProperties();
        static float metersToPixelsRatio = 20.0f; // 1m = 20 px

        public static int Width
        {
            get { return prop.width; }
        }

        public static int Height
        {
            get { return prop.height; }
        }

        public static PointF[] srcPerspective
        {
            get { return prop.src; }
        }

        public static PointF[] dstPerspective
        {
            get { return prop.dst; }
        }

        /// <summary>
        /// after perspective transform
        /// </summary>
        /// <param name="meters"></param>
        /// <returns></returns>
        public static int ToPixels(float meters)
        {
            return prop.height - (int)Math.Round(meters * metersToPixelsRatio);
        }
    }
}