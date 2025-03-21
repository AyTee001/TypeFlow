export interface TypingSessionData {
    challengeId: string | null | undefined,
    characterCount: number,
    errorCount: number
    finishedInSeconds: number
}

export interface TypingSessionResultData {
    id: number,
    userId: number,
    challengeId: number | null | undefined,
    finishedInSeconds: number
    errors: number,
    charactersCount: number,
    accuracy: number
    wordsPerMinute: number
    charactersPerMinute: number
}

export interface TypingSessionDisplayResult {
    finishedInSeconds: number
    errors: number,
    charactersCount: number,
    accuracy: number
    wordsPerMinute: number
    charactersPerMinute: number
}

export interface TypingSessionStatistics{
    bestResult: TypingSessionResultData | undefined | null,
    worstResult: TypingSessionResultData | undefined | null
}

export interface TypingSessionChartStatistics{
    dates: Date[],
    wpmValues: number | null | undefined,
    accuracyValues: number | null | undefined
}