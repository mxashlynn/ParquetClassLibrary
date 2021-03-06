namespace Parquet.Parquets
{
    /// <summary>
    /// Facilitates editing of a <see cref="CollectibleModel"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, subtypes of <see cref="CollectibleModel"/> should never themselves use <see cref="IMutableCollectibleModel"/>.
    /// IMutableCollectibleModel is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface IMutableCollectibleModel : IMutableParquetModel
    {
        /// <summary>The effect generated when a character encounters this Collectible.</summary>
        public CollectingEffect CollectionEffect { get; set; }

        /// <summary>
        /// The scale in points of the effect.
        /// For example, how much to alter a stat if the <see cref="CollectingEffect"/> is set to alter a stat.
        /// </summary>
        public int EffectAmount { get; set; }
    }
}
