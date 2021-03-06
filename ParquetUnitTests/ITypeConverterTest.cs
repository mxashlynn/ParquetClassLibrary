using System;
using System.Linq;
using System.Reflection;
using CsvHelper.TypeConversion;
using Parquet;
using Xunit;

namespace ParquetUnitTests
{
    /// <summary>
    /// Tests that classes implementing <see cref="ITypeConverter"/> also provide a ConverterFactory static property.
    /// </summary>
    public class ITypeConverterTest
    {
        /// <summary>
        /// Ensures that all concrete classes implementing the ITypeConverter interface also implement the ConverterFactory static property.
        /// </summary>
        [Fact]
        internal void AllTypeConvertersProvideFactoriesTest()
        {
            // This discarded value is here to ensure that ParquetClassLibrary is loaded.
            var _ = new Point2D(1, 2);
            var converterProviders = AppDomain.CurrentDomain
                                              .GetAssemblies()
                                              .Where(assembly => assembly.GetName().Name == "ParquetClassLibrary")
                                              .SelectMany(assembly => assembly.GetExportedTypes())
                                              .Where(type => typeof(ITypeConverter).IsAssignableFrom(type)
                                                          && !type.IsInterface
                                                          && !type.IsAbstract);

            var factoryProviders = AppDomain.CurrentDomain
                                              .GetAssemblies()
                                              .Where(assembly => assembly.GetName().Name == "ParquetClassLibrary")
                                              .SelectMany(assembly => assembly.GetExportedTypes())
                                              .Where(type => type.GetRuntimeProperties()
                                                                 .Any(info => info.Name == nameof(ModelTag.ConverterFactory)));

            foreach (var provider in converterProviders)
            {
                Assert.Contains(provider, factoryProviders);
            }
        }
    }
}
