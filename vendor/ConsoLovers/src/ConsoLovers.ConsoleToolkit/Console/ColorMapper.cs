﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;
   using System.Runtime.InteropServices;

   /// <summary>
   ///    Exposes methods used for mapping System.Drawing.Color to System.ConsoleColors. Based on code that was originally written by Alex Shvedov, and that was then modified by
   ///    MercuryP.
   /// </summary>
   public sealed class ColorMapper
   {
      #region Constants and Fields

      const int STD_OUTPUT_HANDLE = -11; // per WinBase.h

      private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1); // per WinBase.h

      #endregion

      #region Public Methods and Operators

      public IMappingAction Map(ConsoleColor consoleColor)
      {
         return new MappingAction(consoleColor);
      }

      /// <summary>Maps a System.Drawing.Color to a System.ConsoleColor.</summary>
      /// <param name="consoleColor">The <see cref="ConsoleColor"/> to be changed.</param>
      /// <param name="color">The <see cref="Color"/> to be mapped to the <see cref="ConsoleColor"/> value.</param>
      public void MapColor(ConsoleColor consoleColor, Color color)
      {
         // NOTE: The default console colors used are gray (foreground) and black (background).
         MappingAction.MapColor(consoleColor, color.R, color.G, color.B);
      }

      #endregion

      #region Methods

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern IntPtr GetStdHandle(int nStdHandle);

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

      #endregion

      [StructLayout(LayoutKind.Sequential)]
      private struct COLORREF
      {
         private uint ColorDWORD;

         internal COLORREF(Color color)
         {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
         }

         internal COLORREF(uint r, uint g, uint b)
         {
            ColorDWORD = r + (g << 8) + (b << 16);
         }
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct CONSOLE_SCREEN_BUFFER_INFO_EX
      {
         internal int cbSize;

         internal COORD dwSize;

         internal COORD dwCursorPosition;

         internal ushort wAttributes;

         internal SMALL_RECT srWindow;

         internal COORD dwMaximumWindowSize;

         internal ushort wPopupAttributes;

         internal bool bFullscreenSupported;

         internal COLORREF black;

         internal COLORREF darkBlue;

         internal COLORREF darkGreen;

         internal COLORREF darkCyan;

         internal COLORREF darkRed;

         internal COLORREF darkMagenta;

         internal COLORREF darkYellow;

         internal COLORREF gray;

         internal COLORREF darkGray;

         internal COLORREF blue;

         internal COLORREF green;

         internal COLORREF cyan;

         internal COLORREF red;

         internal COLORREF magenta;

         internal COLORREF yellow;

         internal COLORREF white;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct COORD
      {
         internal short X;

         internal short Y;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct SMALL_RECT
      {
         internal short Left;

         internal short Top;

         internal short Right;

         internal short Bottom;
      }

      class MappingAction : IMappingAction
      {
         #region Constants and Fields

         private readonly ConsoleColor consoleColor;

         #endregion

         #region Constructors and Destructors

         public MappingAction(ConsoleColor consoleColor)
         {
            this.consoleColor = consoleColor;
         }

         #endregion

         #region IMappingAction Members

         public void To(Color color)
         {
            MapColor(consoleColor, color.R, color.G, color.B);
         }

         #endregion

         #region Methods

         internal static void MapColor(ConsoleColor consoleColor, uint r, uint g, uint b)
         {
            CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe); // 96 = 0x60

            IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE); // 7
            if (hConsoleOutput == INVALID_HANDLE_VALUE)
            {
               throw new ColorMappingException(Marshal.GetLastWin32Error());
            }

            bool brc = GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            if (!brc)
            {
               throw new ColorMappingException(Marshal.GetLastWin32Error());
            }

            switch (consoleColor)
            {
               case ConsoleColor.Black:
                  csbe.black = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkBlue:
                  csbe.darkBlue = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkGreen:
                  csbe.darkGreen = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkCyan:
                  csbe.darkCyan = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkRed:
                  csbe.darkRed = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkMagenta:
                  csbe.darkMagenta = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkYellow:
                  csbe.darkYellow = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Gray:
                  csbe.gray = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.DarkGray:
                  csbe.darkGray = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Blue:
                  csbe.blue = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Green:
                  csbe.green = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Cyan:
                  csbe.cyan = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Red:
                  csbe.red = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Magenta:
                  csbe.magenta = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.Yellow:
                  csbe.yellow = new COLORREF(r, g, b);
                  break;
               case ConsoleColor.White:
                  csbe.white = new COLORREF(r, g, b);
                  break;
            }

            csbe.srWindow.Bottom++;
            csbe.srWindow.Right++;

            brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            if (!brc)
            {
               throw new ColorMappingException(Marshal.GetLastWin32Error());
            }
         }

         #endregion
      }
   }
}