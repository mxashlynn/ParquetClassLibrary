#if DESIGN
using System.Collections.Generic;
using ParquetClassLibrary.Scripts;

namespace ParquetClassLibrary.EditorSupport
{
    /// <summary>
    /// Facilitates editing of a <see cref="InteractionModel"/> from design tools while maintaining a read-only face for use during play.
    /// </summary>
    /// <remarks>
    /// By design, subtypes of <see cref="InteractionModel"/> should never themselves use <see cref="IInteractionModelEdit"/>.
    /// IInteractionModelEdit is for use only by external types that require read/write access to model properties.
    /// </remarks>
    public interface IInteractionModelEdit : IModelEdit
    {
        /// <summary>
        /// Describes the criteria for begining this interaction.
        /// </summary>
        public IList<ModelID> PrerequisitesIDs { get; }

        /// <summary>
        /// Everything this interaction entails.
        /// </summary>
        public IList<ModelID> StepsIDs { get; }

        /// <summary>
        /// Describes the results of finishing this interaction.
        /// </summary>
        public IList<ModelID> OutcomesIDs { get; }
    }
}
#endif
