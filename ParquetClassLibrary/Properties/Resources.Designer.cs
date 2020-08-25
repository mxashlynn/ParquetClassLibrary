﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParquetClassLibrary.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ParquetClassLibrary.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to above.
        /// </summary>
        internal static string DirectionAbove {
            get {
                return ResourceManager.GetString("DirectionAbove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to below.
        /// </summary>
        internal static string DirectionBelow {
            get {
                return ResourceManager.GetString("DirectionBelow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to east.
        /// </summary>
        internal static string DirectionEast {
            get {
                return ResourceManager.GetString("DirectionEast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to north.
        /// </summary>
        internal static string DirectionNorth {
            get {
                return ResourceManager.GetString("DirectionNorth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to south.
        /// </summary>
        internal static string DirectionSouth {
            get {
                return ResourceManager.GetString("DirectionSouth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to west.
        /// </summary>
        internal static string DirectionWest {
            get {
                return ResourceManager.GetString("DirectionWest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to add already-contained {0} &apos;{1}&apos;.  Did you mean to call Replace?.
        /// </summary>
        internal static string ErrorCannotAdd {
            get {
                return ResourceManager.GetString("ErrorCannotAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not convert &apos;{0}&apos; to {1}..
        /// </summary>
        internal static string ErrorCannotConvert {
            get {
                return ResourceManager.GetString("ErrorCannotConvert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not parse &apos;{0}&apos; as {1}..
        /// </summary>
        internal static string ErrorCannotParse {
            get {
                return ResourceManager.GetString("ErrorCannotParse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to remove {0} with ID {1}, but it is not contained in this collection..
        /// </summary>
        internal static string ErrorCannotRemove {
            get {
                return ResourceManager.GetString("ErrorCannotRemove", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to replace non-contained {0} &apos;{1}&apos;.  Did you mean to call Add?.
        /// </summary>
        internal static string ErrorCannotReplace {
            get {
                return ResourceManager.GetString("ErrorCannotReplace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You must clear the model database before reloading it..
        /// </summary>
        internal static string ErrorClearAllFirst {
            get {
                return ResourceManager.GetString("ErrorClearAllFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is of type {1} but must be of type {2}..
        /// </summary>
        internal static string ErrorInvalidCast {
            get {
                return ResourceManager.GetString("ErrorInvalidCast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid position: {0} is not within {1}..
        /// </summary>
        internal static string ErrorInvalidPosition {
            get {
                return ResourceManager.GetString("ErrorInvalidPosition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No model of type {0} exists for ID {1}..
        /// </summary>
        internal static string ErrorModelNotFound {
            get {
                return ResourceManager.GetString("ErrorModelNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be a non-negative number..
        /// </summary>
        internal static string ErrorMustBeNonNegative {
            get {
                return ResourceManager.GetString("ErrorMustBeNonNegative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be a positive number..
        /// </summary>
        internal static string ErrorMustBePositive {
            get {
                return ResourceManager.GetString("ErrorMustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} cannot be empty..
        /// </summary>
        internal static string ErrorMustNotBeEmpty {
            get {
                return ResourceManager.GetString("ErrorMustNotBeEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} cannot be None..
        /// </summary>
        internal static string ErrorMustNotBeNone {
            get {
                return ResourceManager.GetString("ErrorMustNotBeNone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} cannot be null..
        /// </summary>
        internal static string ErrorMustNotBeNull {
            get {
                return ResourceManager.GetString("ErrorMustNotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} cannot be null or empty..
        /// </summary>
        internal static string ErrorMustNotBeNullEmpty {
            get {
                return ResourceManager.GetString("ErrorMustNotBeNullEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No entry/exit found in {0} or {1}..
        /// </summary>
        internal static string ErrorNoExitFound {
            get {
                return ResourceManager.GetString("ErrorNoExitFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: {1} is not within {2}..
        /// </summary>
        internal static string ErrorOutOfBounds {
            get {
                return ResourceManager.GetString("ErrorOutOfBounds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is {1} but must be greater than or equal to {2}..
        /// </summary>
        internal static string ErrorOutOfOrderGTE {
            get {
                return ResourceManager.GetString("ErrorOutOfOrderGTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is {1} but must be less than or equal to {2}..
        /// </summary>
        internal static string ErrorOutOfOrderLTE {
            get {
                return ResourceManager.GetString("ErrorOutOfOrderLTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The direction &apos;{0}&apos; is not defined..
        /// </summary>
        internal static string ErrorUndefinedDirection {
            get {
                return ResourceManager.GetString("ErrorUndefinedDirection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot access {0} on ungenerated {1}..
        /// </summary>
        internal static string ErrorUngenerated {
            get {
                return ResourceManager.GetString("ErrorUngenerated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dimension outside specification: {0}.
        /// </summary>
        internal static string ErrorUnsupportedDimension {
            get {
                return ResourceManager.GetString("ErrorUnsupportedDimension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to duplicate {0} {1}..
        /// </summary>
        internal static string ErrorUnsupportedDuplicate {
            get {
                return ResourceManager.GetString("ErrorUnsupportedDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported {0} node {1}..
        /// </summary>
        internal static string ErrorUnsupportedNode {
            get {
                return ResourceManager.GetString("ErrorUnsupportedNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Serializing or deserializing {0} is not yet supported..
        /// </summary>
        internal static string ErrorUnsupportedSerialization {
            get {
                return ResourceManager.GetString("ErrorUnsupportedSerialization", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to give {0} to {1}..
        /// </summary>
        internal static string WarningTriedToGiveNothing {
            get {
                return ResourceManager.GetString("WarningTriedToGiveNothing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tried to store {0} in {1}..
        /// </summary>
        internal static string WarningTriedToStoreNothing {
            get {
                return ResourceManager.GetString("WarningTriedToStoreNothing", resourceCulture);
            }
        }
    }
}
