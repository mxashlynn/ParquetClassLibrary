using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetClassLibrary;

// Set assembly values.
[assembly: AssemblyTitle("Parquet Class Library")]
[assembly: AssemblyDescription("Core game mechanics for Parquet.")]
[assembly: AssemblyCompany("Queertet")]
[assembly: AssemblyCopyright("2018-2020 Paige Ashlynn")]
[assembly: AssemblyProduct("ParquetClassLibrary")]
[assembly: AssemblyVersion(AssemblyInfo.LibraryVersion)]
[assembly: AssemblyInformationalVersion(AssemblyInfo.LibraryVersion)]
[assembly: AssemblyFileVersion(AssemblyInfo.LibraryVersion)]

// Make no promises to maintain public services.
[assembly: ComVisible(false)]

// Declare American English as the coding language.
[assembly: NeutralResourcesLanguage("en-US")]

// Show warnings on CLS-noncompliant statements to better support .NET languages other than C#.
[assembly: CLSCompliant(true)]

// Allow unit tests to access classes and members with internal accessibility.
[assembly: InternalsVisibleTo("ParquetUnitTests")]

namespace ParquetClassLibrary
{
    /// <summary>
    /// Provides assembly-wide information.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance",
        "CA1815:Override equals and operator equals on value types",
        Justification = "Comparing two AssemblyInfos is nonsensical.")]
    public readonly struct AssemblyInfo
    {
        /// <summary>
        /// Describes the version of the serialized <see cref="Maps.MapModel"/>
        /// data that the class library understands.<para/>
        /// </summary>
        /// <remarks>
        /// The version has the format "{Major}.{Minor}.{Build}".
        /// <list type="number">
        /// <item><term>Major</term>
        /// <description>Breaking changes resulting in lost saves.</description></item>
        /// <item><term>Minor</term>
        /// <description>Backwards-compatible changes, preserving existing saves.</description></item>
        /// <item><term>Build</term>
        /// <description>Procedural updates that do not imply any changes.</description></item>
        /// </remarks>
        public const string SupportedMapDataVersion = "0.1.0";

        /// <summary>
        /// Describes the version of the serialized <see cref="Beings.PlayerCharacter"/>
        /// data that the class library understands.
        /// </summary>
        /// <remarks>
        /// The version has the format "{Major}.{Minor}.{Build}".
        /// <list type="number">
        /// <item><term>Major</term>
        /// <description>Breaking changes resulting in lost saves.</description></item>
        /// <item><term>Minor</term>
        /// <description>Backwards-compatible changes, preserving existing saves.</description></item>
        /// <item><term>Build</term>
        /// <description>Procedural updates that do not imply any changes.</description></item>
        /// </remarks>
        public const string SupportedBeingDataVersion = "0.1.0";

        /// <summary>Describes the version of the class library itself.</summary>
        /// <remarks>
        /// The version has the format "{Major}.{Minor}.{Patch}.{Build}".
        /// <list type="number">
        /// <item><term>Major</term>
        /// <description>Enhancements or fixes that break the API or its serialized data.</description></item>
        /// <item><term>Minor</term>
        /// <description>Enhancements that do not break the API or its serialized data.</description></item>
        /// <item><term>Patch</term>
        /// <description>Fixes that do not break the API or its serialized data.</description></item>
        /// <item><term>Build</term>
        /// <description>Procedural updates that do not imply any changes, such as when rebuilding for APK/IPA submission.</description></item>
        /// </remarks>
        public const string LibraryVersion = "0.1.0.0";
    }
}
