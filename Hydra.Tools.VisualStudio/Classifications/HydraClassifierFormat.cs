//------------------------------------------------------------------------------
// <copyright file="HydraClassifierFormat.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Hydra.Tools.VisualStudio.Classifications
{
    /// <summary>
    /// Defines an editor format for the HydraClassifier type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "hydra.identifier")]
    [Name("hydra.identifier")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class HydraClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraClassifierFormat"/> class.
        /// </summary>
        public HydraClassifierFormat()
        {
            this.DisplayName = "Hydra - identifier"; // Human readable version of the name
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
}
