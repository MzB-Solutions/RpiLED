﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   public class Setup
   {
      #region Public Methods and Operators

      public static ArgumentEngineSetup ArgumentEngine()
      {
         return new ArgumentEngineSetup();
      }

      public static ArgumentMapperSetup ArgumentMapper()
      {
         return new ArgumentMapperSetup();
      }

      public static CommandLinerParserSetup CommandLineArgumentParser()
      {
         return new CommandLinerParserSetup();
      }

      public static EngineFactorySetup EngineFactory()
      {
         return new EngineFactorySetup();
      }

      public static MockSetup MockFor()
      {
         return new MockSetup();
      }

      #endregion

      public static HelpCommandSetup HelpCommand()
      {
         return new HelpCommandSetup();
      }
   }
}