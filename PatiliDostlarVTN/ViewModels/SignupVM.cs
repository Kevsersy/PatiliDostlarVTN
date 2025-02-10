namespace PatiliDostlarVTN.ViewModels;

public record SignupVM(
    string? UserName,
    string? Password,
    string? Email,
    DateTime BoD,
    string? ConfirmPassword
);
