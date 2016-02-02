//------------------------------------------------------------------------------
// <copyright file="HydraClassifierClassificationDefinition.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Hydra.Tools.VisualStudio
{
    /// <summary>
    /// Classification type definition export for HydraClassifier
    /// </summary>
    internal static class HydraClassifierClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "HydraClassifier" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("HydraClassifier")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
