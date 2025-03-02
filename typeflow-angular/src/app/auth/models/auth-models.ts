export interface UserLogin{
    userName: string;
    password: string;
}

export interface UserRegistration {
    userName: string;
    password: string;
    email: string;
}

export interface Tokens{
    accessToken: string;
    refreshToken: string;

    accessTokenExpiration: Date;
    refreshTokenExpiration: Date;
}