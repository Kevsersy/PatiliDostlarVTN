namespace PatiliDostlarVTN.ViewModels
{
    public record RoleSelectionVM
    {
        public string RoleName { get; init; }
        public bool IsAssigned { get; init; }

        public RoleSelectionVM(string roleName, bool isAssigned)
        {
            RoleName = roleName;
            IsAssigned = isAssigned;
        }
    }
}
