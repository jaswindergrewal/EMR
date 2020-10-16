﻿
using System;


    public static class ObjectExtensions
    {

        public static DateTime AsDate(this object input)
        {
            if (input == null)
            {
                return DateTime.MinValue;
            }

            return Convert.ToDateTime(input);
        }

    }

