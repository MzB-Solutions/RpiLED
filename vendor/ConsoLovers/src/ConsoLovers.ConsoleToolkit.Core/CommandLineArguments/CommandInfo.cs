// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using JetBrains.Annotations;
    using System;
    using System.Reflection;

    /// <summary>Information class for a property that was decorated with the <see cref="CommandAttribute"/></summary>
    /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ParameterInfo"/>
    public class CommandInfo : ParameterInfo
    {
        #region Private Methods

        /// <summary>Computes the <see cref="Type"/> of the commands argument.</summary>
        /// <returns></returns>
        private Type ComputeArgumentType()
        {
            var commandInterface = PropertyInfo.PropertyType.GetInterface(typeof(ICommand<>).FullName);
            if (commandInterface == null)
                return null;

            return commandInterface.GenericTypeArguments[0];
        }

        #endregion Private Methods

        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="CommandInfo"/> class.</summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="commandLineAttribute">The command line attribute.</param>
        public CommandInfo([NotNull] PropertyInfo propertyInfo, [NotNull] CommandAttribute commandLineAttribute)
         : base(propertyInfo, commandLineAttribute)
        {
            ArgumentType = ComputeArgumentType();
            IsDefault = commandLineAttribute.IsDefaultCommand;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Gets or sets the type of the argument.</summary>
        public Type ArgumentType { get; }

        /// <summary>Gets the <see cref="CommandAttribute"/>.</summary>
        public CommandAttribute Attribute => (CommandAttribute)CommandLineAttribute;

        /// <summary>Gets a value indicating whether this command is the default command.</summary>
        public bool IsDefault { get; }

        #endregion Public Properties
    }
}