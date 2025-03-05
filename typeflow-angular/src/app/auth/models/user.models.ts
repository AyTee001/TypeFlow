export interface UserData {
    id: string;
    userName: string;
    email: string;
    registeredAt: Date;
}

export interface FullUserData extends UserData
{
    accuracy: number;
    totalTests: number;
    averageCPM: number;
    averageWPM: number;
}
