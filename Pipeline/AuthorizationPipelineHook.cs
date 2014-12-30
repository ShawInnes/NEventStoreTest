using System;
using NEventStore;

namespace NEventSourceTests.Pipeline
{
    public class AuthorizationPipelineHook : IPipelineHook
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ICommit Select(ICommit committed)
        {
            return committed;
        }

        public bool PreCommit(CommitAttempt attempt)
        {
            return true;
        }

        public void PostCommit(ICommit committed)
        {
            
        }

        public void OnPurge(string bucketId)
        {
            
        }

        public void OnDeleteStream(string bucketId, string streamId)
        {
            
        }
    }
}