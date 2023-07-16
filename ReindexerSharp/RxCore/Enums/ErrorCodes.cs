namespace ReindexerClient.RxCore.Enums
{
    public enum ErrorCodes
    {        
        ErrOK = 0,
        ErrParseSQL = 1,
        ErrQueryExec = 2,
        ErrParams = 3,
        ErrLogic = 4,
        ErrParseJson = 5,
        ErrParseDSL = 6,
        ErrConflict = 7,
        ErrParseBin = 8,
        ErrForbidden = 9,
        ErrWasRelock = 10,
        ErrNotValid = 11,
        ErrNetwork = 12,
        ErrNotFound = 13,
        ErrStateInvalidated = 14,
        ErrBadTransaction = 15,
        ErrOutdatedWAL = 16,
        ErrNoWAL = 17,
        ErrDataHashMismatch = 18
    }
}
