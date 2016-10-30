namespace SharpFly_Cluster.Server.Interserver
{
    public class RequestSuccesfulEventArgs
    {
        public bool Accepted { get; private set; }
        public uint Id { get; private set; }

        public RequestSuccesfulEventArgs(bool accepted, uint id)
        {
            Accepted = accepted;
            Id = Id;
        }
    }
}
