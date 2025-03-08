export interface TypingSessionData {
    challengeId: string | null | undefined,
    characterCount: number,
    errorCount: number
    finishedInSeconds: number
}

export interface TypingSessionResult {
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