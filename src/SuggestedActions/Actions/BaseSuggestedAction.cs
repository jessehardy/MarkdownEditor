using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;

namespace MarkdownEditor
{
    abstract class BaseSuggestedAction : ISuggestedAction
    {
        public abstract string DisplayText { get; }

        public virtual bool IsEnabled { get; } = true;

        public virtual bool HasActionSets
        {
            get { return false; }
        }

        public virtual bool HasPreview
        {
            get { return false; }
        }

        public string IconAutomationText
        {
            get { return null; }
        }

        public virtual ImageMoniker IconMoniker
        {
            get { return default(ImageMoniker); }
        }

        public string InputGestureText
        {
            get { return null; }
        }

        public virtual void Dispose()
        {
            // nothing to dispose
        }

        public Task<IEnumerable<SuggestedActionSet>> GetActionSetsAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<SuggestedActionSet>>(null);
        }

        public Task<object> GetPreviewAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public void Invoke(CancellationToken cancellationToken)
        {
            try
            {
                ProjectHelpers.DTE.UndoContext.Open(DisplayText);
                Execute(cancellationToken);
            }
            finally
            {
                ProjectHelpers.DTE.UndoContext.Close();
            }
        }

        public abstract void Execute(CancellationToken cancellationToken);

        public bool TryGetTelemetryId(out Guid telemetryId)
        {
            telemetryId = Guid.Empty;
            return false;
        }
    }
}
