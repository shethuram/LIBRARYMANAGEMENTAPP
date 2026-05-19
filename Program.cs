using LibraryManagementApp.Presentation;

namespace LibraryManagementApp;

public class Program
{
    public static void Main(string[] args)
    {
        MainMenu menu = new();

        menu.ShowMenu();
    }
}