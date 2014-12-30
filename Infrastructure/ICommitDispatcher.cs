namespace NEventSourceTests.Infrastructure
{
    public interface ICommitDispatcher
    {
        void Dispatch(IEvent commit);
    }
}