// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsole.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    using System;

    public interface IConsole
    {
        #region Public Properties

        ConsoleColor BackgroundColor { get; set; }

        int CursorLeft { get; set; }

        int CursorSize { get; set; }
        int CursorTop { get; set; }

        ConsoleColor ForegroundColor { get; set; }

        int LargestWindowHeight { get; }
        int LargestWindowWidth { get; }
        int WindowHeight { get; set; }
        int WindowWidth { get; set; }

        #endregion Public Properties

        #region Public Methods

        void Beep();

        void Clear();

        void Clear(ConsoleColor color);

        ConsoleKeyInfo ReadKey();

        ConsoleKeyInfo ReadKey(bool intercept);

        string ReadLine();

        void ResetColor();

        void SetCursorPosition(int left, int top);

        void WaitForKey(ConsoleKey key);

        void Write(string value);

        void Write(char value);

        void Write(string value, ConsoleColor foreground, ConsoleColor background);

        void Write(string value, ConsoleColor foreground);

        void WriteLine();

        void WriteLine(string value);

        void WriteLine(bool value);

        void WriteLine(char value);

        void WriteLine(char[] value);

        void WriteLine(decimal value);

        void WriteLine(double value);

        void WriteLine(float value);

        void WriteLine(int value);

        void WriteLine(long value);

        void WriteLine(object value);

        void WriteLine(uint value);

        void WriteLine(ulong value);

        void WriteLine(string format, object arg0);

        void WriteLine(string format, params object[] args);

        void WriteLine(char[] buffer, int index, int count);

        void WriteLine(string format, object arg0, object arg1);

        void WriteLine(string format, object arg0, object arg1, object arg2);

        void WriteLine(string format, object arg0, object arg1, object arg2, object arg3);

        void WriteLine(string value, ConsoleColor foreground);

        void WriteLine(string value, ConsoleColor foreground, ConsoleColor background);

        #endregion Public Methods
    }
}