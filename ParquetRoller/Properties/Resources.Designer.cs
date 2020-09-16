﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParquetRoller.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ParquetRoller.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To access this command, please rebuild Roller with editor support enabled..
        /// </summary>
        internal static string ErrorEditorSupport {
            get {
                return ResourceManager.GetString("ErrorEditorSupport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading data files..
        /// </summary>
        internal static string ErrorLoading {
            get {
                return ResourceManager.GetString("ErrorLoading", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specify property.
        ///.
        /// </summary>
        internal static string ErrorNoProperty {
            get {
                return ResourceManager.GetString("ErrorNoProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error saving data files..
        /// </summary>
        internal static string ErrorSaving {
            get {
                return ResourceManager.GetString("ErrorSaving", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unrecognized category{0}.
        ///.
        /// </summary>
        internal static string ErrorUnknownCategory {
            get {
                return ResourceManager.GetString("ErrorUnknownCategory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unrecognized property {0}.
        ///.
        /// </summary>
        internal static string ErrorUnknownProperty {
            get {
                return ResourceManager.GetString("ErrorUnknownProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: {1} collides with {2}.
        ///.
        /// </summary>
        internal static string InfoCollision {
            get {
                return ResourceManager.GetString("InfoCollision", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collisions in {0}:
        ///.
        /// </summary>
        internal static string InfoCollisionsHeader {
            get {
                return ResourceManager.GetString("InfoCollisionsHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No defined content.
        ///.
        /// </summary>
        internal static string InfoNoContent {
            get {
                return ResourceManager.GetString("InfoNoContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Checking {0}..
        /// </summary>
        internal static string MessageChecking {
            get {
                return ResourceManager.GetString("MessageChecking", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: roller (command)
        ///
        ///Commands:
        ///    -h|help                         Display detailed help.
        ///    -v|version                      Display version information.
        ///    -t|templates                    Write CSV templates to current directory.
        ///    -r|roll                         Prepare CSVs in current directory for use.
        ///    -c|check                        Check that map adjacency is consistent.
        ///    -p|list pronouns                List all defined pronoun groups.
        ///    -l|list (property) [category]   List various mo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MessageDefault {
            get {
                return ResourceManager.GetString("MessageDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to     Roller is a tool for working with Parquet configuration files.
        ///    Parquet uses comma-separated value (CSV) files for configuration.
        ///    Roller provides a quick way to examine the content of existing CSV files, to
        ///    generate blank CSV files, and to prepare existing CSV files for use in-game.
        ///
        ///Usage: roller (command)
        ///
        ///Commands:
        ///    -h|help                         Display detailed help.
        ///    -v|version                      Display version information.
        ///    -t|templates                    Write CSV templat [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MessageHelp {
            get {
                return ResourceManager.GetString("MessageHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version:
        ///    Roller      {0}
        ///    Parquet     {1}
        ///.
        /// </summary>
        internal static string MessageVersion {
            get {
                return ResourceManager.GetString("MessageVersion", resourceCulture);
            }
        }
    }
}
