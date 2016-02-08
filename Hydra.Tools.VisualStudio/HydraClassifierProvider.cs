//------------------------------------------------------------------------------
// <copyright file="HydraClassifierProvider.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Hydra.Tools.VisualStudio
{
    /// <summary>
    /// Classifier provider. It adds the classifier to the set of classifiers.
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")] // This classifier applies to all text files.
    internal class HydraClassifierProvider : IClassifierProvider
    {
        /// <summary>
        /// Classification registry to be used for getting a reference
        /// to the custom classification type later.
        /// </summary>
        [Import]
        private IClassificationTypeRegistryService m_ClassificationRegistry;

        public IClassificationType Keyword { get; private set; }
        public IClassificationType Identifier { get; private set; }
        private Dictionary<TokenCategory, IClassificationType> m_CategoryMap;

        private Dictionary<TokenCategory, IClassificationType> FillCategoryMap(IClassificationTypeRegistryService registry)
        {
            var categoryMap = new Dictionary<TokenCategory, IClassificationType>
            {
                [TokenCategory.Keyword] = Keyword = registry.GetClassificationType(PredefinedClassificationTypeNames.Keyword),
                [TokenCategory.Directive] = registry.GetClassificationType(PredefinedClassificationTypeNames.Keyword),
                [TokenCategory.Identifier] = Identifier = registry.GetClassificationType(PredefinedClassificationTypeNames.Identifier)
            };

            return categoryMap;
        }

        /// <summary>
        /// Gets a classifier for the given text buffer.
        /// </summary>
        /// <param name="buffer">The <see cref="ITextBuffer"/> to classify.</param>
        /// <returns>A classifier for the text buffer, or null if the provider cannot do so in its current state.</returns>
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (m_CategoryMap == null)
            {
                m_CategoryMap = FillCategoryMap(m_ClassificationRegistry);
            }

            HydraClassifier res;
            if (!buffer.Properties.TryGetProperty(typeof(HydraClassifier), out res))
            {
                res = new HydraClassifier(this, buffer);
                buffer.Properties.AddProperty(typeof(HydraClassifier), res);
            }

            return res;
        }
    }
}
