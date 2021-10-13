﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>Event args for the <see cref="IArgumentMapper{T}.UnmappedCommandLineArgument"/> event</summary>
    /// <seealso cref="System.EventArgs"/>
    [DebuggerDisplay("{" + nameof(DebuggerString) + "}")]
    public class MapperEventArgs : EventArgs
    {
        #region Internal Properties

        internal string DebuggerString => $"{PropertyInfo?.Name} <= {Argument?.DebuggerString}";

        #endregion Internal Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperEventArgs" /> class.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="instance"></param>
        /// <exception cref="ArgumentNullException">argument</exception>
        public MapperEventArgs([NotNull] CommandLineArgument argument, [CanBeNull] PropertyInfo propertyInfo, object instance)
        {
            Argument = argument ?? throw new ArgumentNullException(nameof(argument));
            PropertyInfo = propertyInfo;
            Instance = instance;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets the argument that could not be mapped.</summary>
        public CommandLineArgument Argument { get; }

        /// <summary>Gets the instance of the arguments class the command <see cref="CommandLineArgument"/> was mapped to.</summary>
        public object Instance { get; }

        /// <summary>Gets the <see cref="PropertyInfo"/> of the property the argument was mapped to.</summary>
        public PropertyInfo PropertyInfo { get; }

        #endregion Public Properties
    }
}