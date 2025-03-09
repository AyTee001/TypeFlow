import { TypingSessionStatistics } from "../../../shared/models/typing-session.models";

export interface UserData {
    id: string;
    userName: string;
    email: string;
    registeredAt: Date;
}

export interface FullUserData extends UserData
{
    statistics: TypingSessionStatistics;
}
