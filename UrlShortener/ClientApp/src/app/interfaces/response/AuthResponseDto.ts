export interface AuthResponseDto {
  isAuthSuccessful: boolean;
  token: string;
  registrationErrors: string[];
}
