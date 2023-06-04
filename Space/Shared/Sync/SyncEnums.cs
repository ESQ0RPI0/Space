namespace Space.Shared.Sync
{
    public enum SyncStates : byte
    {
        Created = 0,
        Processing = 1,
        Completed = 2,
        Deleted = 3,
        Postponed = 4,
        Paused = 5,
        Aborted = 6,
        Error = 255
    }

    public enum SyncProcessStages: byte
    {
        ExternalDataLoading = 0,
        ExternalDataMapping = 1,
        ExternalDataSaving = 2,
        ExternalDataFinalization = 3,
        ExternalDataFinalized = 4,
        InternalDataMapping = 5,
        InternalDataSaving = 6,
        InternalDataFinalization = 7,
        InternalDataFinalized = 8
    }
    public enum SyncTypes : byte
    {
        NewSpace = 0,
        SpaceFund = 1,
        Unknown = 255,
    }
}
