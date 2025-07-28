export default interface  AuthResult{
    isAuthenticated: boolean;
    roles: string[];
    expiresAt: Date;
}