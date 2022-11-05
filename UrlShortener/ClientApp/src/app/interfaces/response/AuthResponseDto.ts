export interface AuthResponseDto {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
  email: string;
  uid: string;
}
