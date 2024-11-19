namespace LibraRestaurant.Domain.Constants;

public static class MaxLengths
{
    public static class Employee
    {
        public const int Email = 320;
        public const int FirstName = 100;
        public const int LastName = 100;
        public const int Mobile = 15;
        public const int Password = 128;
    }

    public static class MenuItem
    {
        public const int Title = 75;
        public const int Slug = 100;
        public const int Summary = 1000;
        public const int SKU = 100;
        public const int Repice = 1000;
        public const int Instruction = 1000;
    }
}