//------------------------------------------------------------------------------
// <copyright file="HydraClassifier.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Hydra.Compiler;
using Hydra.Compiler.Grammar.Tree;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Hydra.Tools.VisualStudio
{
    /// <summary>
    /// Classifier that classifies all text as an instance of the "HydraClassifier" classification type.
    /// </summary>
    internal class HydraClassifier : IClassifier
    {
        private readonly HydraClassifierProvider m_Registry;
        private readonly ITextBuffer m_Buffer;

        public HydraClassifier(HydraClassifierProvider registry, ITextBuffer buffer)
        {
            m_Registry = registry;
            m_Buffer = buffer;
        }

        #region IClassifier

#pragma warning disable 67

        /// <summary>
        /// An event that occurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event gets raised if a non-text change would affect the classification in some way,
        /// for example typing /* would cause the classification to change in C# without directly
        /// affecting the span.
        /// </remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        /// <summary>
        /// Gets all the <see cref="ClassificationSpan"/> objects that intersect with the given range of text.
        /// </summary>
        /// <remarks>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </remarks>
        /// <param name="span">The span currently being classified.</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>();

            var snapshot = span.Snapshot;
            int firstLine = snapshot.GetLineNumberFromPosition(span.Start);
            int lastLine = snapshot.GetLineNumberFromPosition(span.End);

            for (int lineIndex = firstLine; lineIndex <= lastLine; lineIndex++)
            {
                var line = snapshot.GetLineFromLineNumber(lineIndex);
                var lineText = line.GetText();

                var scanner = new HydraScanner();
                scanner.SetSource(lineText, 0);
                scanner.NextToken();

                var token = scanner.NextToken();
                while (token != null && token.TokenType != HydraTokenType.EOF)
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(snapshot, new Span(token.Offset, token.Text.Length)), ClassifyToken(token)));

                    token = scanner.NextToken();
                }
            }

            return result;
        }

        private IClassificationType ClassifyToken(HydraToken token)
        {
            switch (token.TokenType)
            {
                case HydraTokenType.ID:
                    return m_Registry.Identifier;

                default:
                    return m_Registry.Keyword;
            }
        }

        #endregion
    }
}
