using MudBlazor;

namespace JixbeeRedesign.Components.Pages.Screens
{
    public partial class Login
    {
        private string TextValueEmail { get; set; }
        private string TextValuePassword { get; set; }
        private string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;
        private InputType PasswordInput { get; set; } = InputType.Password;
        private bool showPassword { get; set; } = false;

        void TogglePasswordVisiblity()
        {
            if (showPassword)
            {
                showPassword = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                showPassword = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
