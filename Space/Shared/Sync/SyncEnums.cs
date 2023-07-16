namespace Space.Shared.Sync
{
    public enum SyncStates : byte
    {
        Created = 0,
        Processing = 1,
        Postponed = 2,
        Paused = 3,
        Aborted = 252,
        Deleted = 253,
        Completed = 254,
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
